using TMPro;
using UnityEngine;

public class EffectUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI effectNameTMP;
    [SerializeField] TextMeshProUGUI effectValueTMP;

    public void Setup(string effectName, float effectValue, bool showChange = false)
    {
        effectNameTMP.text = effectName;
        effectValueTMP.text = (showChange ? (effectValue > 0 ? "+" : (effectValue < 0) ? "-" : "") : "") + effectValue.ToString();
        effectValueTMP.color = effectValue > 0 ? UIManager.ColorList[ColorName.PositiveNumberColor].color : (effectValue == 0 ? UIManager.ColorList[ColorName.NormalColor].color : UIManager.ColorList[ColorName.NegativeNumberColor].color);
    }

    public void ChangeValue(float effectValue, bool showChange = false)
    {
        effectValueTMP.text = (showChange ? (effectValue > 0 ? "+" : (effectValue < 0) ? "-" : "") : "") + effectValue.ToString();
        effectValueTMP.color = effectValue > 0 ? UIManager.ColorList[ColorName.PositiveNumberColor].color : (effectValue == 0 ? UIManager.ColorList[ColorName.NormalColor].color : UIManager.ColorList[ColorName.NegativeNumberColor].color);
    }
}
