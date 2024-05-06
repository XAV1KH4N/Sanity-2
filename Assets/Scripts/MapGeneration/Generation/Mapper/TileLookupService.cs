using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileLookupService : MonoBehaviour
{
    [SerializeField]
    private Tile groundTile1;
    
    [SerializeField]
    private Tile sandTile1;
    
    [SerializeField]
    private Tile waterTile1;
    
    [SerializeField]
    private Tile blankTile;

    public Tile getTile(TileType type)
    {
        switch (type)
        {
            case TileType.GRASS_TILE_1:
                return groundTile1;

            case TileType.WATER_TILE_1:
                return waterTile1;

            case TileType.SAND_TILE_1:
                return sandTile1;

            default:
                return blankTile;
        }
    }
    
    public Tile getTile(GroundType type)
    {
        switch (type)
        {
            case GroundType.LOWER_GROUND_GRASS:
            case GroundType.MID_GROUND_GRASS:
            case GroundType.HIGH_GROUND_GRASS:
                return groundTile1;

            case GroundType.DEEP_WATER:
            case GroundType.SHALLOW_WATER:
                return waterTile1;

            case GroundType.SAND:
                return sandTile1;

            default:
                return blankTile;
        }
    }
}
