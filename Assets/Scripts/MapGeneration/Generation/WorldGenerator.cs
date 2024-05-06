using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public class WorldGenerator : MonoBehaviour {
    // Outputs a 2d array of floats
    // Separate class for turing this data to 

    [SerializeField]
    private int mapWidth = 100;
    
    [SerializeField]
    private int mapHeight = 100;

    [SerializeField, Range(0.0f, 100f)]
    private float scale = 60; // high makes it smooth

    [SerializeField, Range(0.0f, 1f)]
    private float persistance = 0.5f;

    [SerializeField, Range(0.0f, 5f)]
    private float lacunarity = 1.75f; // 2 is perfect

    [SerializeField, Range(1, 10)]
    private int octaves = 3; // num of features

    [SerializeField]
    float offsetX = 0;// rand.Next(-10000, 10000);

    [SerializeField]
    float offsetY = 0;// rand.Next(-10000, 10000);

    public void regenSeed()
    {
        Random rand = new Random();
        offsetX = rand.Next(-10000, 10000);
        offsetY = rand.Next(-10000, 10000);
    }

    public float[,] createMap()
    {
        return createPerlinWithOctaves();
    }

    private float[,] createPerlinWithOctaves()
    {
        float[,] noise = new float[mapWidth, mapHeight];

        float max = float.NegativeInfinity;
        float min = float.PositiveInfinity;

        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                float amplitude = 1f;
                float frequency = 1f;
                float noise_height = 1f;

                for (int o = 0; o < octaves; o++)
                {
                    float sampleI = i / scale * frequency;
                    float sampleJ = j / scale * frequency;

                    float perlin = Mathf.PerlinNoise(sampleI + offsetX, sampleJ + offsetY);
                    noise_height += perlin * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                noise[i, j] = noise_height;

                max = Math.Max(noise[i, j], max);
                min = Math.Min(noise[i, j], min);
            }
        }

        return normalise(noise, min, max);
    }

    private float[,] normalise(float[,] noise, float max, float min)
    {
        float[,] normalized = new float[mapWidth, mapHeight];
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                normalized[i, j] = Mathf.InverseLerp(min, max, noise[i, j]);
            }
        }
        return normalized;
    }
}
