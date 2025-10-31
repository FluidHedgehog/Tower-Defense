using UnityEngine;
using UnityEngine.InputSystem;
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
        CheckTileAndPlacement(mousePos);

        if (TurretMerger.turret != null)
        {
            CheckMerge(mousePos);
        }
    }

    public static void OnInteract()
    {

    }

    public static void OnHold(Vector2 mousePos)
    {

    }

    public static void OnRelease(Vector2 mousePos)
    {
        ChangeStates.ChangeStateNow(0);
        GridHelper.ClearHelpTiles();
        if (canMerge)
        {
            OnMergeTower();
        }
        else if (canPlace)
        {
            OnPlaceTower();
        }
        

        return;
    }

    static void CheckTileAndPlacement(Vector2 mousePos)
    {
        pos = mousePos;
        currentTile = GridHelper.ChangeToTile(mousePos);

        canPlace = GridHelper.CheckTile(currentTile);
    }

    static void CheckMerge(Vector2 mousePos)
    {
        canMerge = TurretMerger.CanMerge(TurretMerger.turret, TurretMerger.target = GridHelper.DetectTower(mousePos));
        GridHelper.HoverMerge(currentTile);
    }

    static void OnPlaceTower()
    {
        var isPlaced = turretMover.MoveTurret(currentTile);

        if (isPlaced)
        {
            GridHelper.DestroyTower(TurretMerger.turretPos);
        }
        //GridHelper.SetToWorld(currentTile, out Vector3 worldPos);
        
    }

    static void OnMergeTower()
    {
        var isMerged = TurretMerger.MergeTowers(TurretMerger.turret, TurretMerger.target, GridHelper.ChangeToTile(pos));

        if (isMerged)
        {
            GridHelper.DestroyTower(TurretMerger.turretPos);
        }
        //ChangeStates.ChangeStateNow(0);
    }


}
