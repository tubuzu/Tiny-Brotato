using System;
using UnityEngine.SceneManagement;

public static class Loader
{
    private static Action loaderCallbackAction;
    public static void Load(SceneName scene)
    {
        loaderCallbackAction = () =>
        {
            SceneManager.LoadScene(scene.ToString());
        };

        SceneManager.LoadScene(SceneName.LoadingScene.ToString());

    }

    public static void LoaderCallback()
    {
        if (loaderCallbackAction != null)
        {
            loaderCallbackAction();
            loaderCallbackAction = null;
        }
    }
}
