using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData
{
    public List<int> unlockedLevels = new List<int>() { 2 };
    public List<int> completedLevels = new List<int>();
}
