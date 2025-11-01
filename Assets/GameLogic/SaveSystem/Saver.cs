using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Saver : MonoBehaviour
{
    #region REFERENCES  

    [SerializeField] GridManager gridManager;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] EnemyManager enemyManager;

    [SerializeField] BloodSystem bloodSystem;
    [SerializeField] HealthSystem healthSystem;
    [SerializeField] ManaSystem manaSystem;

    #endregion

    #region VARIABLES  

    //public Scene currentScene;

    public List<Vector3Int> avaiblePositions;
    public Dictionary<Vector3Int, GameObject> turretPositions;

    public int currentWave;

    public bool isLastWave;
    public int enhancements;

    public int currentBlood;

    public int currentLives;

    public int currentMana;

    #endregion

    #region METHODS

    public void SaveSaver()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadSaver()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        //currentScene = data.currentScene;

        avaiblePositions = data.avaiblePositions;
        turretPositions = data.turretPositions;

        currentWave = data.currentWave;

        isLastWave = data.isLastWave;
        enhancements = data.enhancements;

        currentBlood = data.currentBlood;

        currentLives = data.currentLives;

        currentMana = data.currentMana;

        ApplyValues();

    }

    public void ApplyValues()
    {
        gridManager.availablePositions = avaiblePositions;
        gridManager.turretPositions = turretPositions;

        //enemySpawner.currentWave = currentWave;

        enemyManager.isLastWave = isLastWave;
        enemyManager.enhancements = enhancements;

        bloodSystem.currentBlood = currentBlood;
        healthSystem.currentLives = currentLives;
        manaSystem.currentMana = currentMana;
    }

    #endregion 

}
