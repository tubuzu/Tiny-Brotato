using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponOptionsMenu : MyMonoBehaviour
{
    [SerializeField] Transform container;

    [SerializeField] Button cancelBtn;
    [SerializeField] Button recycleBtn;
    [SerializeField] Button upgradeBtn;

    [SerializeField] RectTransform rectTransform;

    WeaponUI weaponUI;

    public void Setup(WeaponUI weaponUI)
    {
        this.weaponUI = weaponUI;

        rectTransform.position = Input.mousePosition;

        if (PlayerCtrl.Instance.PlayerWeapon.WeaponCanBeUpgrade(weaponUI.weaponProfile, weaponUI.weaponLevel, weaponUI.weaponIndex)) upgradeBtn.gameObject.SetActive(true);
        else upgradeBtn.gameObject.SetActive(false);
    }

    public void Recycle()
    {
        PlayerCtrl.Instance.PlayerWeapon.RemoveWeaponAt(weaponUI.weaponIndex);
        PlayerCtrl.Instance.PlayerStatus.ChangeGemNum(weaponUI.weaponProfile.weaponPrice / 5);

        StoreUI.InvokeGemCountChangeEvent();
        StoreUI.Instance.popupManager.CloseCurrentPopup();
    }

    public void Upgrade()
    {
        PlayerCtrl.Instance.PlayerWeapon.UpgradeWeapon(weaponUI.weaponProfile, weaponUI.weaponLevel, weaponUI.weaponIndex);

        StoreUI.Instance.popupManager.CloseCurrentPopup();
    }
}
