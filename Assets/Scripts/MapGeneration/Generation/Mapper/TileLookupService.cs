using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using Random = System.Random;

public class TileLookupService : MonoBehaviour
{
    // Ground
    [SerializeField]
    private Tile groundTile1;
    
    [SerializeField]
    private Tile groundTile2;
    
    [SerializeField]
    private Tile groundTile3;
    
    [SerializeField]
    private Tile groundTile4;
    
    [SerializeField]
    private Tile groundForrestTile1;

    [SerializeField]
    private Tile groundSwampTile1;
    

    // Sand
    [SerializeField]
    private Tile sandTile1;
    
    [SerializeField]
    private Tile sandTile2;
    
    [SerializeField]
    private Tile sandTile3;
    
    [SerializeField]
    private Tile sandTile4;
    
    [SerializeField]
    private Tile sandyMudTile1;
    
    [SerializeField]
    private Tile mudTile1;

    // Rock
    [SerializeField]
    private Tile rockTile1;
    

    // Snow
    [SerializeField]
    private Tile snowTile1;
    
    // Water
    [SerializeField]
    private Tile waterTile1;
    
    [SerializeField]
    private Tile deepWaterTile1;
    
    // Debug
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

                // Grass 
            case GroundType.GROUND_GRASS:
                return groundTiles();
            
            case GroundType.DARK_GRASS:
                return new Tile[] { groundForrestTile1 };
            
            case GroundType.SWAMP_GRASS:
                return new Tile[] { groundSwampTile1};

                // Water 
            case GroundType.DEEP_WATER:
                return new Tile[] { deepWaterTile1 };

            case GroundType.SHALLOW_WATER:
                return new Tile[] { waterTile1 };

                // Sand
            case GroundType.SAND:
                return sandTiles();
            
            case GroundType.MUDDY_SAND:
                return new Tile[] { sandyMudTile1 };
                
            case GroundType.MUD:
                return new Tile[] { mudTile1 };
            
            case GroundType.DEBUG_TILE:
                return new Tile[] { debugTile };

            default:
                Debug.Log("Unknown Tile: " + type);
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
