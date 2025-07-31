using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    [SerializeField]
    private PlayerModule playerModule;

    [SerializeField]
    private WorldModule worldModule;

    private ChunkEngine chunkEngine;

    void Start()
    {
        ChunkEngine chunkEngine = new ChunkEngine(playerModule, worldModule);
    }
}
