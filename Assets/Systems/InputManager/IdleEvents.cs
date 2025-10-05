using UnityEngine;

public static class IdleLogic
{
    public static event System.Action OnIdleClick;
    //public static event System.Action<Vector3Int> OnActionGridChange;


    public static void TriggerActionIdle() => OnIdleClick?.Invoke();
    //public static void TriggerActionGridChange(Vector3Int mousePos) => OnActionGridChange?.Invoke(mousePos);

}

public static class IdleEvents
{

    public static void DetectHold()
    {
        IdleLogic.TriggerActionIdle();
    }

}
