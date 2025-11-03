using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public GameObject[] levelButtons;

    private void Start()
    {
        for (int i = 0; i <= ProgressManager.instance.saveData.unlockedLevels.Count - 1; i++)
        {
            levelButtons[i].gameObject.SetActive(true);
        }
    }
}
