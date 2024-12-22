using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using Random = System.Random;

public class TileLookupService : MonoBehaviour
{
    [SerializeField]
    private Tile groundTile1;
    
    [SerializeField]
    private Tile groundTile2;
    
    [SerializeField]
    private Tile groundTile3;
    
    [SerializeField]
    private Tile groundTile4;
    
    [SerializeField]
    private Tile sandTile1;
    
    [SerializeField]
    private Tile sandTile2;
    
    [SerializeField]
    private Tile sandTile3;
    
    [SerializeField]
    private Tile sandTile4;

    [SerializeField]
    private Tile rockTile1;
    
    [SerializeField]
    private Tile snowTile1;
    
    [SerializeField]
    private Tile waterTile1;
    
    [SerializeField]
    private Tile deepWaterTile1;
    
    [SerializeField]
    private Tile blankTile;

    [SerializeField]
    private Tile debugTile;

    private static Random rand = new Random();

    private Tile[] groundTiles()
    {
        return new Tile[] { groundTile1, groundTile2, groundTile3, groundTile4 };
    }

    private Tile[] sandTiles()
    {
        return new Tile[] { sandTile1, sandTile2, sandTile3, sandTile4 };
    }

    private Tile[] getTileSelections(GroundType type)
    {
        switch (type)
        {
            case GroundType.SNOW:
                return new Tile[] { snowTile1 };

            case GroundType.ROCK:
                return new Tile[] { rockTile1 };

            case GroundType.GROUND_GRASS:
                return groundTiles();

            case GroundType.DEEP_WATER:
                return new Tile[] { deepWaterTile1 };

            case GroundType.SHALLOW_WATER:
                return new Tile[] { waterTile1 };

            case GroundType.SAND:
                return sandTiles();
            
            case GroundType.DEBUG_TILE:
                return new Tile[] { debugTile };

            default:
                return new Tile[] { blankTile };
        }
    }
    
    public Tile getTile(GroundType type)
    {
        Tile[] tiles = getTileSelections(type);
        int r = rand.Next(tiles.Length);
        return tiles[r];
    }
}
