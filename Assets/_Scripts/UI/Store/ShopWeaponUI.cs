using UnityEngine.EventSystems;

public class ShopWeaponUI : BaseShopItemUI
{
    public int weaponLevel = 0;
    public WeaponProfile weaponProfile;

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!StoreUI.Instance.shopItemsContainerUI.CanBuy(itemIndex, weaponProfile.weaponPrice)) return;
        if (!PlayerCtrl.Instance.PlayerWeapon.CanAddWeapon(weaponProfile, weaponLevel)) return;
        PlayerCtrl.Instance.PlayerStatus.ChangeGemNum(-weaponProfile.weaponPrice);
        PlayerCtrl.Instance.PlayerWeapon.AddWeapon(weaponProfile, weaponLevel);
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        StoreUI.Instance.shopItemsContainerUI.PurchasedItem(itemIndex);
        if (StoreUI.Instance.shopItemsContainerUI.CheckPurchaseAll()) StartCoroutine(StoreUI.Instance.shopItemsContainerUI.RefreshStoreCoroutine());
        StoreUI.InvokeGemCountChangeEvent();
    }

    public void Setup(WeaponProfile weaponProfile, int level, int itemIndex)
    {
        this.itemIndex = itemIndex;
        this.weaponProfile = weaponProfile;
        weaponLevel = level;
        itemImage.sprite = weaponProfile.weaponSprite;
        itemImage.SetNativeSize();
        itemName.text = weaponProfile.weaponName;
        itemType.text = weaponProfile.weaponType.ToString();
        itemDescription.text = weaponProfile.specialInfo;
        itemPrice.Setup((int)(weaponProfile.weaponPrice * (level + 1) + (level > 0 ? weaponProfile.weaponPrice * 0.25 : 0)));


        EffectUI effectUI = Instantiate(itemEffectUI, itemEffectsContainer.transform).GetComponent<EffectUI>();
        effectUI.Setup(EnumManager.WeaponProps2String[weaponProfile.damage.key], weaponProfile.damage.value[weaponLevel]);

        effectUI = Instantiate(itemEffectUI, itemEffectsContainer.transform).GetComponent<EffectUI>();
        effectUI.Setup(EnumManager.WeaponProps2String[weaponProfile.fireRate.key], weaponProfile.fireRate.value[weaponLevel]);

        effectUI = Instantiate(itemEffectUI, itemEffectsContainer.transform).GetComponent<EffectUI>();
        effectUI.Setup(EnumManager.WeaponProps2String[weaponProfile.range.key], weaponProfile.range.value[weaponLevel]);

        foreach (var effect in weaponProfile.otherEffects)
        {
            effectUI = Instantiate(itemEffectUI, itemEffectsContainer.transform).GetComponent<EffectUI>();
            effectUI.Setup(EnumManager.WeaponProps2String[effect.key], effect.value[weaponLevel]);
        }

        tierBackground.color = UIManager.ColorList[(ColorName)weaponLevel].color;
    }
}
