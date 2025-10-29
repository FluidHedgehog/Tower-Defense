using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    #nullable enable

    [SerializeField] GameObject? options;

    public void OnStart(string Level1)
    {
        SceneManager.LoadScene(Level1);
    }

    public void OnMenu()
    {
        SceneManager.LoadScene(0);
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
