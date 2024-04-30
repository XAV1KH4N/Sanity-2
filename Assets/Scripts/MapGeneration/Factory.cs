using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Factory : MonoBehaviour
{
    [SerializeField]
    private TreeDataModel tree;

    public void createAt(Vector2Int coords, Tilemap map, TileObjectDataType type)
    {
        createAt(new Vector3Int(coords.x, coords.y, 0), map, type);
    }


    public void createAt(Vector3Int coords, Tilemap map, TileObjectDataType type)
    {
        switch(type)
        {
            case TileObjectDataType.TREE:
                createTree(coords, map);
                break;

            default:
                break;
        }
    }

    private void createTree(Vector3Int coords, Tilemap map)
    {
        (int, int) dimensions = tree.getDimension();
        Debug.Log("Creating Tree " + dimensions);
        for (int y = 0; y < dimensions.Item2; y++)
        {
            for (int x = 0; x < dimensions.Item1; x++)
            {
                Tile tile = tree.getTile(x,y);
                Vector3Int relative = new Vector3Int(coords.x + x, coords.y - y);
                map.SetTile(relative, tile);
            }
        }
    }
}
