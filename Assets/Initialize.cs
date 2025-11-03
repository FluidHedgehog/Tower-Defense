using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialize : MonoBehaviour
{

    void Start()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        if (ProgressManager.instance == null) Debug.LogWarning("Instance = null");

        ProgressManager.instance.currentSceneName = SceneManager.GetSceneByName("Menu").name;
    }

}
