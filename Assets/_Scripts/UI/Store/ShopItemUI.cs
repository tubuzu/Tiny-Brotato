using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItemUI : BaseShopItemUI
{
    public ItemProfile itemProfile;

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!StoreUI.Instance.shopItemsContainerUI.CanBuy(itemIndex, itemProfile.itemPrice)) return;
        PlayerCtrl.Instance.PlayerStatus.ChangeGemNum(-itemProfile.itemPrice);
        PlayerCtrl.Instance.PlayerInventory.AddItem(itemProfile);
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        StoreUI.Instance.itemsContainerUI.UpdateItem(itemProfile, 1);
        StoreUI.Instance.shopItemsContainerUI.PurchasedItem(itemIndex);
        if (StoreUI.Instance.shopItemsContainerUI.CheckPurchaseAll()) StartCoroutine(StoreUI.Instance.shopItemsContainerUI.RefreshStoreCoroutine());
        StoreUI.InvokeGemCountChangeEvent();
    }

    public void Setup(ItemProfile itemProfile, int itemIndex)
    {
        this.itemIndex = itemIndex;
        this.itemProfile = itemProfile;
        itemImage.sprite = itemProfile.itemSprite;
        itemImage.SetNativeSize();
        itemName.text = itemProfile.itemName;
        itemType.text = itemProfile.itemType.ToString();
        itemDescription.text = itemProfile.specialInfo;
        itemPrice.Setup(itemProfile.itemPrice);

        foreach (var effect in itemProfile.effects)
        {
            EffectUI effectUI = Instantiate(itemEffectUI, itemEffectsContainer.transform).GetComponent<EffectUI>();
            effectUI.Setup(EnumManager.PlayerProps2String[effect.key], effect.value, true);
        }

        tierBackground.color = UIManager.ColorList[(ColorName)itemProfile.itemLevel].color;
    }
}
