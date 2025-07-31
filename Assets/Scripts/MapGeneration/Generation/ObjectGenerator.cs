using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectGenerator
{
    private RandomGenerator random;
    private RandomVectorGenerator randomVector;
    private ObjectGeneratorMetaData metaData;
    private Map map;
    private int[,] grid;

    public ObjectGenerator(RandomGenerator random, Map map, ObjectGeneratorMetaData metaData)
    {
        this.random = random;
        this.grid = new int[map.getWidth(), map.getHeight()];
        this.map = map;
        this.metaData = metaData;
        this.randomVector = new RandomVectorGenerator(random, map);
    }

    public FeatureData createFeatures() {
        FeatureData data = new FeatureData();
        withHouseData(data);
        withTreeData(data);
        return data;
    }

    private List<Vector2Int> sampleSomeChunks()
    {
        List<(Vector2Int, BiomeType)> chunks = sampleChunks();
        List<(Vector2Int, BiomeType)> randomChunks = random.shuffleList(chunks);
        return randomChunks.Select(x => x.Item1).Take(10).ToList();
    }

    private void withHouseData(FeatureData data)
    {
        foreach (Vector2Int mark in sampleSomeChunks())
        {
            List<Vector2Int> samples = populateWithTypeAnyBiome(metaData.houseMetaData, mark, TileObjectDataType.HOUSE);
            data.addSamples(TileObjectDataType.HOUSE, samples);
        }
    }

    private void withTreeData(FeatureData data)
    {
        List<(Vector2Int, BiomeType)> chunks = sampleChunks();

        for (int i = 0; i < chunks.Count; i++)
        {
            Vector2Int mark = chunks[i].Item1;
            BiomeType biome = chunks[i].Item2;

            (TileObjectDataType, List<Vector2Int>) samples = populateTreeData(mark, biome);
            Dictionary<Direction, BiomeType> neighbours = getNeighbours(chunks, i);
            addSamplesWithBlend(data, samples, neighbours, biome);
        }
    }

    private void addSamplesWithBlend(FeatureData data, (TileObjectDataType, List<Vector2Int>) samples, Dictionary<Direction, BiomeType> neighbours, BiomeType biome)
    {
        void blendFor(BiomeType b)
        {
            if (b != BiomeType.NONE && b != biome)
            {
                List<Vector2Int> trees = random.shuffleList(samples.Item2);
                int subsetSize = (int)Mathf.Floor(trees.Count * metaData.blend);
                List<Vector2Int> poached = trees.Take(subsetSize).ToList();

                foreach (Vector2Int v in poached)
                {
                    trees.Remove(v);
                }

                TileObjectDataType poachedType = treeTypeFor(b);
                data.addSamples(samples.Item1, trees);
                data.addSamples(poachedType, poached);
            }
        }

        foreach (Direction dir in Enum.GetValues(typeof(Direction)))
        {
            blendFor(neighbours[dir]);
        }
    }

    private TileObjectDataType treeTypeFor(BiomeType biome)
    {
        switch (biome)
        {
            case BiomeType.PLAINS:
                return TileObjectDataType.ROUND_TREE;

            case BiomeType.FORREST:
                return TileObjectDataType.TALL_TREE;

            case BiomeType.SWAMP:
                return TileObjectDataType.POINTY_TREE;

            default:
                return TileObjectDataType.NONE;
        }
    }

    private Dictionary<Direction, BiomeType> getNeighbours(List<(Vector2Int, BiomeType)> samples, int target)
    {
        BiomeType getBiome(int i) { return (i >= 0 && i < samples.Count) ? samples[i].Item2 : BiomeType.NONE; }

        int rowCount = map.getHeight() / Chunk.Height;
        int colCount = map.getWidth() / Chunk.Width;

        int row = target / colCount;
        int col = target % colCount;

        Dictionary<Direction, BiomeType> dict = new Dictionary<Direction, BiomeType>();

        int rn = row + 1;
        int cn = col;   
        int ixn = (rn*col) + cn;
        dict.Add(Direction.NORTH, getBiome(ixn));
        
        int re = row;
        int ce = col + 1;   
        int ixe = (re*col) + ce;
        dict.Add(Direction.EAST, getBiome(ixe));
        
        int rs = row - 1;
        int cs = col;   
        int ixs = (rs*col) + ce;
        dict.Add(Direction.SOUTH, getBiome(ixs));
        
        int rw = row;
        int cw = col - 1;   
        int ixw = (rw*col) + cw;
        dict.Add(Direction.WEST, getBiome(ixw));

        return dict;
    }


    private (TileObjectDataType, List<Vector2Int>) populateTreeData(Vector2Int point, BiomeType biome)
    {
        bool rand = random.randomInt(0, metaData.treeVariance) == 1;
        TileObjectDataType type = TileObjectDataType.TALL_TREE;
        List<Vector2Int> samples = new List<Vector2Int>();

        BiomeType finalBiome = rand ? random.choose(BiomeTypeUtils.otherGroundBiomes(biome)) : biome;

        switch (finalBiome)
        {
            case BiomeType.PLAINS:
                type = TileObjectDataType.ROUND_TREE;
                samples = populateWithType(metaData.roundTreeMetaData, point, type, BiomeType.PLAINS);
                break;

            case BiomeType.FORREST:
                type = TileObjectDataType.TALL_TREE;
                samples = populateWithType(metaData.tallTreeMetaData, point, type, BiomeType.FORREST);
                break;

            case BiomeType.SWAMP:
                type = TileObjectDataType.POINTY_TREE;
                samples = populateWithType(metaData.pointyTreeMetaData, point, type, BiomeType.SWAMP);
                break;

            default:
                break;
        }

        return (type, samples);
    }

    private List<Vector2Int> populateWithType(ObjectSpawnParams metaData, Vector2Int firstPoint, TileObjectDataType type, BiomeType biome)
    {
        RandomQueue processList = new RandomQueue(random);

        List<Vector2Int> samples = new List<Vector2Int>();

        processList.push(firstPoint);

        int count = 0;
        int MAX = 5;

        while (!processList.isEmpty() && count < MAX)
        {
            Vector2Int point = processList.pop();
            for (int i = 0; i < metaData.k_points; i++)
            {
                Vector2Int newPoint = randomVector.randomVector(point, metaData.min_dist, biome, 1);

                if (newPoint != Vector2Int.zero)
                {
                    if (inBounds(newPoint) && !near(newPoint, metaData.min_dist, type))
                    {
                        processList.push(newPoint);
                        samples.Add(newPoint);
                        addToGrid(grid, type, newPoint, metaData);
                    }
                }
            }
            count++;
        }
        return samples;
    }

    private List<Vector2Int> populateWithTypeAnyBiome(ObjectSpawnParams metaData, Vector2Int firstPoint, TileObjectDataType type)
    {
        RandomQueue processList = new RandomQueue(random);

        List<Vector2Int> samples = new List<Vector2Int>();

        processList.push(firstPoint);

        int count = 0;
        int MAX = 2000;

        while (!processList.isEmpty() && count < MAX)
        {
            Vector2Int point = processList.pop();
            for (int i = 0; i < metaData.k_points; i++)
            {
                Vector2Int newPoint = randomVector.randomVector(point, metaData.min_dist);

                if (newPoint != Vector2Int.zero)
                {
                    if (inBounds(newPoint) && !near(newPoint, metaData.min_dist, type))
                    {
                        processList.push(newPoint);
                        samples.Add(newPoint);
                        addToGrid(grid, type, newPoint, metaData);
                    }
                }
            }
            count++;
        }

        return samples;
    }

    private void addToGrid(int[,] grid, TileObjectDataType type, Vector2Int point, ObjectSpawnParams metaData)
    {
        int val = (int)type;
        grid[point.x, point.y] = val;

        return;
        for (int y = 0; y < metaData.getHeight(); y++)
        {
            for (int x = 0; x < metaData.getWidth(); x++)
            {
                int relativeX = point.x + x;
                int relativeY = point.y - y;

                if (relativeX >= 0 && relativeX < grid.GetLength(0) && relativeY >= 0 && relativeY < grid.GetLength(1))
                {
                    grid[relativeX, relativeY] = val;
                }
            }

        }
    }

    private bool near(Vector2Int new_point, int min_dist, TileObjectDataType type)
    {
        for (int i = -min_dist; i < min_dist; i++)
        {
            for (int j = -min_dist; j < min_dist; j++)
            {
                Vector2Int offset = new Vector2Int(i, j);
                bool isTaken = gridTaken(new_point + offset, type);
                float d = dist(new_point + offset, new_point);
                bool isMinDist = d < min_dist;
                if (isTaken && isMinDist)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private float dist(Vector2Int a, Vector2Int b)
    {
        return Vector2.Distance(a, b);
    }

    private bool gridTaken(Vector2Int p, TileObjectDataType type)
    {
        return inBounds(p) && grid[p.x, p.y] == (int)type;
    }

    private bool inBounds(Vector2Int p)
    {
        return p.x < map.getWidth() && p.x >= 0 && p.y < map.getHeight() && p.y >= 0;
    }

    private List<(Vector2Int, BiomeType)> sampleChunks()
    {
        List<(Vector2Int, BiomeType)> samples = new List<(Vector2Int, BiomeType)>();

        foreach (Vector2Int marker in map.getKeys())
        {
            GroundType ground = map.getGroundTypeAt(marker);
            BiomeType biome = BiomeTypeUtils.getBiomeType(ground);
            samples.Add((marker, biome));
        }

        return samples;
    }
}

class RandomQueue
{
    private RandomGenerator rand;

    public RandomQueue(RandomGenerator rand)
    {
        this.rand = rand;
    }

    protected List<Vector2Int> queue = new List<Vector2Int>();

    public void push(Vector2Int points)
    {
        queue.Add(points);
    }

    public Vector2Int pop()
    {
        int i = rand.randomInt(0, queue.Count);
        Vector2Int vect = queue[i];
        queue.RemoveAt(i);
        return vect;
    }

    public bool isEmpty()
    {
        return queue.Count == 0;
    }
}

class RandomVectorGenerator {
    private Map map;
    private RandomGenerator random;

    private List<Vector2Int> directions = new List<Vector2Int>
    {
        new Vector2Int(0, 1), // UP
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, 0), // LEFT
        new Vector2Int(1, 0), // RIGHT
        new Vector2Int(-1 ,1), // UP-LEFT
        new Vector2Int(1, 1), // UP-RIGHT
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(1, -1) // DOWN-RIGHT
    };

    public RandomVectorGenerator(RandomGenerator random, Map map)
    {
        this.map = map;
        this.random = random;
    }

    public Vector2Int randomVector(Vector2Int origin, int minDist, BiomeType biome, int var)
    {
        int dist = minDist + random.randomInt(0, var + 1);
        return randomVector(origin, dist, biome);
    }
    
    public Vector2Int randomVector(Vector2Int origin, int minDist, BiomeType biome)
    {
        int distance = random.randomInt(0, 2) + minDist;
        List<Vector2Int> availableDirections = directions
            .Select(d => getRelative(origin, d, distance))
            .Where(d => isSameBiome(d, biome))
            .ToList();

        Vector2Int direction = (availableDirections.Count == 0) ? Vector2Int.zero : random.choose(availableDirections);

        return direction;
    }

        public Vector2Int randomVector(Vector2Int origin, int minDist)
    {
        int distance = random.randomInt(0, 2) + minDist;
        List<Vector2Int> availableDirections = directions
            .Select(d => getRelative(origin, d, distance))
            .ToList();

        Vector2Int direction = (availableDirections.Count == 0) ? Vector2Int.zero : random.choose(availableDirections);

        return direction;
    }

    private bool isSameBiome(Vector2Int coord, BiomeType biome)
    {
        GroundType ground = map.getGroundTypeAt(coord);
        return BiomeTypeUtils.getBiomeType(ground) == biome;
    }

    private Vector2Int getRelative(Vector2Int origin, Vector2Int direction, int distance) { 
        Vector2 normalizedDirection = ((Vector2)direction).normalized;
        Vector2 unroundedVector = (Vector2)origin + (normalizedDirection * distance);
        Vector2Int newVector = Vector2Int.RoundToInt(unroundedVector);
        return newVector;
    }
}


enum Direction
{
    NORTH,
    EAST,
    SOUTH,
    WEST
}