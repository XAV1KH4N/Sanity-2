using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelMapper : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1f)]
    private float deepWaterLevel = 0.16f;

    [SerializeField, Range(0.0f, 1f)]
    private float shallowWaterLevel = 0.32f;

    [SerializeField, Range(0.0f, 1f)]
    private float sandLevel= 0.45f;

    [SerializeField, Range(0.0f, 1f)]
    private float groundLevel1 = 0.55f;

    [SerializeField, Range(0.0f, 1f)]
    private float groundLevel2 = 0.80f;
    
    [SerializeField, Range(0.0f, 1f)]
    private float groundLevel3 = 1f; // Doesnt matter, will always be else

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
        else if (value < groundLevel1) return GroundType.LOWER_GROUND_GRASS; // Ground 1
        else if (value < groundLevel2) return GroundType.MID_GROUND_GRASS; // Ground 2
        else return GroundType.HIGH_GROUND_GRASS; // Ground 3
    }
}
