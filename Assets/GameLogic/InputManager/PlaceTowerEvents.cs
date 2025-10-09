using UnityEngine;
using UnityEngine.InputSystem;

public static class PlaceTowerEvents
{
    static TurretMover turretMover;
    static bool canPlace;
    static Vector3Int currentTile;

    public static void Initialize(TurretMover turret)
    {
        turretMover = turret;
    }

    public static void OnPoint(Vector2 mousePos)
    {
        currentTile = ChangeToTile(mousePos);
        canPlace = GridHelper.CheckTile(currentTile);
    }

    public static void OnInteract()
    {
        if (canPlace)
        {
            turretMover.PlaceTurret(currentTile);
        }
        else
        {
            return;
        }
    }

    public static void OnHold()
    {

    }

    public static void OnRelease()
    {

    }

    static Vector3Int ChangeToTile(Vector2 mousePos)
    {
        GridHelper.AlignToGrid(mousePos, out Vector3Int tilePos);
        return tilePos;
    }

}
