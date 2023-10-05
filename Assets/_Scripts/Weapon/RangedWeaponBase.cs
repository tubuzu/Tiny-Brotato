using UnityEngine;

public class RangedWeaponBase : WeaponBase
{
    [Header("Gun Base")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private LayerMask shooterLayer;
    [SerializeField] protected float bulletSpeed = 10f;
    [SerializeField] protected EnumManager.BulletCode bulletCode;
    // [SerializeField] protected int numberOfBullets = 1;
    [SerializeField] protected float mutationAngle = 5f;

    [SerializeField] protected int enemiesCanDamage = 1;

    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = transform.Find("Model").GetComponent<Animator>();
    }

    protected override void Attack()
    {
        // animator.SetTrigger("Fire");
        // StartCoroutine(AudioManager.Instance.PoolPlayRandomSFX(fireAudio, Random.Range(0.04f, 0.1f)));
        Vector2 direction;
        if (curEnemy != null)
            direction = ((Vector2)(curEnemy.Collider.bounds.center - shootPoint.position)).normalized;
        else
        {
            direction = transform.right;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion quaternion = Quaternion.Euler(0, 0, angle + Random.Range(-mutationAngle, mutationAngle));
        BulletCtrl bullet = BulletSpawner.Instance.Spawn(bulletCode.ToString(), shootPoint.position, quaternion).GetComponent<BulletCtrl>();
        bullet.BulletImpact.Setup(shooterLayer, enemyLayer, damage, enemiesCanDamage);

        Vector2 moveDirection = new(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        bullet.BulletMovement.Move(moveDirection, bulletSpeed);

        animator.SetTrigger("Shoot");
    }

    // protected override void Attack(EnemyCtrl enemy)
    // {
    //     float initialAngle = -(numberOfBullets - 1) * angleBetweenBullets / 2f;
    //     for (int i = 0; i < numberOfBullets; i++)
    //     {
    //         float currentAngle = initialAngle + i * angleBetweenBullets;
    //         Quaternion bulletRotation = Quaternion.Euler(0f, 0f, currentAngle);

    //         BulletCtrl bullet = BulletSpawner.Instance.Spawn(bulletType.ToString(), shootPoint.position, transform.parent.rotation * bulletRotation).GetComponent<BulletCtrl>();
    //         bullet.SetShooter(shooter);

    //         if (bulletFlyRange > 0)
    //         {
    //             bullet.BulletFly.SetDistanceToDespawn(bulletFlyRange);
    //         }

    //         if (bulletFlyThroughEnemy) bullet.BulletImpact.FlyThroughEnemy = true;
    //     }
    // }
}
