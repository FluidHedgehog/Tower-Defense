using UnityEngine;

public static class GridHelper
{
    static GridManager gridManager;

    public static void Initialize(GridManager grid)
    {
        gridManager = grid;
    }

    public static void AlignToGrid(Vector2 mousePos, out Vector3Int tilePos)
    {

        tilePos = gridManager.towerTilemap.WorldToCell(mousePos);

    }

    public static Vector3Int ChangeToTile(Vector2 mousePos)
    {
        AlignToGrid(mousePos, out Vector3Int tilePos);
        return tilePos;
    }

    public static bool CheckTile(Vector3Int tile)
    {
        if (gridManager.availablePositions.Contains(tile))
        {
            gridManager.SetCorrectTile(tile);
            return true;
        }
        else
        {
            gridManager.SetWrongTile(tile);
            return false;
        }
    }

    public static void HoverMerge(Vector3Int tile)
    {
        gridManager.SetMergePossibility(tile);
    }

    public static bool DetectPosition(Vector3Int tile)
    {
        if (gridManager.turretPositions[tile] != null)
        {
            return true;
        }
        else return false;
    }

    public static GameObject DetectTower(Vector2 mousePos)
    {
        gridManager.turretPositions.TryGetValue(ChangeToTile(mousePos), out GameObject turret);

        if (turret != null)
        {
            return turret;
        }
        else return null;
    }
}
