using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Factory : MonoBehaviour
{
    [SerializeField]
    private TileObjectDataModel roundTree;
    
    [SerializeField]
    private TileObjectDataModel tallTree;
    
    [SerializeField]
    private TileObjectDataModel pointyTree;

    public void createAt(Vector2Int coords, Tilemap map, TileObjectDataType type)
    {
        createAt(new Vector3Int(coords.x, coords.y, 0), map, type);
    }


    public void createAt(Vector3Int coords, Tilemap map, TileObjectDataType type)
    {
        switch(type)
        {
            case TileObjectDataType.TALL_TREE:
                createTiledObject(coords, map, tallTree);
                break;
            
            case TileObjectDataType.POINTY_TREE:
                createTiledObject(coords, map, pointyTree);
                break;            
            
            case TileObjectDataType.ROUND_TREE:
                createTiledObject(coords, map, roundTree);
                break;

            default:
                break;
        }
    }

    private void createTiledObject(Vector3Int coords, Tilemap map, TileObjectDataModel model)
    {
        (int, int) dimensions = model.getDimension();
        for (int y = 0; y < dimensions.Item2; y++)
        {
            for (int x = 0; x < dimensions.Item1; x++)
            {
                Tile tile = model.getTile(x,y);
                Vector3Int relative = new Vector3Int(coords.x + x, coords.y - y);
                map.SetTile(relative, tile);
            }
        }
    }
}
