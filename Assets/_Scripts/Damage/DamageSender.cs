using UnityEngine;

public class DamageSender : MyMonoBehaviour
{
    [SerializeField] protected float damage = 1;
    public float Damage { get => damage; set => damage = value; }
    public virtual bool Send(Transform obj, float finalDamage = -1)
    {
        CreateImpactFX();

        if (obj.TryGetComponent(out DamageReceiver damageReceiver))
        {
            if (finalDamage == -1) Send(damageReceiver, damage);
            else Send(damageReceiver, finalDamage);
            return true;
        }
        return false;
    }

    public virtual void Send(DamageReceiver damageReceiver, float finalDamage)
    {
        damageReceiver.Deduct(finalDamage);
    }

    protected virtual void CreateImpactFX()
    {
        string fxName = GetFXName();
        Vector3 hitPos = transform.position;
        Quaternion hitRot = transform.rotation;
        FXSpawner.Instance.Spawn(fxName, hitPos, hitRot);
    }
    protected virtual string GetFXName() => "";
    protected virtual void DestroyObject()
    {
        Destroy(transform.parent.gameObject);
    }
}
