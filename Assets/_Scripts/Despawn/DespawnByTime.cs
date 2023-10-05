// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class DespawnByTime : MyMonoBehaviour
{
    [SerializeField] protected float aliveTime;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (aliveTime > 0)
            Invoke(nameof(Despawn), aliveTime);
    }

    protected virtual void Despawn()
    {
        Destroy(gameObject);
    }

    public virtual void SetAliveTime(float time)
    {
        aliveTime = time;
        CancelInvoke(nameof(Despawn));
        Invoke(nameof(Despawn), aliveTime);
    }
}
