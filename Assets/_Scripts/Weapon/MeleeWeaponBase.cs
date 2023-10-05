using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeleeWeaponBase : WeaponBase
{
    [SerializeField] private Animator animator;
    [SerializeField] Collider2D attackArea;
    [SerializeField] float attackDelay = 0.2f;

    protected override void OnEnable()
    {
        base.OnEnable();
        attackArea.enabled = false;
    }

    protected override void Attack()
    {
        StartCoroutine(MeleeAttackCoroutine());
    }

    IEnumerator MeleeAttackCoroutine()
    {
        animator.SetTrigger("Attack");
        attackArea.enabled = true;
        yield return new WaitForSeconds(attackDelay);

        List<Collider2D> hitEnemies = new();
        ContactFilter2D contactFilter = new();
        contactFilter.SetLayerMask(enemyLayer);
        contactFilter.useTriggers = true;
        Physics2D.OverlapCollider(attackArea, contactFilter, hitEnemies);

        foreach (var enemy in hitEnemies)
        {
            if (!enemy.gameObject.CompareTag("DamageReceiver")) continue;
            KeyValuePair<float, bool> kvp = new(0.0f, false);
            kvp = PlayerCtrl.Instance.PlayerDamageSender.GetPlayerDamage(damage);
            float finalDamage = kvp.Key;

            if (PlayerCtrl.Instance.PlayerDamageSender.Send(enemy.transform, finalDamage))
            {
                GameObject textPopupGO;
                if (kvp.Value)
                {
                    textPopupGO = TextPopupSpawner.Instance.Spawn(EnumManager.TextPopupCode.CRIT_DAMAGE_TEXT_POPUP.ToString(), enemy.transform.position, Quaternion.identity);
                }
                else
                {
                    textPopupGO = TextPopupSpawner.Instance.Spawn(EnumManager.TextPopupCode.DAMAGE_TEXT_POPUP.ToString(), enemy.transform.position, Quaternion.identity);
                }
                if (textPopupGO != null) textPopupGO.GetComponentInChildren<TextMeshProUGUI>().text = ((int)finalDamage).ToString();

                EnemyDamageReceiver enemyDamageReceiver = enemy.GetComponent<EnemyDamageReceiver>();
                // if (!enemyDamageReceiver.EnemyCtrl.EnemyStatus.IsBoss && !enemyDamageReceiver.IsDead())
                // {
                //     enemyDamageReceiver.EnemyCtrl.EnemyMovement.AddForceToEnemy(enemyDamageReceiver.EnemyCtrl.transform.position - PlayerCtrl.Instance.transform.position, 200f);
                // }
            }
        }
        attackArea.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
