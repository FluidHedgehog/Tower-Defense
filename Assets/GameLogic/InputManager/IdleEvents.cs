using UnityEngine;

public static class IdleEvents
{
#nullable enable
    public static void OnPoint(Vector2 mousePos)
    {

    }

    public static void OnInteract()
    {

    }

    public static void OnHold(Vector2 mousePos)
    {
        GameObject? tower = GridHelper.DetectTower(mousePos);
        
        if (tower == null)
        {
            return;
        }
        else
        {
            TurretMerger.turret = tower;

            ChangeStates.ChangeStateNow(1);
        }
    }

    public static void OnRelease(Vector2 mousePos)
    {

    }
}
