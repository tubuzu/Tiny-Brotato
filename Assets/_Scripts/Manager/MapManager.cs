using UnityEngine;

public class MapManager : MyMonoBehaviour
{
    public static MapManager Instance;
    public BoxCollider2D mapBounds;

    protected override void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

        base.Awake();
    }
}
