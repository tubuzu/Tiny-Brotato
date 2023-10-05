using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbstract : MyMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl { get { return playerCtrl; } }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
    }
    protected virtual void LoadPlayerCtrl()
    {
        if (playerCtrl != null) return;
        Transform curTransform = transform;
        while (!curTransform.TryGetComponent(out playerCtrl)) curTransform = curTransform.parent;
    }
}
