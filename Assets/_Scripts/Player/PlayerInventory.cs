using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : PlayerAbstract
{
    public Dictionary<EnumManager.ItemCode, (ItemProfile itemProfile, int count)> inventory = new();

    public void AddSavedItems(List<SaveItem> items)
    {
        foreach (var item in items) AddSavedItem(item);
    }
    public void AddItems(List<ItemProfile> itemProfiles)
    {
        foreach (var item in itemProfiles) AddItem(item);
    }

    public void AddSavedItem(SaveItem savedItem)
    {
        if (inventory.ContainsKey(savedItem.itemCode))
        {
            inventory[savedItem.itemCode] = (inventory[savedItem.itemCode].itemProfile, inventory[savedItem.itemCode].count + savedItem.count);
        }
        else
        {
            ItemProfile loadedItem = Resources.Load<ItemProfile>("ScriptableObjects/Item/" + savedItem.itemCode.ToString());
            inventory.Add(savedItem.itemCode, (loadedItem, savedItem.count));
        }
        foreach (ItemEffect itemEffect in inventory[savedItem.itemCode].itemProfile.effects)
        {
            PlayerStatus.ChangePlayerAttrFuncDict[itemEffect.key](itemEffect.value * savedItem.count);
        }
    }

    public void AddItem(ItemProfile itemProfile)
    {
        if (inventory.ContainsKey(itemProfile.itemCode))
        {
            inventory[itemProfile.itemCode] = (inventory[itemProfile.itemCode].itemProfile, inventory[itemProfile.itemCode].count + 1);
        }
        else
        {
            inventory.Add(itemProfile.itemCode, (itemProfile, 1));
        }
        foreach (ItemEffect itemEffect in itemProfile.effects)
        {
            PlayerStatus.ChangePlayerAttrFuncDict[itemEffect.key](itemEffect.value);
            StoreUI.Instance.playerStatusUiDic[itemEffect.key].ChangeValue(PlayerStatus.GetPlayerAttrDict[itemEffect.key]());
        }
    }

    public List<SaveItem> GetSaveItem()
    {
        List<SaveItem> result = new();
        foreach (var kvp in inventory)
        {
            result.Add(new SaveItem(kvp.Key, kvp.Value.count));
        }
        return result;
    }
}
