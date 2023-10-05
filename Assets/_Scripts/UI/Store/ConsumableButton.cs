using System;
using TMPro;
using UnityEngine;

public class ConsumableButton : MyMonoBehaviour
{
    public int gemNeeded = 0;
    [SerializeField] TextMeshProUGUI gemCountTMP;

    protected override void OnEnable()
    {
        base.OnEnable();
        StoreUI.GemCountChangeEvent += Refresh;
    }
    protected virtual void OnDisable()
    {
        StoreUI.GemCountChangeEvent -= Refresh;
    }

    public void Setup(int gemNeeded)
    {
        this.gemNeeded = gemNeeded;
        gemCountTMP.text = this.gemNeeded.ToString();
        if (this.gemNeeded <= PlayerCtrl.Instance.PlayerStatus.GemNum) gemCountTMP.color = UIManager.ColorList[ColorName.PositiveNumberColor].color;
        else gemCountTMP.color = UIManager.ColorList[ColorName.NegativeNumberColor].color;
    }

    public void Refresh()
    {
        if (this.gemNeeded <= PlayerCtrl.Instance.PlayerStatus.GemNum) gemCountTMP.color = UIManager.ColorList[ColorName.PositiveNumberColor].color;
        else gemCountTMP.color = UIManager.ColorList[ColorName.NegativeNumberColor].color;
    }

    public bool IsEnoughGem(int gemHaving) => gemHaving >= gemNeeded;
}
