using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TextPopupType 
{
    
}

public class TextPopupSpawner : Spawner
{
    private static TextPopupSpawner instance;
    public static TextPopupSpawner Instance { get => instance; }
    protected override void Awake()
    {
        base.Awake();
        if (Instance != null) Debug.LogError("Only 1 TextPopupSpawner allow to exist!");
        instance = this;
    }
}
