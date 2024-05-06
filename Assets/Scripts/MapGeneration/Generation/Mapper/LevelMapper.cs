using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelMapper : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private Tile blankTile;

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

    public void printMap(float[,] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                float n = map[i, j];                //1 = RED, 0 = GREEN

                blankTile.color = chooseTile(n);
                tilemap.SetTile(new Vector3Int(i, j, 0), blankTile);
            }
        }
    }

    private Color chooseTile(float value)
    {
        if (value < deepWaterLevel) return color(45, 70, 175); // Deep Water
        else if (value < shallowWaterLevel) return color(80, 145, 230); // Shallow Water
        else if (value < sandLevel) return color(200, 200, 85); // Sand
        else if (value < groundLevel1) return color(85, 200, 125); // Ground 1
        else if (value < groundLevel2) return color(50, 150, 85); // Ground 2
        else return color(20, 120, 55); // Ground 3
    }

    private Color color(float r, float g, float b)
    {
        return new Color(r/255f, g/255f, b/255f);
    }

}
