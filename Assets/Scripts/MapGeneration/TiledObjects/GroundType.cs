using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundType
{
    DEEP_WATER,
    SHALLOW_WATER,
    SAND,
    GROUND_GRASS,
    ROCK,
    SNOW,

    SWAMP_GRASS,
    DARK_GRASS,

    MUDDY_SAND,
    MUD,

    DEBUG_TILE
}

public class GroundTypeUtils
{
    public static List<GroundType> grassTypes = new List<GroundType> { GroundType.GROUND_GRASS, GroundType.DARK_GRASS, GroundType.SWAMP_GRASS };
    public static List<GroundType> sandTypes = new List<GroundType> { GroundType.SAND, GroundType.MUDDY_SAND, GroundType.MUD};
    public static List<GroundType> waterTypes = new List<GroundType> { GroundType.SHALLOW_WATER, GroundType.DEEP_WATER };

    public static bool isWater(GroundType type)
    {
        return waterTypes.Contains(type);
    }

    public static bool isGround(GroundType type)
    {
        return grassTypes.Contains(type);
    }

    public static bool isSand(GroundType type)
    {
        return sandTypes.Contains(type);
    }
}