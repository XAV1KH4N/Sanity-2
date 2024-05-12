using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Timeline;

public class WorldModule : AppModule
{
    [SerializeField]
    private Factory factory;

    [SerializeField]
    private Tilemap ground;
    
    [SerializeField]
    private Tilemap collision;

    [SerializeField]
    private ColourMapper colourMapper;
    
    [SerializeField]
    private LevelMapper levelMapper;

    [SerializeField]
    private WorldGenerator generator;

    [SerializeField]
    private TileLookupService tileLookupService;
   
    private TypeMapper typeMapper;

    void Start()
    {
        typeMapper = new TypeMapper(tileLookupService, ground, collision);
        factory.OnObjectEntry += handleOnObjectEntry;
        createMap();
        testTiledObjects();
    }

    public void regenSeed()
    {
        generator.regenSeed();
        createMap();
    }

    public void createMap()
    {
        float[,] map = generator.createMap(); 
        GroundType[,] mapType = levelMapper.mapType(map);
        typeMapper.drawMap(mapType);
    }

    private void handleOnObjectEntry(Vector3Int coords, TileData data)
    {
        TileData dup = factory.getTileData(coords);
        Debug.Log("Player near: " + dup.getType());
    }

    private void testTiledObjects()
    {
        factory.createAt(new Vector3Int(2, 3, 0), collision, TileObjectDataType.ROUND_TREE);
        factory.createAt(new Vector3Int(-5, 3, 0), collision, TileObjectDataType.ROUND_TREE);
        factory.createAt(new Vector3Int(-1, 1, 0), collision, TileObjectDataType.ROUND_TREE);
    }
}
