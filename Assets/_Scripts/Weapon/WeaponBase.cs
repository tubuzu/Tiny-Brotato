using System.Collections;
using System.Linq;
using UnityEngine;

public abstract class WeaponBase : MyMonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;

    [Header("Weapon Base")]
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float fireTimer;

    public int curLevel = 0;
    public WeaponProfile weaponProfile;

    protected Collider2D[] colliders;

    protected Vector2 wantedWeaponDirection;
    protected Vector2 prevWeaponDirection;
    protected float elapsedTime = 0f;
    protected float delay360 = 0.07f;
    protected bool canAttack = true;

    protected EnemyDamageReceiver curEnemy;

    public int weaponIndex;

    protected virtual void Update()
    {
        if (!PlayerCtrl.Instance.PlayerWeapon.isActive) return;
        AutoAttackEnemy();
        AutoLookAtEnemy();
        if (fireTimer > 0)
            fireTimer -= Time.deltaTime;
    }

    protected virtual void AutoAttackEnemy()
    {
        float weaponRange = PlayerCtrl.Instance.PlayerWeapon.GetFireRange(attackRange);
        colliders = Physics2D.OverlapCircleAll(transform.position, weaponRange, enemyLayer);
        colliders = colliders.Where(collider => collider.CompareTag("DamageReceiver")).ToArray();

        if (colliders.Length == 0)
        {
            curEnemy = null;
            return;
        }
        else if (curEnemy != null && curEnemy.IsDead()) curEnemy = null;

        if (fireTimer <= 0 && canAttack)
        {
            if (curEnemy == null)
            {
                Collider2D randomEnemy = colliders[Random.Range(0, colliders.Length)];
                if (randomEnemy.TryGetComponent(out curEnemy) && !curEnemy.IsDead())
                {
                    wantedWeaponDirection = ((Vector2)(curEnemy.Collider.bounds.center - transform.position)).normalized;

                    if (prevWeaponDirection != wantedWeaponDirection)
                    {
                        elapsedTime = Time.deltaTime;
                        prevWeaponDirection = transform.right;
                    }

                    StartCoroutine(AttackCoroutine());
                }
            }
            else StartCoroutine(AttackCoroutine());
        }
        else
        {
            if (curEnemy != null)
            {
                wantedWeaponDirection = ((Vector2)(curEnemy.EnemyCtrl.transform.position - transform.position)).normalized;

                if (prevWeaponDirection != wantedWeaponDirection)
                {
                    elapsedTime = Time.deltaTime;
                    prevWeaponDirection = transform.right;
                }
            }
        }
    }

    protected virtual void AutoLookAtEnemy()
    {
        if (colliders.Length == 0)
        {
            Vector2 playerMoveDirection = PlayerCtrl.Instance.PlayerMovement.MoveDirection;
            if (playerMoveDirection != Vector2.zero)
            {
                LookAtDirection(playerMoveDirection);
            }
            return;
        }

        if (elapsedTime < delay360)
        {
            LookAtDirection(Vector2.Lerp(prevWeaponDirection, wantedWeaponDirection, elapsedTime / delay360));
            elapsedTime += Time.deltaTime;
        }
        else LookAtDirection(wantedWeaponDirection);
    }

    protected void LookAtDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (direction.x >= 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        else
        {
            transform.rotation = Quaternion.Euler(180f, 0f, -angle);
        }
    }

    protected virtual IEnumerator AttackCoroutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(delay360);
        Attack();
        fireTimer = PlayerCtrl.Instance.PlayerWeapon.GetFireRate(fireRate);
        canAttack = true;
    }

    protected abstract void Attack();

    public virtual void SetWeaponProps(WeaponProfile profile, int level, int index)
    {
        curLevel = level;
        spriteRenderer.material = ResourceManager.Instance.weaponTierMaterials[level];

        weaponProfile = profile;
        weaponIndex = index;
        damage = profile.damage.value[level];
        attackRange = profile.range.value[level];
        fireRate = profile.fireRate.value[level];
    }

    public virtual void IncreaseLevel()
    {
        curLevel++;
        damage = weaponProfile.damage.value[curLevel];
        attackRange = weaponProfile.range.value[curLevel];
        fireRate = weaponProfile.fireRate.value[curLevel];
        spriteRenderer.material = ResourceManager.Instance.weaponTierMaterials[curLevel];
    }
}
