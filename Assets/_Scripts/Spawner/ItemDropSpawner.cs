// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class ItemDropSpawner : Spawner
{
    private static ItemDropSpawner instance;
    public static ItemDropSpawner Instance { get => instance; }
    protected override void Awake()
    {
        base.Awake();
        if (Instance != null) Debug.LogError("Only 1 ItemDropSpawner allow to exist!");
        instance = this;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        GameManager.Instance.OnWaveStart += OnGameStart;
    }

    protected virtual void OnDisable()
    {
        GameManager.Instance.OnWaveStart -= OnGameStart;
    }

    void OnGameStart()
    {
        DespawnAllActiveObject();
    }
}
