// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class EnemyAbstract : MyMonoBehaviour
{
    [SerializeField] protected EnemyCtrl enemyCtrl;
    public EnemyCtrl EnemyCtrl { get { return enemyCtrl; } }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadEnemyCtrl();
    }
    protected virtual void LoadEnemyCtrl()
    {
        if (enemyCtrl != null) return;
        Transform curTransform = transform;
        while (!curTransform.TryGetComponent(out enemyCtrl)) curTransform = curTransform.parent;
    }
}
