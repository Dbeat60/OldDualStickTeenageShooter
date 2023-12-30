using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{

    // Public members

    // Private members

    private static SceneMaster Instance;
    
    [SerializeField]
    private string titleSceneName= "TitleScene";
    [SerializeField]
    private string gameOverSceneName="GameOver";

    [SerializeField]
    private Camera sceneMasterCamera;
    [SerializeField]
    private ScreenFader screenFader;
    
    private bool loading;

    private bool IsSceneMasterCameraActive => sceneMasterCamera != null && sceneMasterCamera.enabled;

    private IEnumerator loadingCoroutine;
    private IEnumerator fadingCoroutine;

    // Initialization

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Trying to instance SceneMaster more than once");
            return;
        }

        Instance = this;
        loading = false;
        loadingCoroutine = null;
        fadingCoroutine = null;
    }

    private void Start()
    {
        GoToTitleScreen();
    }

    // Public methods

    public static void GoToTitleScreen()
    {
        Debug.Log("Go to title screen");
        Instance.StartLoading(Instance.MakeSingleScene(Instance.titleSceneName));
    }

    public static void UnloadScene(string sceneName)
    {
        Instance.StartLoading(Instance.RemoveSceneCoroutine(sceneName));
    }

    public static void UnloadCurrentScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        UnloadScene(sceneName);
    }

    public static void ChangeActiveScene(string sceneName)
    {
        Instance.StartLoading(Instance.ChangeActiveSceneCoroutine(sceneName));
    }

    public static void GoToGameOverScreen()
    {
        Instance.StartLoading(Instance.MakeSingleScene(Instance.gameOverSceneName));
    }

    // Private methods

    private void StartLoading(IEnumerator coroutine)
    {
        if (loading)
        {
            Debug.LogWarning("SceneMaster is busy");
            return;
        }

        if (loadingCoroutine != null)
        {
            StopCoroutine(loadingCoroutine);
            loadingCoroutine = null;
        }
        if (fadingCoroutine != null)
        {
            StopCoroutine(fadingCoroutine);
            fadingCoroutine = null;
        }

        loadingCoroutine = coroutine;
        StartCoroutine(loadingCoroutine);
    }

    private IEnumerator AddSceneCoroutine(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene == null)
        {
            Debug.LogError("Scene " + sceneName + " not found");
            yield break;
        }
        
        loading = true;

        fadingCoroutine = FadeIn();
        yield return StartCoroutine(fadingCoroutine);

        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        Scene sceneLoaded = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(sceneLoaded);
        SetMasterCameraEnable(SceneManager.sceneCount == 1);
        loading = false;
        
        fadingCoroutine = FadeOut();
        yield return StartCoroutine(fadingCoroutine);

        loadingCoroutine = null;
    }

    private IEnumerator MakeSingleScene(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene == null)
        {
            Debug.LogError("Scene " + sceneName + " not found");
            yield break;
        }

        loading = true;

        fadingCoroutine = FadeIn();
        yield return StartCoroutine(fadingCoroutine);

        SetMasterCameraEnable(true);
        while (SceneManager.sceneCount > 1)
        {
            scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            yield return SceneManager.UnloadSceneAsync(scene.buildIndex);
        }
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        Scene sceneLoaded = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(sceneLoaded);
        SetMasterCameraEnable(false);
        loading = false;

        fadingCoroutine = FadeOut();
        yield return StartCoroutine(fadingCoroutine);

        loadingCoroutine = null;
    }

    private IEnumerator RemoveSceneCoroutine(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene == null)
        {
            Debug.LogError("Scene " + sceneName + " not found to unload it");
            yield break;
        }

        loading = true;

        fadingCoroutine = FadeIn();
        yield return StartCoroutine(fadingCoroutine);

        SetMasterCameraEnable(SceneManager.sceneCount == 2);
        yield return SceneManager.UnloadSceneAsync(sceneName);
        loading = false;
        
        fadingCoroutine = FadeOut();
        yield return StartCoroutine(fadingCoroutine);

        loadingCoroutine = null;
    }

    private IEnumerator ChangeActiveSceneCoroutine(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene == null)
        {
            Debug.LogError("Scene " + sceneName + " not found");
            yield break;
        }

        loading = true;

        fadingCoroutine = FadeIn();
        yield return StartCoroutine(fadingCoroutine);

        SetMasterCameraEnable(true);
        Scene sceneToUnload = SceneManager.GetActiveScene();
        yield return SceneManager.UnloadSceneAsync(sceneToUnload.buildIndex);
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        Scene sceneLoaded = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(sceneLoaded);
        SetMasterCameraEnable(false);
        loading = false;

        fadingCoroutine = FadeOut();
        yield return StartCoroutine(fadingCoroutine);

        loadingCoroutine = null;
    }
    

    // Fade utils

    private IEnumerator FadeIn()
    {
        screenFader.FadeIn();
        while (screenFader.fading)
        {
            yield return null;
        }

        fadingCoroutine = null;
    }

    private IEnumerator FadeOut()
    {
        screenFader.FadeOut();
        while (screenFader.fading)
        {
            yield return null;
        }

        fadingCoroutine = null;
    }

    private void SetMasterCameraEnable(bool enable)
    {
        sceneMasterCamera.enabled = enable;
    }

}
