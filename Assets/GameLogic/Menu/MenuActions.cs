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

    public void OnMenuWon()
    {
        SceneManager.UnloadSceneAsync("Won");
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }
    public void OnMenuLost()
    {
        SceneManager.UnloadSceneAsync("Lost");
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }

    public void OnOptions(bool isOptionsEnabled)
    {
        options?.SetActive(isOptionsEnabled);
    }

    public void OnCredits()
    {
        SceneManager.UnloadSceneAsync("Won");
        SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
    }

    public void OnExit()
    {
        Application.Quit();
    }

}
