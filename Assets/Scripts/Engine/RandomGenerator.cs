using UnityEngine;
using System;
using Random = System.Random;
using System.Collections.Generic;
using System.Linq;

public class RandomGenerator 
{ 
    private int seed_hash;
    private Random rand;

    public RandomGenerator(string seed) : this(seed.GetHashCode()) { }

    public RandomGenerator() : this((int)Math.Round(UnityEngine.Random.Range(-100000f, 100000f))) { }

    public RandomGenerator(int seed_hash)
    {
        this.seed_hash = seed_hash;
        this.rand = new Random(seed_hash);
    }

    public float randomFloat(int min, int max)
    {
        return rand.Next(min * 100, max * 100) / 100f;
    }

    public int randomInt(int min, int max) // Inclusive lower, Exclusive upper
    {
        return rand.Next(min, max);
    }

    public int getSeedHash()
    {
        return seed_hash;
    }

    public Vector2Int randomVector(Vector2Int size)
    {
        int x = randomInt(0, size.x);
        int y = randomInt(0, size.y);
        return new Vector2Int(x, y);
    }

    public Vector2Int randomVectorAround(Vector2Int point, int min_dist)
    {
        float r1 = randomFloat(0, 1);
        float r2 = randomFloat(0, 1);
        float radius = min_dist * (r1 + 1);
        double angle = 2 * Math.PI * r2;
        int x = (int)Math.Round(point.x + radius * Math.Cos(angle));
        int y = (int)Math.Round(point.y + radius * Math.Sin(angle));
        return new Vector2Int(x, y);
    }

    public T choose<T>(List<T> lst)
    {
        int i = randomInt(0, lst.Count);
        return lst[i];
    }

    public List<T> shuffleList<T>(List<T> lst)
    {
        return lst.OrderBy(item => rand.Next(lst.Count * 100)).ToList();
    }
}

