using UnityEngine;
using UnityEngine.UI;

public static class HealthSystemEvents
{
    public static event System.Action<int> OnHealthAdded;
    public static event System.Action<int> OnHealthRemoved;

    public static void TriggerHealthAdded(int val) => OnHealthAdded?.Invoke(val);
    public static void TriggerHealthRemoved(int val) => OnHealthRemoved?.Invoke(val);
}

public class HealthSystem : MonoBehaviour
{
    [SerializeField] Slider healthSlider;

    [SerializeField] int maxLives;

    int currentLives { get; set; }


    void ChangeValue()
    {
        healthSlider.value = currentLives;
    }


    void OnEnable()
    {
        HealthSystemEvents.OnHealthAdded += OnHealthAdded;
        HealthSystemEvents.OnHealthRemoved += OnHealthRemoved;
        healthSlider.maxValue = maxLives;
        currentLives = maxLives;
        ChangeValue();
    }

    void OnDisable()
    {
        HealthSystemEvents.OnHealthAdded -= OnHealthAdded;
        HealthSystemEvents.OnHealthRemoved -= OnHealthRemoved;
    }

    void OnHealthAdded(int val)
    {
        currentLives += val;
        ValidateHealth();
    }

    void OnHealthRemoved(int val)
    {
        currentLives -= val;
        ValidateHealth();
    }

    void ValidateHealth()
    {
        if (currentLives > maxLives)
        {
            currentLives = maxLives;
        }
        else if (currentLives <= 0)
        {
            GameOver();
        }

        ChangeValue();
    }
    
    void GameOver()
    {
        
    }
}
