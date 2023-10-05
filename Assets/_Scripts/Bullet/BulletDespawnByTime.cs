using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawnByTime : DespawnByTime
{
    protected override void Despawn()
    {
        BulletSpawner.Instance.Despawn(transform.parent.gameObject);
    }
}
