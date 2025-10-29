using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject startButton;

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

        switch (isPausing)
        {
            case true:
                startButton.SetActive(true);
                pauseButton.SetActive(false);
                Time.timeScale = 0f;
                return;
            case false:
                startButton.SetActive(false);
                pauseButton.SetActive(true);
                Time.timeScale = 1f;
                return;
        }
    }

    public void OnSpeedUp(int speed)
    {
        Time.timeScale = speed;
    }

}
