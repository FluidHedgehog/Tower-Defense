using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager instance;
    public SaveData saveData;
    public string currentSceneName { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            saveData = SaveSystem.Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CompleteLevel(string levelIndexName)
    {
        var levelIndex = SceneManager.GetSceneByName(levelIndexName).buildIndex;

        if (!saveData.completedLevels.Contains(levelIndex))
        {
            saveData.completedLevels.Add(levelIndex);
        }
        if (!saveData.unlockedLevels.Contains(levelIndex + 1))
        {
            saveData.unlockedLevels.Add(levelIndex + 1);
        }

        SaveSystem.Save(saveData);
    }
}
