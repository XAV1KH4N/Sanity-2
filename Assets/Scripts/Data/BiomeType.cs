using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BiomeType
{
    SWAMP,
    PLAINS,
    FORREST,
    SNOW,
    ROCK,
    WATER,

    NONE
}

public class BiomeTypeUtils
{
    public static BiomeType getBiomeType(GroundType ground)
    {
        switch (ground)
        {
            case GroundType.DEEP_WATER:
            case GroundType.SHALLOW_WATER:
                return BiomeType.WATER;

            case GroundType.ROCK:
                return BiomeType.ROCK;

            case GroundType.SNOW:
                return BiomeType.SNOW;

            case GroundType.SWAMP_GRASS:
            case GroundType.MUD:
                return BiomeType.SWAMP;
            
            case GroundType.MUDDY_SAND:
            case GroundType.DARK_GRASS:
                return BiomeType.FORREST;

            case GroundType.SAND:
            case GroundType.GROUND_GRASS:
                return BiomeType.PLAINS;

            default:
                return BiomeType.PLAINS;
        }
    }

    public static List<BiomeType> otherGroundBiomes(BiomeType biome)
    {
        List<BiomeType> biomes = new List<BiomeType> { BiomeType.SWAMP, BiomeType.PLAINS, BiomeType.FORREST};
        biomes.Remove(biome);
        return biomes;
    }
}
