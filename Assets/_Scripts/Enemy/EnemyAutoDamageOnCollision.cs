using System.Collections;
using UnityEngine;

public class EnemyAutoDamageOnCollision : EnemyAbstract
{
    private float enemyDamageDelay = 0.5f;
    private bool canAttack = true;
    private bool triggeringPlayer = false;

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DamageReceiver" && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            triggeringPlayer = true;
            StartCoroutine(ContinuousDamage(collision));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DamageReceiver" && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            triggeringPlayer = false;
        }
    }

    private void ResetCoolDown()
    {
        canAttack = true;
    }

    IEnumerator ContinuousDamage(Collider2D player)
    {
        while (triggeringPlayer && !enemyCtrl.EnemyDamageReceiver.IsDead() && GameManager.Instance.WaveInProgress)
        {
            if (canAttack)
            {
                enemyCtrl.EnemyDamageSender.Send(player.transform);
                canAttack = false;
                Invoke(nameof(ResetCoolDown), enemyDamageDelay);
            }
            yield return null;
        }
    }
}
