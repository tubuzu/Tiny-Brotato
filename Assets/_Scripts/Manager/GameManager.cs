using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MyMonoBehaviour
{
    public static GameManager Instance;
    // public static GameManager Instance
    // {
    //     if (Instance == null)
    //     {
    //         Instance = FindObjectOfType(typeof(GameManager)).GetComponent<GameManager>();
    //     }
    //     return Instance;
    // }

    SaveData saveData;

    [Header("Waves")]
    public List<Wave> waves;
    [SerializeField] int currentWaveIndex = 0;
    public int CurrentWaveIndex { get => currentWaveIndex; set => currentWaveIndex = value; }

    WaitForSeconds waitForOneSec = new WaitForSeconds(1);

    public Action OnWaveStart;
    public Action OnWaveStop;
    [SerializeField] bool waveInProgress = false;
    public bool WaveInProgress => waveInProgress;

    public int leftTime;


    [Header("Start equiments")]
    [SerializeField] List<ItemProfile> startItems;
    [SerializeField] List<WeaponProfile> startWeapons;

    protected override void Awake()
    {
        Instance = this;

        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        saveData = SaveSystem.Load();
        // load items and weapons
        if (saveData.progressSaved)
        {
            currentWaveIndex = saveData.currentWave;
            PlayerCtrl.Instance.PlayerInventory.AddSavedItems(saveData.items);
            PlayerCtrl.Instance.PlayerWeapon.AddSavedWeapons(saveData.weapons);
            PlayerCtrl.Instance.PlayerStatus.Level = saveData.curLevel;
            PlayerCtrl.Instance.PlayerStatus.MaxEXP *= MathF.Pow(1.2f, saveData.curLevel);
            PlayerCtrl.Instance.PlayerStatus.ChangeCurrentEXP(saveData.curExp);
            PlayerCtrl.Instance.PlayerStatus.ChangeGemNum(saveData.curGem);
        }
        else
        {
            currentWaveIndex = 0;
            PlayerCtrl.Instance.PlayerInventory.AddItems(startItems);
            foreach (var weapon in startWeapons)
            {
                PlayerCtrl.Instance.PlayerWeapon.AddWeapon(weapon, 0);
            }
            PlayerCtrl.Instance.PlayerStatus.ChangeGemNum(0);
        }

        UIManager.Instance.SetLevelText(PlayerCtrl.Instance.PlayerStatus.Level);
        StoreUI.Instance.Initial();

        SpawnWave();
    }

    public void SpawnWave()
    {
        if (currentWaveIndex >= waves.Count) return;

        waveInProgress = true;
        OnWaveStart?.Invoke();

        // spawn enemy
        EnemySpawner.Instance.SpawnWave(waves[currentWaveIndex]);

        // ui update
        UIManager.Instance.ChangeWaveCount(currentWaveIndex);
        StartTimer(waves[currentWaveIndex].time);
    }

    public void StartTimer(int time)
    {
        StartCoroutine(TimerCoroutine(time));
    }

    public void WinWave()
    {
        StopAllCoroutines();
        waveInProgress = false;

        OnWaveStop?.Invoke();

        currentWaveIndex++;

        if (currentWaveIndex == waves.Count)
        {
            saveData.progressSaved = false;
            SaveSystem.Save(saveData);
            WinGame();
        }
        else
        {
            SaveGame();
            UIManager.Instance.WinWave();
        }
    }

    public void NextWave()
    {
        PlayerCtrl.Instance.PlayerDamageReceiver.Reborn();
        SpawnWave();
    }

    void WinGame()
    {
        UIManager.Instance.WinGame();
    }

    IEnumerator TimerCoroutine(int time)
    {
        leftTime = time;
        while (leftTime > 0)
        {
            UIManager.Instance.ChangeWaveTimer(leftTime);
            yield return waitForOneSec;
            leftTime--;
        }
        UIManager.Instance.ChangeWaveTimer(leftTime);
        yield return new WaitForSeconds(0.5f);
        WinWave();
    }

    public void GameOver()
    {
        StopAllCoroutines();

        waveInProgress = false;
        OnWaveStop?.Invoke();
        saveData.progressSaved = false;
        SaveSystem.Save(saveData);

        UIManager.Instance.LoseGame();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    // private void OnApplicationQuit()
    // {
    //     SaveGame();
    // }

    public void SaveGame()
    {
        if (currentWaveIndex == waves.Count) return;
        saveData.weapons = PlayerCtrl.Instance.PlayerWeapon.GetSaveWeapon();
        saveData.items = PlayerCtrl.Instance.PlayerInventory.GetSaveItem();
        saveData.currentWave = currentWaveIndex;
        saveData.progressSaved = true;
        saveData.curLevel = PlayerCtrl.Instance.PlayerStatus.Level;
        saveData.curExp = PlayerCtrl.Instance.PlayerStatus.CurrentEXP;
        saveData.curGem = PlayerCtrl.Instance.PlayerStatus.GemNum;
        SaveSystem.Save(saveData);
        Debug.Log("Saved");
    }
}
