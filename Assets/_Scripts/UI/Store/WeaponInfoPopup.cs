using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoPopup : BaseItemInfoPopup
{
    protected int level;

    public void Setup(WeaponProfile weaponProfile, int level)
    {
        foreach (Transform t in itemEffectsContainer.transform) Destroy(t.gameObject);

        itemImage.sprite = weaponProfile.weaponSprite;
        itemImage.SetNativeSize();
        itemName.text = weaponProfile.weaponName;
        itemType.text = weaponProfile.weaponType.ToString();
        itemDescription.text = weaponProfile.specialInfo;

        EffectUI effectUI = Instantiate(itemEffectUI, itemEffectsContainer.transform).GetComponent<EffectUI>();
        effectUI.Setup(EnumManager.WeaponProps2String[weaponProfile.damage.key], weaponProfile.damage.value[level], true);
        effectUI = Instantiate(itemEffectUI, itemEffectsContainer.transform).GetComponent<EffectUI>();
        effectUI.Setup(EnumManager.WeaponProps2String[weaponProfile.fireRate.key], weaponProfile.fireRate.value[level], true);
        effectUI = Instantiate(itemEffectUI, itemEffectsContainer.transform).GetComponent<EffectUI>();
        effectUI.Setup(EnumManager.WeaponProps2String[weaponProfile.range.key], weaponProfile.range.value[level], true);

        foreach (var effect in weaponProfile.otherEffects)
        {
            effectUI = Instantiate(itemEffectUI, itemEffectsContainer.transform).GetComponent<EffectUI>();
            effectUI.Setup(EnumManager.WeaponProps2String[effect.key], effect.value[level], true);
        }

        tierBackground.color = UIManager.ColorList[(ColorName)level].color;
    }
}
