using System.Collections.Generic;
// using System.Linq;
using UnityEngine;

public class ResourceManager : MyMonoBehaviour
{
    public static ResourceManager Instance;

    public List<Material> weaponTierMaterials = new();

    protected override void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

        // LoadMaterials();

        base.Awake();
    }

    // protected virtual void LoadMaterials()
    // {
    //     weaponTierMaterials = Resources.LoadAll<Material>("Material/Weapon/").ToList();
    // }
}
