using System.Collections;
using UnityEngine;

public class EnemyAnimation : CharacterAnimation
{
    [SerializeField] protected EnemyCtrl enemyCtrl;
    public EnemyCtrl EnemyCtrl { get { return enemyCtrl; } }

    WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    Color originalColor = Color.white;
    Color shootAttackColor = Color.red;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyCtrl();
    }
    protected virtual void LoadEnemyCtrl()
    {
        enemyCtrl = transform.parent.GetComponent<EnemyCtrl>();
        if (enemyCtrl == null) enemyCtrl = transform.GetComponent<EnemyCtrl>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        spriteRenderer.color = Color.white;
        canChangeAnim = true;
    }

    public void SetSpeed(float speed)
    {
        if (speed > 0.2f) ChangeAnimationState(EnemyAnimationState.Move.ToString());
        else ChangeAnimationState(EnemyAnimationState.Idle.ToString());
    }
    // public void CollisionAttack()
    // {
    //     ChangeAnimationState(EnemyAnimationState.CollisionAttack.ToString());
    //     canChangeAnim = false;
    //     Invoke(nameof(ResetCanChangeAnim), collisionAttackDuration);
    // }

    public void WarningBeforeAttack(float attackDelay)
    {
        StartCoroutine(AttackAnimationCoroutine(attackDelay));
    }

    IEnumerator AttackAnimationCoroutine(float attackDelay)
    {
        float timer;
        float elapsed = 0.25f;

        for (int i = 0; i < Mathf.RoundToInt(attackDelay / 0.5f); i++)
        {
            timer = 0;
            while (timer <= elapsed)
            {
                timer += Time.fixedDeltaTime;
                float lerpAmount = timer / elapsed;
                spriteRenderer.color = Color.Lerp(originalColor, shootAttackColor, lerpAmount);

                yield return waitForFixedUpdate;
            }
            timer = 0;
            while (timer <= elapsed)
            {
                timer += Time.fixedDeltaTime;
                float lerpAmount = timer / elapsed;
                spriteRenderer.color = Color.Lerp(shootAttackColor, originalColor, lerpAmount);

                yield return waitForFixedUpdate;
            }
        }

        spriteRenderer.color = originalColor;
    }
}

public enum EnemyAnimationState
{
    Idle,
    Move,
    Die,
    CollisionAttack,
    ShootAttack,
    StartRushAttack,
    RushAttack,
    EndRushAttack,
    SpawnBomb,
    StartShootLaser,
    // Laser,
    // EndLaser,
}
