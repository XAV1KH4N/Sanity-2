using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpeedEvaluator
{
    private PlayerModule playerModule;
    private WorldModule worldModule;

    public GroundSpeedEvaluator(PlayerModule playerModule, WorldModule worldModule)
    {
        this.playerModule = playerModule;
        this.worldModule = worldModule;
    }

    public float ratio()
    {
        Vector2Int tilePos = playerModule.getPlayerLocation();
        GroundType type = worldModule.getGroundTypeAt(tilePos);

        switch(type) {
            case GroundType.SHALLOW_WATER:
                return 0.5f;

            case GroundType.SAND:
                return 0.8f;
            
            case GroundType.ROCK:
                return 1.15f;

            case GroundType.GROUND_GRASS:
            default:
                return 1f;
        }
    }
}
