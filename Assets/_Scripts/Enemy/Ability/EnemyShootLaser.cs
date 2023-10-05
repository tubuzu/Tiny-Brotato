using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class EnemyShootLaser : EnemyAbility
{
    [SerializeField] protected float laserDelay = .25f;
    [SerializeField] protected float laserDuration = 1f;
    [SerializeField] protected EnumManager.BulletCode bulletCode;

    [SerializeField] protected Transform shootPoint;

    [SerializeField] protected LayerMask shooterLayer;
    [SerializeField] protected LayerMask enemyLayer;

    public override IEnumerator ReleaseAttack()
    {
        StartCoroutine(enemyCtrl.EnemyMovement.ChangeVelocity(Vector2.zero));
        enemyCtrl.EnemyAnimation.ChangeAnimationState(EnemyAnimationState.StartShootLaser.ToString());
        enemyCtrl.EnemyAnimation.SetCanChangeAnim(false);

        Vector2 direction = playerDamageReceiver.Collider.bounds.center - shootPoint.position;

        yield return new WaitForSeconds(laserDelay);

        GameObject laser = BulletSpawner.Instance.Spawn(bulletCode.ToString(), shootPoint.position, Quaternion.identity);
        laser.transform.right = direction;
        laser.GetComponentInChildren<DespawnByTime>().SetAliveTime(laserDuration);
        laser.GetComponentInChildren<BulletImpact>().Setup(shooterLayer, enemyLayer, damage, -1);

        float originalDamage = enemyCtrl.EnemyDamageSender.Damage;
        enemyCtrl.EnemyDamageSender.Damage = damage;

        yield return new WaitForSeconds(laserDuration);

        enemyCtrl.EnemyDamageSender.Damage = originalDamage;

        yield return new WaitForSeconds(laserDelay);

        enemyCtrl.EnemyAnimation.SetCanChangeAnim(true);
    }
}
