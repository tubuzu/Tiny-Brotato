using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsContainerUI : MyMonoBehaviour
{
    [SerializeField] GameObject itemUIPrefab;
    List<ItemUI> itemUIList = new();

    public void Setup()
    {
        LoadItems();
    }

    protected void LoadItems()
    {
        // Debug.Log(PlayerCtrl.Instance.PlayerInventory.inventory.Count);
        foreach (var item in PlayerCtrl.Instance.PlayerInventory.inventory)
        {
            Debug.Log(item.Value.itemProfile.itemCode);
            UpdateItem(item.Value.itemProfile, item.Value.count);
        }
    }

    public void UpdateItem(ItemProfile item, int add)
    {
        ContainsItem(item.itemCode, out ItemUI itemUI);
        if (itemUI == null)
        {
            GameObject itemGO = Instantiate(itemUIPrefab, transform);
            itemUIList.Add(itemGO.GetComponent<ItemUI>());
            itemUIList[^1].Setup(item, add);
        }
        else itemUI.UpdateCount(add);
    }

    void ContainsItem(EnumManager.ItemCode itemType, out ItemUI itemUI)
    {
        itemUI = itemUIList.Where(x => x.itemProfile.itemCode == itemType).FirstOrDefault();
    }
}
