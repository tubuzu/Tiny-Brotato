using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum StartMenuButtonName
{
    BackToMainMenu = 0,
    About = 1,
    Settings = 2,
    Quit = 3,
    Play = 4,
    Continue = 5,
}
public class StartScene : MonoBehaviour
{
    public static StartScene Instance;

    GameObject mainSub;
    GameObject settingsSub;
    GameObject aboutSub;

    public GameObject arrow;

    [SerializeField] Button continueButton;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        mainSub = transform.Find("MainSub").gameObject;
        settingsSub = transform.Find("SettingsSub").gameObject;
        aboutSub = transform.Find("AboutSub").gameObject;
        mainSub.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        settingsSub.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        aboutSub.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        SaveData saveData = SaveSystem.Load();
        if (saveData.progressSaved)
        {
            continueButton.gameObject.SetActive(true);
        }
        else continueButton.gameObject.SetActive(false);
    }
    private void Start()
    {
        // musicPlayer..PlayMusic(MusicSound.BackgroundSound);
        MenuButtonClick(StartMenuButtonName.BackToMainMenu);
    }
    public void MenuButtonClick(StartMenuButtonName name)
    {
        mainSub.SetActive(false);
        settingsSub.SetActive(false);
        aboutSub.SetActive(false);

        // AudioManager.Instance.PlayButtonClickSfx();
        switch (name)
        {
            case StartMenuButtonName.Play:
                SaveData saveData = SaveSystem.Load();
                if (saveData.progressSaved)
                {
                    saveData.progressSaved = false;
                    SaveSystem.Save(saveData);
                }
                SceneLoader.Instance.LoadScene(SceneName.GameScene.ToString());
                break;
            case StartMenuButtonName.Continue:
                SceneLoader.Instance.LoadScene(SceneName.GameScene.ToString());
                break;
            case StartMenuButtonName.BackToMainMenu:
                mainSub.SetActive(true);
                break;
            case StartMenuButtonName.Settings:
                settingsSub.SetActive(true);
                break;
            case StartMenuButtonName.About:
                aboutSub.SetActive(true);
                break;
            case StartMenuButtonName.Quit:
                Application.Quit();
                break;
        }
    }
}
