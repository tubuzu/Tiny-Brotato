// using System.Collections.Generic;
// using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public StartMenuButtonName subMenu;
    public GameObject arrow;

    public void OnClick()
    {
        StartScene.Instance.MenuButtonClick(subMenu);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (arrow != null)
            arrow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (arrow != null)
            arrow.SetActive(false);
    }
}
