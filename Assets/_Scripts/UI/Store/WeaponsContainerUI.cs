using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsContainerUI : MyMonoBehaviour
{

    [SerializeField] GameObject weaponUIPrefab;
    List<WeaponUI> weaponUIList = new();

    // public void Setup()
    // {
    //     LoadWeapons();
    // }

    // protected void LoadWeapons()
    // {
    //     int i = 0;
    //     foreach (var weapon in PlayerCtrl.Instance.PlayerWeapon.weapons)
    //     {
    //         GameObject weaponGO = Instantiate(weaponUIPrefab, transform);
    //         weaponUIList.Add(weaponGO.GetComponent<WeaponUI>());
    //         weaponUIList[^1].Setup(weapon.weaponProfile, weapon.curLevel, i);
    //         i++;
    //     }
    // }

    public void AddWeapon(WeaponProfile weapon, int level)
    {
        GameObject weaponGO = Instantiate(weaponUIPrefab, transform);
        weaponUIList.Add(weaponGO.GetComponent<WeaponUI>());
        weaponUIList[^1].Setup(weapon, level, weaponUIList.Count - 1);
    }

    public void UpgradeWeaponAt(int index)
    {
        weaponUIList[index].UpdateLevel(weaponUIList[index].weaponLevel + 1);
    }

    public void RemoveWeaponAt(int index)
    {
        GameObject removeWeapon = weaponUIList[index].gameObject;
        weaponUIList.RemoveAt(index);
        Destroy(removeWeapon);
        UpdateWeaponIndexes();
    }

    public void UpdateWeaponIndexes()
    {
        for (int i = 0; i < weaponUIList.Count; i++)
        {
            weaponUIList[i].weaponIndex = i;
        }
    }
}
