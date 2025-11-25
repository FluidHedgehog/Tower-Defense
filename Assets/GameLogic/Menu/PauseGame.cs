using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject options;
    private int currentSpeed;

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            OnPause(true);
        }
    }

    public void OnStartGame()
    {
        Time.timeScale = 1f;
    }

    public void OnEnable()
    {
        Time.timeScale = 0f;
    }

    public void OnPause(bool isPausing)
    {
        Debug.Log(currentSpeed);
        switch (isPausing)
        {
            case true:
                startButton.SetActive(true);
                pauseButton.SetActive(false);
                options.SetActive(true);
                Time.timeScale = 0f;
                return;
            case false:
                startButton.SetActive(false);
                pauseButton.SetActive(true);
                options.SetActive(false);
                Time.timeScale = currentSpeed;
                return;
        }
    }

    public void OnSpeedUp(int speed)
    {
        Time.timeScale = speed;
        currentSpeed = speed;
        Debug.Log(currentSpeed);
    }

}
