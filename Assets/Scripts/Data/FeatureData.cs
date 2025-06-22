using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FeatureData
{
    public List<Vector2Int> pointyTrees;
    public List<Vector2Int> roundTrees;
    public List<Vector2Int> tallTrees;
    public List<Vector2Int> houses;


    public FeatureData(List<Vector2Int> pointyTrees,
        List<Vector2Int> roundTrees,
        List<Vector2Int> tallTrees,
        List<Vector2Int> houses)
    {
        this.pointyTrees = pointyTrees;
        this.roundTrees = roundTrees;
        this.tallTrees = tallTrees;
        this.houses = houses;
    }

    public FeatureData()
    {
        this.pointyTrees = new List<Vector2Int>();
        this.roundTrees = new List<Vector2Int>();
        this.tallTrees = new List<Vector2Int>();
        this.houses = new List<Vector2Int>();
    }

    public List<(Vector2Int, TileObjectDataType)> combined()
    {
        List<(Vector2Int, TileObjectDataType)> house = houses.Select(t => (t, TileObjectDataType.HOUSE)).ToList();
        List<(Vector2Int, TileObjectDataType)> pointy = pointyTrees.Select(t => (t, TileObjectDataType.POINTY_TREE)).ToList();
        List<(Vector2Int, TileObjectDataType)> round = roundTrees.Select(t => (t, TileObjectDataType.ROUND_TREE)).ToList();
        List<(Vector2Int, TileObjectDataType)> tall = tallTrees.Select(t => (t, TileObjectDataType.TALL_TREE)).ToList();

        return house.Concat(pointy).Concat(round).Concat(tall).ToList();
    }

    public void addSamples(TileObjectDataType type, List<Vector2Int> samples)
    {
        switch (type)
        {
            case TileObjectDataType.TALL_TREE:
                tallTrees.AddRange(samples);
                break;

            case TileObjectDataType.POINTY_TREE:
                pointyTrees.AddRange(samples);
                break;

            case TileObjectDataType.ROUND_TREE:
                roundTrees.AddRange(samples);
                break;

            case TileObjectDataType.HOUSE:
                houses.AddRange(samples);
                break;

            default:
                break;
        }
    }
}
