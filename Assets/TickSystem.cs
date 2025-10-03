using UnityEngine;

public static class TickActions
{
    public static event System.Action OnTick;

    public static void TriggerTick() => OnTick?.Invoke();
}

public class TickSystem : MonoBehaviour
{

    [SerializeField] float tickInterval;
    [SerializeField] float timer;

    void Start()
    {
        GameConfig.tickInterval = tickInterval;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= tickInterval)
        {
            timer -= tickInterval;
            TickActions.TriggerTick();
        }
    }
}
