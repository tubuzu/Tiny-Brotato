using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class EnemyShootBullet : EnemyAbility
{
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected EnumManager.BulletCode bulletCode;
    [SerializeField] protected float bulletSpeed = 10f;
    [SerializeField] protected LayerMask shooterLayer;
    [SerializeField] protected LayerMask enemyLayer;

    public override IEnumerator ReleaseAttack()
    {
        Vector2 direction = playerDamageReceiver.Collider.bounds.center - shootPoint.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion quaternion = Quaternion.Euler(0, 0, angle);
        BulletCtrl bullet = BulletSpawner.Instance.Spawn(bulletCode.ToString(), shootPoint.position, quaternion).GetComponent<BulletCtrl>();
        bullet.BulletImpact.Setup(shooterLayer, enemyLayer, damage, 1);

        Vector2 moveDirection = new(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        bullet.BulletMovement.Move(moveDirection, bulletSpeed);

        yield break;
    }
}
