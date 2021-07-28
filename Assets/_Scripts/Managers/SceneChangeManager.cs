using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChangeManager : MonoBehaviour
{
    public delegate void SceneEvents();
    public static event SceneEvents OnMainSceneLoaded;

    public static SceneChangeManager instance = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

    }

    //Public method used to start async scene loading
    public void GoToScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }

    //Async scene loading, so there can be an animation within the loading screen
    private IEnumerator LoadScene(string sceneName)
    {       
        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync("LoadingScene");
        sceneLoading.allowSceneActivation = false;

        yield return new WaitWhile(() => sceneLoading.progress < 0.9f);

        sceneLoading.allowSceneActivation = true;

        AsyncOperation sceneLoading2 = SceneManager.LoadSceneAsync(sceneName);
        sceneLoading2.allowSceneActivation = false;

        yield return new WaitWhile(() => sceneLoading2.progress < 0.9f);

        sceneLoading2.allowSceneActivation = true;

        if (sceneName == "GameScene")
            OnMainSceneLoaded.Invoke();
    }
}
