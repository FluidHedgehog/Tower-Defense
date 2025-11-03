using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    #nullable enable

    [SerializeField] GameObject? options;

    public void OnStart()
    {
        SceneManager.UnloadSceneAsync("Menu");
        SceneManager.LoadScene("LevelMap", LoadSceneMode.Additive);
    }

    public void OnChooseLevel(string levelName)
    {
        SceneManager.UnloadSceneAsync("LevelMap");
        SceneManager.LoadScene(levelName, LoadSceneMode.Additive);

        var scene = SceneManager.GetSceneByName(levelName);
        ProgressManager.instance.currentSceneName = scene.name;
    }

    public void OnMenu()
    {
        SceneManager.UnloadSceneAsync("Won");
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);

        
        //SceneManager.UnloadSceneAsync("Lost");
    }

    public void OnOptions(bool isOptionsEnabled)
    {
        options?.SetActive(isOptionsEnabled);
    }

    public void OnExit()
    {
        Application.Quit();
    }

}
