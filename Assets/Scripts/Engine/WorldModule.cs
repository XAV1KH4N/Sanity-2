using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Timeline;

public class WorldModule : MonoBehaviour
{
    [SerializeField]
    private Factory factory;

    [SerializeField]
    private Tilemap collision;

    [SerializeField]
    private ColourMapper colourMapper;
    
    [SerializeField]
    private LevelMapper levelMapper;

    [SerializeField]
    private WorldGenerator generator;

    void Start()
    {
        createMap();
    }

    public void regenSeed()
    {
        Debug.Log("Regen seed");
        generator.regenSeed();
        createMap();
    }

    public void createMap()
    {
        Debug.Log("Creating map");
        float[,] map = generator.createMap();
        outputMap(map);
    }

    private void outputMap(float[,] map)
    {
        colourMapper.deleteMap(map);
        levelMapper.printMap(map);
    }

    private void testTiledObjects()
    {
        factory.createAt(new Vector3Int(2, 3, 0), collision, TileObjectDataType.ROUND_TREE);
        factory.createAt(new Vector3Int(-5, 3, 0), collision, TileObjectDataType.TALL_TREE);
        factory.createAt(new Vector3Int(-1, 1, 0), collision, TileObjectDataType.POINTY_TREE);
    }
}
