using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MyMonoBehaviour
{
    [SerializeField] protected BulletImpact bulletImpact;
    public BulletImpact BulletImpact { get => bulletImpact; }
    [SerializeField] protected BulletDamageSender bulletDamageSender;
    public BulletDamageSender BulletDamageSender { get => bulletDamageSender; }
    [SerializeField] protected BulletMovement bulletMovement;
    public BulletMovement BulletMovement { get => bulletMovement; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBulletImpact();
        this.LoadBulletSender();
        this.LoadBulletFly();
    }
    protected virtual void LoadBulletImpact()
    {
        if (this.bulletImpact != null) return;
        this.bulletImpact = transform.Find("Impact").GetComponent<BulletImpact>();
    }
    protected virtual void LoadBulletFly()
    {
        if (this.bulletMovement != null) return;
        this.bulletMovement = transform.Find("Movement").GetComponent<BulletMovement>();
    }
    protected virtual void LoadBulletSender()
    {
        if (this.bulletDamageSender != null) return;
        this.bulletDamageSender = transform.Find("DamageSender").GetComponent<BulletDamageSender>();
    }
}
