using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAbstract : MyMonoBehaviour
{
    [Header("Bullet Abstract")]
    [SerializeField] protected BulletCtrl bulletCtrl;
    public BulletCtrl BulletCtrl { get => bulletCtrl; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadBulletCtrl();
    }
    protected virtual void LoadBulletCtrl()
    {
        if (bulletCtrl != null) return;
        Transform curTransform = transform;
        while (!curTransform.TryGetComponent(out bulletCtrl)) curTransform = curTransform.parent;
    }
}
