using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MyMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image itemImage;
    [SerializeField] Image tierBackground;
    [SerializeField] TextMeshProUGUI countTMP;

    public int count = 1;
    public ItemProfile itemProfile;

    public void Setup(ItemProfile profile, int count = 1)
    {
        itemProfile = profile;
        itemImage.sprite = profile.itemSprite;
        if (count > 1)
        {
            this.count = count;
            countTMP.gameObject.SetActive(true);
            countTMP.text = "x" + count.ToString();
        }
        else countTMP.gameObject.SetActive(false);
        tierBackground.color = UIManager.ColorList[(ColorName)itemProfile.itemLevel].color;
    }

    public void UpdateCount(int add)
    {
        count += add;
        countTMP.gameObject.SetActive(true);
        countTMP.text = "x" + count.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject tooltip = ToolTipManager.Instance.GetToolTip(ToolTipName.ItemInfo);
        tooltip.TryGetComponent(out ItemInfoPopup itemInfoPopup);
        if (itemInfoPopup != null)
            itemInfoPopup.Setup(itemProfile);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipManager.Instance.HideCurrentToolTip();
    }
}
