using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelMapper : MonoBehaviour
{
    // World Levels
    [SerializeField, Range(0.0f, 1f)]
    protected float deepWaterLevel = 0.16f;

    [SerializeField, Range(0.0f, 1f)]
    protected float shallowWaterLevel = 0.32f;

    [SerializeField, Range(0.0f, 1f)]
    protected float sandLevel= 0.45f;

    [SerializeField, Range(0.0f, 1f)]
    protected float groundLevel = 0.85f;

    [SerializeField, Range(0.0f, 1f)]
    protected float rockLevel = 0.9f;
    
    [SerializeField, Range(0.0f, 1f)]
    protected float snowLevel = 1f; // Doesnt matter, will always be else

    // Biome Levels
    [SerializeField, Range(0.0f, 1f)]
    protected float plains = 0.45f;
    
    [SerializeField, Range(0.0f, 1f)]
    protected float forrest = 0.75f;
    
    [SerializeField, Range(0.0f, 1f)]
    protected float swamp = 0.1f;


    public GroundType[,] mapType(float[,] worldMap, float[,] biomeMap)
    {
        int width = worldMap.GetLength(0);
        int height = worldMap.GetLength(1);

        GroundType[,] groundTypes = new GroundType[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float groundValue = worldMap[i, j];
                float biomeValue = biomeMap[i, j];
                groundTypes[i, j] = getType(groundValue, biomeValue);
            }
        }
        return groundTypes;
    }

    private GroundType getType(float groundValue, float biomeValue)
    {
        if (groundValue < deepWaterLevel) return GroundType.DEEP_WATER; 
        else if (groundValue < shallowWaterLevel) return GroundType.SHALLOW_WATER; 
        else if (groundValue < sandLevel) return getSandType(biomeValue); 
        else if (groundValue < groundLevel) return getGrassType(biomeValue); 
        else if (groundValue < rockLevel) return GroundType.ROCK; 
        else return GroundType.SNOW; 
    }

    private GroundType getGrassType(float biomeValue)
    {
        if (biomeValue < plains) return GroundType.GROUND_GRASS;
        if (biomeValue < forrest) return GroundType.DARK_GRASS;
        else return GroundType.SWAMP_GRASS;
    }

    private GroundType getSandType(float biomeValue)
    {
        if (biomeValue < plains) return GroundType.SAND;
        if (biomeValue < forrest) return GroundType.MUDDY_SAND;
        else return GroundType.MUD;
    }
}