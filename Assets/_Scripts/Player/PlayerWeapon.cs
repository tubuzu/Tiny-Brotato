using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : PlayerAbstract
{
    public List<WeaponBase> weapons = new();
    Transform[] weaponPositions = new Transform[ConstManager.MAX_WEAPON_CAN_EQUIP];

    public bool isActive = true;

    protected override void OnEnable()
    {
        base.OnEnable();
        GameManager.Instance.OnWaveStart += OnGameStart;
        GameManager.Instance.OnWaveStop += OnGameStop;
    }

    protected virtual void OnDisable()
    {
        GameManager.Instance.OnWaveStart -= OnGameStart;
        GameManager.Instance.OnWaveStop -= OnGameStop;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadWeaponPositions();
    }

    public void AddSavedWeapons(List<SaveWeapon> savedWeapons)
    {
        foreach (var weapon in savedWeapons) AddSavedWeapon(weapon);
    }

    public void AddSavedWeapon(SaveWeapon savedWeapon)
    {
        WeaponProfile loadedWeapon = Resources.Load<WeaponProfile>("ScriptableObjects/Weapon/" + savedWeapon.weaponCode.ToString());
        AddWeapon(loadedWeapon, savedWeapon.level);
    }

    public void AddWeapon(WeaponProfile weaponProfile, int weaponLevel)
    {
        if (weapons.Count < ConstManager.MAX_WEAPON_CAN_EQUIP)
        {
            GameObject loadedWeapon = Resources.Load<GameObject>("Prefabs/Weapon/" + weaponProfile.weaponCode.ToString());
            GameObject instantiatedWeapon = Instantiate(loadedWeapon, transform);
            WeaponBase weaponBase = instantiatedWeapon.GetComponent<WeaponBase>();
            weapons.Add(weaponBase);
            weaponBase.SetWeaponProps(weaponProfile, weaponLevel, weapons.Count - 1);
            StoreUI.Instance.weaponsContainerUI.AddWeapon(weaponProfile, weaponLevel);
            RefreshWeaponPosition();
            return;
        }
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].weaponProfile.weaponCode == weaponProfile.weaponCode && weapons[i].curLevel == weaponLevel)
            {
                UpgradeWeaponAt(i);
                RefreshWeaponPosition();
                return;
            }
        }
    }

    public bool CanAddWeapon(WeaponProfile weaponProfile, int level)
    {
        if (weapons.Count < ConstManager.MAX_WEAPON_CAN_EQUIP) return true;
        if (level < ConstManager.MAX_ITEM_LEVEL - 1 && FindWeapon(weaponProfile, level) != null) return true;
        return false;
    }

    WeaponBase FindWeapon(WeaponProfile weaponProfile, int level)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i].weaponProfile.weaponCode == weaponProfile.weaponCode && weapons[i].curLevel == level) return weapons[i];
        }
        return null;
    }

    public bool WeaponCanBeUpgrade(WeaponProfile weaponProfile, int weaponLevel, int weaponIndex)
    {
        if (weaponLevel >= ConstManager.MAX_ITEM_LEVEL - 1) return false;
        for (int i = 0; i < weapons.Count; i++)
        {
            if (i == weaponIndex) continue;
            if (weapons[i].weaponProfile.weaponCode == weaponProfile.weaponCode && weapons[i].curLevel == weaponLevel) return true;
        }
        return false;
    }

    public bool UpgradeWeapon(WeaponProfile weaponProfile, int weaponLevel, int weaponIndex)
    {
        if (weaponLevel >= ConstManager.MAX_ITEM_LEVEL - 1) return false;
        for (int i = 0; i < weapons.Count; i++)
        {
            if (i == weaponIndex) continue;
            if (weapons[i].weaponProfile.weaponCode == weaponProfile.weaponCode && weapons[i].curLevel == weaponLevel)
            {
                UpgradeWeaponAt(weaponIndex);
                RemoveWeaponAt(i);
                return true;
            }
        }
        return false;
    }

    public void RefreshWeaponPosition()
    {
        foreach (Transform t in weaponPositions) t.gameObject.SetActive(false);
        if (weapons.Count == 0) return;
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].transform.SetParent(weaponPositions[weapons.Count - 1].GetChild(i));
            weapons[i].transform.localPosition = Vector3.zero;
            weapons[i].weaponIndex = i;
        }
        weaponPositions[weapons.Count - 1].gameObject.SetActive(true);
    }

    public void RemoveWeaponAt(int weaponIndex)
    {
        GameObject removeWeapon = weapons[weaponIndex].gameObject;
        weapons.RemoveAt(weaponIndex);
        Destroy(removeWeapon);
        StoreUI.Instance.weaponsContainerUI.RemoveWeaponAt(weaponIndex);
        RefreshWeaponPosition();
    }

    public void UpgradeWeaponAt(int weaponIndex)
    {
        weapons[weaponIndex].IncreaseLevel();
        StoreUI.Instance.weaponsContainerUI.UpgradeWeaponAt(weaponIndex);
    }

    // public void RemoveWeapon(WeaponProfile weaponProfile, int level)
    // {
    //     for (int i = 0; i < weapons.Count; i++)
    //     {
    //         if (weapons[i].weaponProfile.weaponCode == weaponProfile.weaponCode && weapons[i].curLevel == level)
    //         {
    //             weapons.RemoveAt(i);
    //             return;
    //         }
    //     }
    // }

    protected void LoadWeaponPositions()
    {
        int idx = 0;
        foreach (Transform child in transform)
        {
            weaponPositions[idx] = child;
            idx++;
        }
    }

    public float GetFireRate(float fireRate)
    {
        return fireRate * (1 - (float)playerCtrl.PlayerStatus.AttackSpeed / 100);
    }

    public float GetFireRange(float fireRange)
    {
        return fireRange * (1 + (float)playerCtrl.PlayerStatus.AttackRangeMultiplier / 100);
    }

    void OnGameStart()
    {
        isActive = true;
    }
    void OnGameStop()
    {
        isActive = false;
    }

    public List<SaveWeapon> GetSaveWeapon()
    {
        List<SaveWeapon> result = new();
        foreach (var weapon in weapons)
        {
            result.Add(new SaveWeapon(weapon.weaponProfile.weaponCode, weapon.curLevel));
        }
        return result;
    }
}
