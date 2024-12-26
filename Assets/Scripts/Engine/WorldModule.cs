using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
    }

    public void regenSeed()
    {
        generator.regenSeed();
        createMap();
    }

    public void createMap()
    {
        Debug.Log("Begin Map Generation");
        GroundType[,] groundTypes = createGround();
        
        initMap(groundTypes);
        initFactory();
        drawMap(groundTypes);
        
        Debug.Log("Complete");
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

    private void drawMap(GroundType[,] groundTypes)
    {
        List<Vector2Int> markers = map.getKeys();

        typeMapper.drawMap(groundTypes);
        //typeMapper.drawMarkers(markers);
    }

    private void initFactory()
    {
        factory.setMap(map);

        List<(Vector2Int, BiomeType)> sample = factory.sampleChunks();

        FeatureData data = createFeatureData(sample);
        data.combined().ForEach(a => factory.createAt(a.Item1, a.Item2));
    }

    private void initMap(GroundType[,] groundTypes)
    {
        map = new Map(groundTypes);
    }

    private FeatureData createFeatureData(List<(Vector2Int, BiomeType)> sample)
    {
        return generator.createFeatures(random, sample);
    }

    private GroundType[,] createGround()
    {
        float[,] noiseWorldMap = generator.createMap();

        generator.regenSeed();
        float[,] noiseBiomeMap = generator.createMap();

        GroundType[,] groundTypes = levelMapper.mapType(noiseWorldMap, noiseBiomeMap);
        return groundTypes;
    }

    private void testTiledObjects()
    {
        factory.createAt(new(2, 3), TileObjectDataType.ROUND_TREE);
        factory.createAt(new(15, 3), TileObjectDataType.POINTY_TREE);
        factory.createAt(new(6, 5), TileObjectDataType.TALL_TREE);
    }
}
