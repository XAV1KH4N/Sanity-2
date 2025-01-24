using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkManager
{
    private int width;
    private int height;
    private Dictionary<Vector2Int, Chunk> chunks = new Dictionary<Vector2Int, Chunk>();

    public ChunkManager(int w, int h)
    {
        this.width = w;
        this.height = h;
        createChunks();
    }

    public void addObject(Vector2Int coords, TileData data)
    {
        Chunk chunk = getChunkFor(coords);
        chunk.add(coords, data);
    }

    public bool contains(Vector2Int coord)
    {
        bool isXWithin = 0 <= coord.x && coord.x < width;
        bool isYWithin = 0 <= coord.y && coord.y < height;
        if (isXWithin && isYWithin)
        {
            return getChunkFor(coord).contains(coord);
        }
        else
        {
            return false;
        }
    }
    
    public TileData get(Vector2Int coord)
    {
        return getChunkFor(coord).get(coord);
    }

    public List<Vector2Int> getKeys()
    {
        return chunks.Keys.ToList();
    }

    private Chunk getChunkFor(Vector2Int coords)
    {
        int x = (coords.x / Chunk.Width) * Chunk.Width;
        int y = (coords.y / Chunk.Height) * Chunk.Height;
        return chunks[new Vector2Int(x, y)];
    }

    private void createChunks()
    {
        for (int x = 0; x < width; x+= Chunk.Width)
        {
            for (int y = 0; y < height; y+= Chunk.Height)
            {
                Vector2Int marker = new Vector2Int(x, y);
                Chunk chunk = new Chunk(marker);
                chunks.Add(marker, chunk);
            }
        }
    }
}
