using UnityEngine;

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
            turretMover.MoveTurret(currentTile);
            GridHelper.ClearHelpTiles();
            ChangeStates.ChangeStateNow(0);
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
