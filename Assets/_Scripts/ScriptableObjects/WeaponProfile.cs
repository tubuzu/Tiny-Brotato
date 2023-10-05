using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponProperty
{
    public EnumManager.WeaponProps key;
    public List<float> value;
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Data System/Weapon")]
public class WeaponProfile : ScriptableObject
{
    public Sprite weaponSprite;
    public string weaponName;
    public EnumManager.WeaponCode weaponCode;
    public EnumManager.WeaponType weaponType;
    public WeaponProperty damage;
    public WeaponProperty fireRate;
    public WeaponProperty range;
    public List<WeaponProperty> otherEffects;
    public int weaponPrice;
    [TextArea] public string specialInfo;
}
