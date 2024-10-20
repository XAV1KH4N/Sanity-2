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
    private Tilemap upperFeature;

    [SerializeField]
    private ColourMapper colourMapper;
    
    [SerializeField]
    private LevelMapper levelMapper;

    [SerializeField]
    private WorldGenerator generator;

    [SerializeField]
    private TileLookupService tileLookupService;

    public RandomGenerator random;

    private TypeMapper typeMapper;
    private Map map;
    
    void Start()
    {
        random = new RandomGenerator();
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
        float[,] noiseMap = generator.createMap();
        GroundType[,] groundTypes = levelMapper.mapType(noiseMap);
        FeatureData data = generator.createFeatures(random);

        map = new Map(groundTypes);
        typeMapper.drawMap(groundTypes);

        factory.setMap(map);
        data.combined().ForEach(a => factory.createAt(a.Item1, a.Item2));
    }

    public GroundType getGroundTypeAt(Vector2Int coords)
    {
        return map.getGroundTypeAt(coords);
    }

    private void handleOnObjectEntry(Vector2Int coords, TileData data)
    {
        TileData dup = map.getTileData(coords);
        Debug.Log("Player near: " + dup.getType());
    }

    private void testTiledObjects()
    {
        factory.createAt(new(2, 3), TileObjectDataType.ROUND_TREE);
        factory.createAt(new(-5, 3), TileObjectDataType.POINTY_TREE);
        factory.createAt(new(-1, 1), TileObjectDataType.TALL_TREE);
    }
}
