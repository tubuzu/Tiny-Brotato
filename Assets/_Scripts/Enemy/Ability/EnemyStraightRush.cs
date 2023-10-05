using System;
using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class EnemyStraightRush : EnemyAbility
{
    [SerializeField] protected float rushDuration = 1f;
    [SerializeField] protected float rushSpeed = 6f;
    [SerializeField] protected float rushDelay = .25f;

    [SerializeField] protected GameObject selfImpactCollider;
    [SerializeField] protected GameObject rushImpactCollider;


    public override IEnumerator ReleaseAttack()
    {
        enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.StartRushAttack.ToString());
        enemyCtrl.EnemyAnimation.SetCanChangeAnim(false);

        StartCoroutine(enemyCtrl.EnemyMovement.ChangeVelocity(Vector2.zero));
        yield return new WaitForSeconds(rushDelay);

        Vector2 direction = playerDamageReceiver.Collider.bounds.center - enemyCtrl.EnemyDamageReceiver.Collider.bounds.center;
        enemyCtrl.EnemyAnimation.SetCanChangeAnim(true);
        enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.RushAttack.ToString());
        enemyCtrl.EnemyAnimation.SetCanChangeAnim(false);
        if (rushImpactCollider != null)
            rushImpactCollider.SetActive(true);
        float originalDamage = enemyCtrl.EnemyDamageSender.Damage;
        enemyCtrl.EnemyDamageSender.Damage = damage;

        StartCoroutine(enemyCtrl.EnemyMovement.ChangeVelocity(direction, rushSpeed));
        yield return new WaitForSeconds(rushDuration);

        enemyCtrl.EnemyAnimation.SetCanChangeAnim(true);
        enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.EndRushAttack.ToString());
        enemyCtrl.EnemyAnimation.SetCanChangeAnim(false);
        if (rushImpactCollider != null)
            rushImpactCollider.SetActive(false);
        enemyCtrl.EnemyDamageSender.Damage = originalDamage;

        StartCoroutine(enemyCtrl.EnemyMovement.ChangeVelocity(Vector2.zero));
        yield return new WaitForSeconds(rushDelay);

        enemyCtrl.EnemyAnimation.SetCanChangeAnim(true);
    }
}
