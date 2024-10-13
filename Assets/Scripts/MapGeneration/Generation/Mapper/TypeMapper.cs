using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TypeMapper
{
    private TileLookupService tiles;
    private Tilemap groundLayer;
    private Tilemap collisionLayer;

    public TypeMapper(TileLookupService tiles, Tilemap groundLayer, Tilemap collisionLayer)
    {
        this.tiles = tiles;
        this.groundLayer = groundLayer;
        this.collisionLayer = collisionLayer;
    }

    public void drawMap(GroundType[,] types)
    {
        for(int i = 0; i < types.GetLength(0); i++)
        {
            for(int j = 0; j < types.GetLength(1); j++)
            {
                GroundType type = types[i, j];
                Tile tile = tiles.getTile(type);
                if (type == GroundType.DEEP_WATER) 
                    collisionLayer.SetTile(new Vector3Int(i, j, 0), tile);
                else 
                    groundLayer.SetTile(new Vector3Int(i, j, 0), tile);
            }
        }
    }
}
