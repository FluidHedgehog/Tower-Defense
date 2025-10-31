using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public static class PlaceTowerEvents
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
        //var isPlaced =
        turretMover.PlaceTurret(currentTile);

        // if (isPlaced)
        // {
        //     GridHelper.DestroyTower(TurretMerger.turretPos);
        // }
        //GridHelper.SetToWorld(currentTile, out Vector3 worldPos);
        //ChangeStates.ChangeStateNow(0);


        // GridHelper.DestroyTower(TurretMerger.turretPos);
        // turretMover.PlaceTurret(currentTile);
        // ChangeStates.ChangeStateNow(0);
    }

    static void OnMergeTower()
    {
        var isMerged = TurretMerger.MergeTowers(TurretMerger.turret, TurretMerger.target, GridHelper.ChangeToTile(pos));
        
        if (isMerged)
        {
            GridHelper.DestroyTower(TurretMerger.turretPos);
        }
        //ChangeStates.ChangeStateNow(0);



        // GridHelper.DestroyTower(TurretMerger.turretPos);
        // //GridHelper.AlignToGrid(pos, out Vector3Int posi);
        // TurretMerger.MergeTowers(TurretMerger.turret, TurretMerger.target, GridHelper.ChangeToTile(pos));
        // ChangeStates.ChangeStateNow(0);
    }


}
