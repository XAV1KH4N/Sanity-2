using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeDataModel : TileObjectDataModel
{
    [SerializeField]
    private Tile[] tiles; 

    public Tile getTile(int x, int y)
    {
        return tiles[y * width + x];
    }
}