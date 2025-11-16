using UnityEngine;

public static class IdleEvents
{

    static GameObject currentRange;
    static GameObject currentTurret;
#nullable enable

    public static void OnPoint(Vector2 mousePos)
    {
        GameObject? tower = GridHelper.DetectTower(mousePos);
        if (tower == null)
        {
            if (currentRange == null) return;
            if (currentRange.activeSelf)
            {
                currentRange.SetActive(false);
            }
            return;
        }

        if (currentTurret != null && tower != currentTurret)
        {
            currentTurret.gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        }

        currentTurret = tower;
        currentRange = tower.gameObject.transform.GetChild(1).GetChild(0).gameObject; 
        currentRange.SetActive(true);
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
            TurretMerger.turretPos = GridHelper.ChangeToTile(mousePos);

            ChangeStates.ChangeStateNow(2);
        }
    }

    public static void OnRelease(Vector2 mousePos)
    {

    }
}
