// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponUI : MyMonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] Image weaponImage;
    [SerializeField] Image tierBackground;
    public WeaponProfile weaponProfile;
    public int weaponLevel;

    public int weaponIndex;

    public void Setup(WeaponProfile weaponProfile, int weaponLevel, int weaponIndex)
    {
        this.weaponIndex = weaponIndex;
        this.weaponLevel = weaponLevel;
        this.weaponProfile = weaponProfile;
        weaponImage.sprite = weaponProfile.weaponSprite;
        tierBackground.color = UIManager.ColorList[(ColorName)weaponLevel].color;
    }

    public void UpdateLevel(int weaponLevel)
    {
        this.weaponLevel = weaponLevel;
        tierBackground.color = UIManager.ColorList[(ColorName)weaponLevel].color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StoreUI.Instance.popupManager.ShowWeaponOptionsPopup(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject tooltip = ToolTipManager.Instance.GetToolTip(ToolTipName.WeaponInfo);
        tooltip.TryGetComponent(out WeaponInfoPopup weaponInfoPopup);
        if (weaponInfoPopup != null)
            weaponInfoPopup.Setup(weaponProfile, weaponLevel);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipManager.Instance.HideCurrentToolTip();
    }
}
