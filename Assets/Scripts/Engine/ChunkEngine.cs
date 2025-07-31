using System.Collections.Generic;
using UnityEngine;

public class ChunkEngine
{
    private PlayerModule playerModule;
    private WorldModule worldModule;
    public ChunkEngine(PlayerModule playerModule, WorldModule worldModule)
    {
        this.playerModule = playerModule;
        this.worldModule = worldModule;

        playerModule.OnPlayerMovement += onPlayerMovement;
    }

    private void onPlayerMovement()
    {
        Vector2Int tileCoords = playerModule.getPlayerLocation();
        List<Chunk> allChunks = worldModule.getChunks();
        foreach (Chunk chunk in allChunks)
        {
            worldModule.setTile(chunk.getCoord(), GroundType.SNOW);
        }

        List<Chunk> chunks = worldModule.getSurroundingChunks(tileCoords);
        foreach (Chunk chunk in chunks)
        {
            worldModule.setTile(chunk.getCoord(), GroundType.DEBUG_TILE);
        }

        Chunk thisChunk = worldModule.getChunk(tileCoords);
        worldModule.setTile(thisChunk.getCoord(), GroundType.SHALLOW_WATER);

        worldModule.setTile(new Vector2Int(0,0), GroundType.DARK_GRASS);
    }
}
