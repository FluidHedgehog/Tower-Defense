using UnityEngine;
using UnityEngine.UIElements;

public static class MoveTowerEvents
{
    static TurretMover turretMover;
    static bool canPlace;
    static bool canMerge;
    static Vector3Int currentTile;
    static Vector2 pos;

    public static void Initialize(TurretMover turret)
    {
        turretMover = turret;
    }

    public static void OnPoint(Vector2 mousePos)
    {
        pos = mousePos;
        currentTile = GridHelper.ChangeToTile(mousePos);

        canPlace = GridHelper.CheckTile(currentTile);

        if (TurretMerger.turret != null)
        {
            canMerge = TurretMerger.CanMerge(TurretMerger.turret, TurretMerger.target = GridHelper.DetectTower(mousePos));
            GridHelper.HoverMerge(currentTile);
        }
    }

    public static void OnInteract()
    {
        if (canPlace)
        {
            GridHelper.DestroyTower(TurretMerger.turretPos);
            if (turretMover == null)
            {
                Debug.LogWarning("No turretMover reference!");
            }
            //GridHelper.SetToWorld(currentTile, out Vector3 worldPos);
            turretMover.MoveTurret(currentTile);
            
            GridHelper.ClearHelpTiles();
            ChangeStates.ChangeStateNow(0);
        }
        else if (canMerge)
        {
            GridHelper.DestroyTower(TurretMerger.turretPos);

            var posi = GridHelper.ChangeToTile(pos);
            TurretMerger.MergeTowers(TurretMerger.turret, TurretMerger.target, posi);
            GridHelper.ClearHelpTiles();
        }
        else
        {
            return;
        }
    }

    public static void OnHold(Vector2 mousePos)
    {

    }

    public static void OnRelease(Vector2 mousePos)
    {

    }


}
