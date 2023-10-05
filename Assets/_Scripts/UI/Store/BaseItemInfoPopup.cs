using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseItemInfoPopup : MyMonoBehaviour
{
    [SerializeField] protected Image itemImage;
    [SerializeField] protected TextMeshProUGUI itemName;
    [SerializeField] protected TextMeshProUGUI itemType;
    [SerializeField] protected TextMeshProUGUI itemDescription;

    [SerializeField] protected GameObject itemEffectsContainer;
    [SerializeField] protected GameObject itemEffectUI;

    [SerializeField] protected Image tierBackground;
}
