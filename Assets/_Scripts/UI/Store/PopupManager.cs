using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] WeaponOptionsMenu weaponOptionsMenu;
    [SerializeField] GameObject background;

    GameObject currentPopup;

    public void ShowWeaponOptionsPopup(WeaponUI weaponUI)
    {
        weaponOptionsMenu.Setup(weaponUI);
        currentPopup = weaponOptionsMenu.gameObject;
        background.SetActive(true);
        currentPopup.SetActive(true);
    }

    public void CloseCurrentPopup()
    {
        currentPopup.SetActive(false);
        background.SetActive(false);
    }
}
