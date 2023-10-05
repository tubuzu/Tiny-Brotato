using System.Collections;
using UnityEngine;

public class EnemyDamageReceiver : DamageReceiver
{
    [SerializeField] protected EnemyCtrl enemyCtrl;
    public EnemyCtrl EnemyCtrl { get { return enemyCtrl; } }

    [SerializeField] float despawnDelay = 1;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyCtrl();
    }
    protected virtual void LoadEnemyCtrl()
    {
        if (!transform.parent.TryGetComponent(out enemyCtrl)) enemyCtrl = transform.GetComponent<EnemyCtrl>();
    }

    protected override void OnDead()
    {
        // FXSpawner.Instance.Spawn(DeathEffectType.BloodEffect.ToString(), transform.parent.position, Quaternion.identity);
        // AudioManager.Instance.PlayEnemyDeathSfx();
        StartCoroutine(OnDeadCoroutine());
    }

    IEnumerator OnDeadCoroutine()
    {
        enemyCtrl.EnemyAnimation.SetCanChangeAnim(true);
        enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.Die.ToString());
        enemyCtrl.EnemyAnimation.SetCanChangeAnim(false);

        Bounds bounds = Collider.bounds;
        if (enemyCtrl.EnemyStatus.IsBoss)
        {
            PlayerCtrl.Instance.PlayerStatus.ChangeGemNum(enemyCtrl.EnemyStatus.GetPoints());
        }
        else
        {
            for (int i = 0; i < enemyCtrl.EnemyStatus.GetPoints(); i++)
            {
                Vector2 randomPoint = new(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
                float randomAngle = Random.Range(0f, Mathf.PI * 2);
                Vector2 randomDirection = new(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
                randomDirection.Normalize();
                GameObject itemGO = ItemDropSpawner.Instance.Spawn(EnumManager.ItemDropCode.GEM.ToString(), randomPoint, Quaternion.identity);
                itemGO.GetComponent<ItemDrop>().OnShowUp(randomDirection);
            }
        }

        if (!enemyCtrl.EnemyStatus.IsBoss)
        {
            enemyCtrl.Rb.drag = 5;
            enemyCtrl.EnemyMovement.AddForceToEnemy(enemyCtrl.transform.position - PlayerCtrl.Instance.transform.position, 1000f);
        }
        else
        {
            GameManager.Instance.WinWave();
        }

        yield return new WaitForSeconds(despawnDelay);
        enemyCtrl.Rb.drag = 0;
        enemyCtrl.EnemyAnimation.SetCanChangeAnim(true);
        EnemySpawner.Instance.Despawn(enemyCtrl.gameObject);
    }

    // IEnumerator OnDeadRollBack()
    // {
    //     Vector2 direction = enemyCtrl.transform.position - PlayerCtrl.Instance.transform.position;
    //     float timer = 0;
    //     while(timer < despawnDelay)
    //     {

    //     }
    // }
}
