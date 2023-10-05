using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MyMonoBehaviour
{
    public static StoreUI Instance;

    public delegate void GemCountChangeHandler();
    public static event GemCountChangeHandler GemCountChangeEvent;
    public static void InvokeGemCountChangeEvent() => GemCountChangeEvent?.Invoke();

    [Header("Content")]
    [SerializeField] GameObject contentContainer;
    bool open = false;

    //header
    [SerializeField] TextMeshProUGUI gemCount;
    [SerializeField] ConsumableButton refreshStoreButton;
    [SerializeField] Sprite lockedSprite;
    [SerializeField] Sprite unlockedSprite;
    [SerializeField] Image lockImage;

    //body
    [SerializeField] GameObject playerStatusContainer;
    [SerializeField] GameObject statusTextPrefab;

    public ShopItemsContainerUI shopItemsContainerUI;

    public ItemsContainerUI itemsContainerUI;
    public WeaponsContainerUI weaponsContainerUI;

    public PopupManager popupManager;

    public Dictionary<EnumManager.PlayerProps, EffectUI> playerStatusUiDic = new();

    protected override void Awake()
    {
        if (Instance == null) Instance = this;
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        GemCountChangeEvent += OnGemCountChange;
    }

    protected virtual void OnDisable()
    {
        GemCountChangeEvent -= OnGemCountChange;
    }

    public void Initial()
    {
        itemsContainerUI.Setup();
        LoadPlayerStatus();
        shopItemsContainerUI.LoadData();
        Reload();
    }

    public void Reload()
    {
        LoadGemCount();
        shopItemsContainerUI.RefreshStore();
        refreshStoreButton.Setup(1 + (GameManager.Instance.CurrentWaveIndex - 1) * 2);
    }

    protected void LoadPlayerStatus()
    {
        int i = 0;
        foreach (KeyValuePair<EnumManager.PlayerProps, string> kvp in EnumManager.PlayerProps2String)
        {
            EffectUI effectUI = Instantiate(statusTextPrefab, playerStatusContainer.transform).GetComponent<EffectUI>();
            effectUI.Setup(kvp.Value, PlayerStatus.GetPlayerAttrDict[kvp.Key]());
            playerStatusUiDic.Add(kvp.Key, effectUI);
            i++;
        }
    }

    void OnGemCountChange()
    {
        LoadGemCount();
        refreshStoreButton.Refresh();
    }

    protected void LoadGemCount()
    {
        gemCount.text = PlayerCtrl.Instance.PlayerStatus.GemNum.ToString();
    }

    public void ToggleLockItems()
    {
        lockImage.sprite = shopItemsContainerUI.ToggleLockItems() ? lockedSprite : unlockedSprite;
    }

    public void RefreshStore()
    {
        if (!refreshStoreButton.IsEnoughGem(PlayerCtrl.Instance.PlayerStatus.GemNum)) return;
        StartCoroutine(shopItemsContainerUI.RefreshStoreCoroutine());
        PlayerCtrl.Instance.PlayerStatus.ChangeGemNum(-refreshStoreButton.gemNeeded);
        refreshStoreButton.Setup(refreshStoreButton.gemNeeded * 2);
        InvokeGemCountChangeEvent();
    }

    public void NextWave()
    {
        ToggleShow();
        GameManager.Instance.NextWave();
    }

    public void ToggleShow()
    {
        open = !open;
        contentContainer.SetActive(open);
        if (open) Reload();
    }

}
