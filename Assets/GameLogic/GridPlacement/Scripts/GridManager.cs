using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] public Tilemap towerTilemap;
    [SerializeField] public Tilemap helperTilemap;

    [SerializeField] TileBase correctTile;
    [SerializeField] TileBase wrongTile;

    public List<Vector3Int> availablePositions = new();
    public Dictionary<Vector3Int, GameObject> turretPositions = new();

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

        GridHelper.Initialize(this);
    }

    public void AddTurret(Vector3Int turretPos, GameObject turret)
    {
        turretPositions.Add(turretPos, turret);
        availablePositions.Remove(turretPos);
        SetSpriteLayer();
    }

    public void SetCorrectTile(Vector3Int tile)
    {
        helperTilemap.ClearAllTiles();
        helperTilemap.SetTile(tile, correctTile);
    }


    public void SetWrongTile(Vector3Int tile)
    {
        helperTilemap.ClearAllTiles();
        helperTilemap.SetTile(tile, wrongTile);
    }

    public void SetMergePossibility(Vector3Int tile)
    {
        helperTilemap.ClearAllTiles();
        helperTilemap.SetTile(tile, correctTile);
    }

    public void SetSpriteLayer()
    {
        foreach(var values in turretPositions.Values)
        {
            var val = values.gameObject.GetComponentInChildren<SpriteRenderer>();
            val.sortingOrder = -(int)values.gameObject.transform.position.y;
        }
    }
}