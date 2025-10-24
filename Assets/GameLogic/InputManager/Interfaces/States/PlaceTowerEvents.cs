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
            turretMover.PlaceTurret(currentTile);
            ChangeStates.ChangeStateNow(0);
            GridHelper.ClearHelpTiles();
        }
        else if (canMerge)
        {
            GridHelper.AlignToGrid(pos, out Vector3Int posi);
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
