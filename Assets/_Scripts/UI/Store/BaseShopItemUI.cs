using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseShopItemUI : MyMonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected Image itemImage;
    [SerializeField] protected TextMeshProUGUI itemName;
    [SerializeField] protected TextMeshProUGUI itemType;
    [SerializeField] protected TextMeshProUGUI itemDescription;

    [SerializeField] protected ConsumableButton itemPrice;

    [SerializeField] protected GameObject itemEffectsContainer;
    [SerializeField] protected GameObject itemEffectUI;

    [SerializeField] protected Image tierBackground;

    [SerializeField] protected CanvasGroup canvasGroup;

    protected int itemIndex;

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
