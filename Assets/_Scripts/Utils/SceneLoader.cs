using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum SceneName
{
    LoadingScene,
    StartScene,
    GameScene,
}

public class SceneLoader : MyMonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;
    [SerializeField] private TextMeshProUGUI _percentText;
    private float _targetProgress;

    protected override void Awake()
    {
        base.Awake();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string sceneName)
    {
        // Application.LoadLevel("Loading");
        _targetProgress = 0;
        _progressBar.fillAmount = 0;

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        _loaderCanvas.SetActive(true);

        do
        {
            await Task.Delay(100);
            _targetProgress = scene.progress;
        } while (scene.progress < 0.9f);

        await Task.Delay(500);

        scene.allowSceneActivation = true;

        while (!scene.isDone)
        {
            await Task.Delay(100);
        }

        _loaderCanvas.SetActive(false);
    }

    private void FixedUpdate()
    {
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _targetProgress, 3 * Time.fixedDeltaTime);
        _percentText.text = Mathf.RoundToInt(_progressBar.fillAmount * 100).ToString() + "%";
    }
}
