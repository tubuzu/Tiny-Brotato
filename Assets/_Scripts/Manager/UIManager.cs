using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ColorName
{
    Tier0 = 0,
    Tier1 = 1,
    Tier2 = 2,
    Tier3 = 3,

    NormalColor = 100,
    PositiveNumberColor = 101,
    NegativeNumberColor = 102,
}

public class MyColor
{
    public readonly string hexCode;
    public readonly int alpha;
    public Color color;
    public MyColor(string hexCode = "#FFFFFF", int alpha = 255)
    {
        this.hexCode = hexCode;
        this.alpha = alpha;
    }
    public void Setup()
    {
        ColorUtility.TryParseHtmlString(hexCode, out color);
        color.a = alpha / 255f;
    }
}

public class UIManager : MyMonoBehaviour
{
    public static UIManager Instance;

    public static Dictionary<ColorName, MyColor> ColorList = new()
    {
        { ColorName.NormalColor, new MyColor() },
        { ColorName.PositiveNumberColor, new MyColor("#9FFF58") },
        { ColorName.NegativeNumberColor, new MyColor("#FF6161") },
        { ColorName.Tier0, new MyColor("#FFFFFF", 0) },
        { ColorName.Tier1, new MyColor("#3DBFFF", 60) },
        { ColorName.Tier2, new MyColor("#FF10E0", 80) },
        { ColorName.Tier3, new MyColor("#FF251A", 100) },
    };

    [SerializeField] BarUI hpBar;
    [SerializeField] BarUI hpDecreaseBG;
    [SerializeField] BarUI expBar;
    [SerializeField] TextMeshProUGUI gemCountTMP;
    [SerializeField] TextMeshProUGUI waveCountTMP;
    [SerializeField] TextMeshProUGUI waveTimerTMP;
    [SerializeField] TextMeshProUGUI waveCompleteTMP;
    [SerializeField] TextMeshProUGUI curlevelTMP;
    [SerializeField] TextMeshProUGUI levelUpTMP;

    [SerializeField] GameObject winGamePanel;
    [SerializeField] GameObject loseGamePanel;

    public StoreUI store;

    protected override void Awake()
    {
        base.Awake();
        if (Instance == null) Instance = this;

        foreach (KeyValuePair<ColorName, MyColor> kvp in ColorList)
        {
            kvp.Value.Setup();
        }
    }

    public void ChangeHpValue(float value)
    {
        hpBar.ChangeBarValue(value);
        hpDecreaseBG.ChangeBarValue(value);
    }
    public void ChangeExpValue(float value)
    {
        expBar.ChangeBarValue(value);
    }


    public void ChangeWaveCount(int index)
    {
        waveCountTMP.text = (index + 1).ToString();
    }
    public void ChangeWaveTimer(int time)
    {
        waveTimerTMP.text = time.ToString();
    }
    public void ChangeGemCount(int count)
    {
        gemCountTMP.text = count.ToString();
    }


    public void WinWave()
    {
        StartCoroutine(WinWaveCoroutine());
    }
    public void WinGame()
    {
        winGamePanel.SetActive(true);
    }
    public void LoseGame()
    {
        loseGamePanel.SetActive(true);
    }

    IEnumerator WinWaveCoroutine()
    {
        yield return ShowWaveCompleteText();

        ToggleStore();
    }

    public void ToggleStore()
    {
        StoreUI.Instance.ToggleShow();
    }

    IEnumerator ShowWaveCompleteText()
    {
        waveCompleteTMP.text = "";
        waveCompleteTMP.gameObject.SetActive(true);
        string text = "Wave Complete!";
        foreach (char c in text)
        {
            waveCompleteTMP.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(.7f);
        waveCompleteTMP.gameObject.SetActive(false);
    }

    public void ShowLevelUpText(int curLevel)
    {
        SetLevelText(curLevel);
        StartCoroutine(ShowLevelUpTextCoroutine());
    }
    public void SetLevelText(int curLevel)
    {
        curlevelTMP.text = "Lv " + curLevel.ToString();
    }
    IEnumerator ShowLevelUpTextCoroutine()
    {
        levelUpTMP.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        levelUpTMP.gameObject.SetActive(false);
    }
}
