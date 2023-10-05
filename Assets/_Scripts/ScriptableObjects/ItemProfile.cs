using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEffect
{
    public EnumManager.PlayerProps key;
    public int value;
}

[CreateAssetMenu(fileName = "New Item", menuName = "Data System/Item")]
public class ItemProfile : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public int itemLevel;
    public EnumManager.ItemCode itemCode;
    public EnumManager.ItemType itemType;
    public List<ItemEffect> effects;
    [TextArea] public string specialInfo;
    public int itemPrice;
}
