using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPopup : BaseItemInfoPopup
{
    public void Setup(ItemProfile itemProfile)
    {
        foreach (Transform t in itemEffectsContainer.transform) Destroy(t.gameObject);

        itemImage.sprite = itemProfile.itemSprite;
        itemImage.SetNativeSize();
        itemName.text = itemProfile.itemName;
        itemType.text = itemProfile.itemType.ToString();
        itemDescription.text = itemProfile.specialInfo;

        foreach (var effect in itemProfile.effects)
        {
            EffectUI effectUI = Instantiate(itemEffectUI, itemEffectsContainer.transform).GetComponent<EffectUI>();
            effectUI.Setup(EnumManager.PlayerProps2String[effect.key], effect.value, true);
        }

        tierBackground.color = UIManager.ColorList[(ColorName)itemProfile.itemLevel].color;
    }
}
