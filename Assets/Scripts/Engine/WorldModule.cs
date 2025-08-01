using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        GroundType[,] groundTypes = createGround();
        
        initMap(groundTypes);
        initFactory();
        drawMap(groundTypes);
    }

    public GroundType getGroundTypeAt(Vector2Int coords)
    {
        return map.getGroundTypeAt(coords);
    }

    public List<Chunk> getSurroundingChunks(Vector2Int tileCoords)
    {
        return map.getSurroundingChunk(tileCoords);
    }

      public List<Chunk> getChunks()
    {
        return map.getChunks();
    }

    public Chunk getChunk(Vector2Int coods)
    {
        return map.getChunkAt(coods);
    }


    public void setTile(Vector2Int coords, GroundType type)
    {
        if (coords.x < 0 || coords.x >= map.getWidth() || coords.y < 0 || coords.y >= map.getHeight())
            return;

        map.setTile(type, coords);
        Tile tile = tileLookupService.getTile(type);
        ground.SetTile(to3D(coords), tile);
    }

    private Vector3Int to3D(Vector2Int coords) {
        return new Vector3Int(coords.x, coords.y, 0);
    }

    private void handleOnObjectEntry(Vector2Int coords, TileData data)
    {
        TileData dup = map.getTileData(coords);
        //Debug.Log("Player near: " + dup.getType());
    }

    private void drawMap(GroundType[,] groundTypes)
    {
        typeMapper.drawMap(groundTypes);
        //typeMapper.drawMarkers(map.getKeys());
    }

    private void initFactory()
    {
        factory.setMap(map);

        FeatureData data = createFeatureData(map);
        data.combined().ForEach(a => factory.createAt(a.Item1, a.Item2));
    }

    private void initMap(GroundType[,] groundTypes)
    {
        map = new Map(groundTypes);
    }

    private FeatureData createFeatureData(Map map)
    {
        return generator.createFeatures(random, map);
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
