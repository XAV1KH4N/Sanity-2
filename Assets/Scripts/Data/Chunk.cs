using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public static int Width = 5;
    public static int Height = 5;

    private Vector2Int marker;

    private Dictionary<Vector2Int, TileData> chunks = new Dictionary<Vector2Int, TileData>();

    public Chunk(Vector2Int marker)
    {
        this.marker = marker;
    }

    public void add(Vector2Int coords, TileData data)
    {
        chunks.Add(coords, data);
    }

    public TileData get(Vector2Int coord)
    {
        return chunks[coord];
    }
    
    public bool contains(Vector2Int coord)
    {
        return chunks.ContainsKey(coord);
    }

    public List<Vector2Int> getAllCoords()
    {
        List<Vector2Int> coords = new List<Vector2Int>();
        for(int x = 0; x < Width; x++)
        {
            for(int y = 0; y < Height; y++)
            {
                Vector2Int offset = new Vector2Int(x, y);
                coords.Add(offset + marker);
            }
        }
        return coords;
    }

    public bool isCoordWithin(Vector2Int c)
    {
        return isXWithin(c.x) && isYWithin(c.y);
    }

    public Vector2Int getCoord()
    {
        return marker;
    }

    private bool isXWithin(int x)
    {
        return x >= marker.x && x < marker.x + Width;
    }
    
    private bool isYWithin(int y)
    {
        return y >= marker.y && y < marker.y + Height;
    }
}
