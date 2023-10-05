// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Spawner
{
    private static BulletSpawner instance;
    public static BulletSpawner Instance { get => instance; }
    protected override void Awake()
    {
        base.Awake();
        if (Instance != null) Debug.LogError("Only 1 BulletSpawner allow to exist!");
        instance = this;
    }
}
