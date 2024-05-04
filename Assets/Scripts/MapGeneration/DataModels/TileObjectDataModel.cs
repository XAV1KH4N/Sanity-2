using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileObjectDataModel : MonoBehaviour 
{
    [SerializeField]
    private Tile[] tiles;

    [SerializeField]
    protected TileObjectDataType type;

    [SerializeField]
    protected int width;
    
    [SerializeField]
    protected int height;

    public (int, int) getDimension()
    {
        return (width, height);
    }

    public Tile getTile(int x, int y)
    {
        return tiles[y * width + x];
    }
}