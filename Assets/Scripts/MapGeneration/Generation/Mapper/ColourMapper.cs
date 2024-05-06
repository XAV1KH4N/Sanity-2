using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ColourMapper : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private Tile blankTile;

    private Gradient colourMapper = new Gradient();

    public void printMap(float[,] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                float n = map[i, j];                //1 = RED, 0 = GREEN

                blankTile.color = colourMapper.MapToGradient(n);
                tilemap.SetTile(new Vector3Int(i, j, 0), blankTile);
            }
        }
    }
    
    public void deleteMap(float[,] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                tilemap.SetTile(new Vector3Int(i, j, 0), null);
            }
        }
    }
}

class Gradient
{
    private static Tuple<float, Color>[] gradientStops = {
            Tuple.Create(0.0f, Color.red),
            Tuple.Create(0.25f, Color.yellow),
            Tuple.Create(0.5f, Color.green),
            Tuple.Create(0.75f, Color.blue),
            Tuple.Create(1.0f, Color.magenta)
        };


    private Color LerpColor(Color color1, Color color2, float t)
    {
        float r = (1 - t) * color1.r + t * color2.r;
        float g = (1 - t) * color1.g + t * color2.g;
        float b = (1 - t) * color1.b + t * color2.b;
        return new Color(r, g, b);
    }

    public Color MapToGradient(float value)
    {
        int n = gradientStops.Length;
        for (int i = 0; i < n - 1; i++)
        {
            if (gradientStops[i].Item1 <= value && value <= gradientStops[i + 1].Item1)
            {
                float t = (value - gradientStops[i].Item1) / (gradientStops[i + 1].Item1 - gradientStops[i].Item1);
                return LerpColor(gradientStops[i].Item2, gradientStops[i + 1].Item2, t);
            }
        }
        return Color.black;
    }
}