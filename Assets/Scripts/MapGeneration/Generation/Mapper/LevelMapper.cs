using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelMapper : MonoBehaviour
{
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

    public GroundType[,] mapType(float[,] map)
    {
        GroundType[,] groundTypes = new GroundType[map.GetLength(0), map.GetLength(1)];
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                float value = map[i, j];
                groundTypes[i, j] = getType(value);
            }
        }
        return groundTypes;
    }

    private GroundType getType(float value)
    {
        if (value < deepWaterLevel) return GroundType.DEEP_WATER; // Deep Water
        else if (value < shallowWaterLevel) return GroundType.SHALLOW_WATER; // Shallow Water
        else if (value < sandLevel) return GroundType.SAND; // Sand
        else if (value < groundLevel) return GroundType.GROUND_GRASS; // Ground 1
        else if (value < rockLevel) return GroundType.ROCK; // Ground 2
        else return GroundType.SNOW; // Ground 3
    }
}