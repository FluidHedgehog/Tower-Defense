using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitCredits : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            SceneManager.UnloadSceneAsync("Credits");
            SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        }
    }
}