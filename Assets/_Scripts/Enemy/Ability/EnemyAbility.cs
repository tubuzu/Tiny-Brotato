using System.Collections;
using UnityEngine;

public abstract class EnemyAbility : EnemyAbstract
{
    [SerializeField] protected float damage;
    [SerializeField] protected float range;

    public float Damage { get => damage; set => damage = value; }
    public float Range { get => range; set => range = value; }

    protected PlayerDamageReceiver playerDamageReceiver;

    protected override void Start()
    {
        base.Start();
        playerDamageReceiver = PlayerCtrl.Instance.PlayerDamageReceiver;
    }

    public abstract IEnumerator ReleaseAttack();

    public virtual void CancelAttack() => StopAllCoroutines();
}
