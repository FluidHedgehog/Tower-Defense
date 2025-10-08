using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField ]Tilemap towerTilemap;
    
    List<Vector3Int> availablePositions = new();

    void Start()
    {
        BoundsInt bounds = towerTilemap.cellBounds;
        foreach (var pos in bounds.allPositionsWithin)
        {
            TileBase tile = towerTilemap.GetTile(pos);
            if (tile != null)
            {
                availablePositions.Add(pos);
            }
        }
    }
}