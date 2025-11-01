using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData
{
    //public Scene currentScene;

    public List<Vector3Int> avaiblePositions;
    public Dictionary<Vector3Int, GameObject> turretPositions;

    public int currentWave;

    public bool isLastWave;
    public int enhancements;

    public int currentBlood;

    public int currentLives;

    public int currentMana;

    public PlayerData(Saver saver)
    {
        //currentScene = saver.currentScene;

        avaiblePositions = saver.avaiblePositions;
        turretPositions = saver.turretPositions;

        currentWave = saver.currentWave;

        isLastWave = saver.isLastWave;
        enhancements = saver.enhancements;

        currentBlood = saver.currentBlood;

        currentLives = saver.currentLives;

        currentMana = saver.currentMana;
    }

}
