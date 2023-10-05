// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

public class TextPopupDespawnByTime : DespawnByTime
{
    protected override void Despawn()
    {
        TextPopupSpawner.Instance.Despawn(gameObject);
    }
}
