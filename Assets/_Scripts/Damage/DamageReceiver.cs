using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class DamageReceiver : MyMonoBehaviour
{
    [Header("Damage Receiver")]
    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected float hp = 1;
    [SerializeField] protected float hpMax = 1;
    [SerializeField] protected bool isImmortal = false;
    [SerializeField] protected bool isDead = false;

    public float HP => hp;
    public float HPMax => hpMax;
    public Collider2D Collider => _collider;

    protected override void OnEnable()
    {
        base.OnEnable();
        Reborn();
    }
    protected override void Reset()
    {
        base.Reset();
        Reborn();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCollider();
    }
    protected virtual void LoadCollider()
    {
        if (_collider != null) return;
        if (!TryGetComponent(out _collider)) return;
        _collider.isTrigger = true;
    }
    public virtual void Reborn()
    {
        hp = hpMax;
        isDead = false;
    }
    public virtual void Add(float add)
    {
        if (hp == hpMax) return;
        hp += add;
        if (hp > hpMax) hp = hpMax;
        OnAdd(add);
    }
    public virtual void Deduct(float deduct)
    {
        if (isImmortal) return;
        if (hp <= 0) return;
        hp -= deduct;
        if (hp < 0)
        {
            hp = 0;
        }
        OnDeduct(deduct);
        if (IsDead() && !isDead)
        {
            isDead = true;
            OnDead();
        }
    }
    public virtual bool IsDead()
    {
        return hp <= 0;
    }
    protected virtual void OnAdd(float add)
    {

    }
    protected virtual void OnDeduct(float deduct)
    {

    }
    protected virtual void OnDead()
    {

    }
}
