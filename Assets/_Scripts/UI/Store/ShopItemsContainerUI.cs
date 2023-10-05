using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopItemsContainerUI : MyMonoBehaviour
{
    [SerializeField] GameObject shopItemUI;
    [SerializeField] GameObject shopWeaponUI;

    bool isLocked = false;
    bool[] purchasedItem = new bool[ConstManager.MAX_ITEM_IN_STORE];
    readonly List<List<float>> luckyByLevel = new()
    {
        new() {1f, 0f, 0f, 0f},
        new() {0.85f, 0.15f, 0f, 0f},
        new() {0.7f, 0.25f, 0.05f, 0f},
        new() {0.7f, 0.25f, 0.05f, 0f},
        new() {0.6f, 0.3f, 0.1f, 0f},
        new() {0.6f, 0.3f, 0.1f, 0f},
        new() {0.5f, 0.3f, 0.2f, 0f},
        new() {0.5f, 0.3f, 0.2f, 0f},
        new() {0.4f, 0.3f, 0.2f, 0.1f},
        new() {0.4f, 0.3f, 0.2f, 0.1f},
        new() {0.3f, 0.3f, 0.2f, 0.2f},
        new() {0.3f, 0.3f, 0.2f, 0.2f},
        new() {0.2f, 0.3f, 0.3f, 0.2f},
        new() {0.2f, 0.3f, 0.3f, 0.2f},
        new() {0.1f, 0.3f, 0.3f, 0.3f},
        new() {0.1f, 0.3f, 0.3f, 0.3f},
        new() {0f, 0.2f, 0.5f, 0.3f},
        new() {0f, 0.2f, 0.5f, 0.3f},
        new() {0f, 0.2f, 0.4f, 0.4f},
        new() {0f, 0.2f, 0.4f, 0.4f},
        new() {0f, 0.1f, 0.5f, 0.4f},
        new() {0f, 0.1f, 0.5f, 0.4f},
        new() {0f, 0f, 0.5f, 0.5f},
        new() {0f, 0f, 0.5f, 0.5f},
    };

    List<ItemProfile>[] itemsByLevel = new List<ItemProfile>[ConstManager.MAX_ITEM_LEVEL];
    List<WeaponProfile> allWeapons = new();

    BaseShopItemUI[] curItemsInStore = new BaseShopItemUI[ConstManager.MAX_ITEM_IN_STORE];
    List<BaseShopItemUI> itemsNeedToDelete = new();

    WaitForSeconds waitForRefresh = new WaitForSeconds(.2f);

    public void LoadData()
    {
        allWeapons = Resources.LoadAll<WeaponProfile>("ScriptableObjects/Weapon/").ToList();
        List<ItemProfile> allItems = Resources.LoadAll<ItemProfile>("ScriptableObjects/Item/").ToList();
        for (int i = 0; i < ConstManager.MAX_ITEM_LEVEL; i++) itemsByLevel[i] = new();
        foreach (var item in allItems)
        {
            itemsByLevel[item.itemLevel].Add(item);
        }
    }

    public bool CanBuy(int itemIndex, int price)
    {
        if (PlayerCtrl.Instance.PlayerStatus.GemNum < price || purchasedItem[itemIndex]) return false;
        return true;
    }

    public void PurchasedItem(int itemIndex) => purchasedItem[itemIndex] = true;

    public bool CheckPurchaseAll() => purchasedItem.All(x => x);

    public void RefreshStore()
    {
        for (int i = 0; i < ConstManager.MAX_ITEM_IN_STORE; i++)
        {
            if (isLocked && !purchasedItem[i]) continue;
            purchasedItem[i] = false;

            float randNum = (float)Random.Range(0, 101) / 100;
            // Debug.Log(randNum);
            int selectLevel = 0;
            float curRate = 0;
            for (int j = 0; j < ConstManager.MAX_ITEM_LEVEL; j++)
            {
                curRate += luckyByLevel[PlayerCtrl.Instance.PlayerStatus.Level - 1][j];
                if (randNum <= curRate)
                {
                    selectLevel = j;
                    break;
                }
            }

            int selectedIndex = Random.Range(0, itemsByLevel[selectLevel].Count + allWeapons.Count);

            if (selectedIndex < itemsByLevel[selectLevel].Count)
            {
                ShopItemUI itemUI = Instantiate(shopItemUI, transform).GetComponent<ShopItemUI>();
                itemUI.Setup(itemsByLevel[selectLevel][selectedIndex], i);
                itemUI.transform.SetSiblingIndex(i);
                if (curItemsInStore[i] != null) itemsNeedToDelete.Add(curItemsInStore[i]);
                curItemsInStore[i] = itemUI;
            }
            else
            {
                ShopWeaponUI weaponUI = Instantiate(shopWeaponUI, transform).GetComponent<ShopWeaponUI>();
                weaponUI.Setup(allWeapons[selectedIndex - itemsByLevel[selectLevel].Count], selectLevel, i);
                weaponUI.transform.SetSiblingIndex(i);
                if (curItemsInStore[i] != null) itemsNeedToDelete.Add(curItemsInStore[i]);
                curItemsInStore[i] = weaponUI;
            }
        }

        foreach (BaseShopItemUI item in itemsNeedToDelete) Destroy(item.gameObject);
        itemsNeedToDelete.Clear();
    }

    public IEnumerator RefreshStoreCoroutine()
    {
        yield return waitForRefresh;
        RefreshStore();
    }
    public bool ToggleLockItems()
    {
        isLocked = !isLocked;
        return isLocked;
    }
}
