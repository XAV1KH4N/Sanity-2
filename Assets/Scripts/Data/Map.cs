using System;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    private GroundType[,] groundTypes;
    private int width;
    private int height;

    private ChunkManager chunks;

    public Map(GroundType[,] groundTypes)
    {
        this.groundTypes = groundTypes;
        this.width = groundTypes.GetLength(0);
        this.height = groundTypes.GetLength(1);
        this.chunks = new ChunkManager(width, height);
    }

    public List<Chunk> getChunks()
    {
        return chunks.getChunks();
    }

    public GroundType getGroundTypeAt(Vector2Int coords)
    {
        if (coords.x < 0 || coords.x >= width || coords.y < 0 || coords.y >= height) // out of bounds
            return GroundType.GROUND_GRASS;

        return groundTypes[coords.x, coords.y];
    }

    public void addTiledObject(Vector2Int coords, TileData data)
    {
        chunks.addObject(coords, data);
    }

    public TileData getTileData(Vector2Int coords)
    {
        if (isCoordTaken(coords)) return chunks.get(coords);
        return null;
    }

    public bool isCoordTaken(Vector2Int coords)
    {
        return chunks.contains(coords);
    }

    public List<Vector2Int> getKeys()
    {
        return chunks.getKeys();
    }

    public int getWidth()
    {
        return width;
    }

    public int getHeight()
    {
        return height;
    }
}
