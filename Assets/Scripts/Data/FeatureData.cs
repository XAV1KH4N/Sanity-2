using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FeatureData
{
    public List<Vector2Int> pointyTrees;
    private List<Vector2Int> roundTrees;
    public List<Vector2Int> tallTrees;
    
    public FeatureData(List<Vector2Int> pointyTrees,
        List<Vector2Int> roundTrees,
        List<Vector2Int> tallTrees)
    {
        this.pointyTrees = pointyTrees;
        this.roundTrees = roundTrees;
        this.tallTrees = tallTrees;
    }
    
    public FeatureData()
    {
        this.pointyTrees = new List<Vector2Int>();
        this.roundTrees = new List<Vector2Int>();
        this.tallTrees = new List<Vector2Int>();
    }

    public List<(Vector2Int, TileObjectDataType)> combined()
    {
        List<(Vector2Int, TileObjectDataType)> pointy = pointyTrees.Select(t => (t, TileObjectDataType.POINTY_TREE)).ToList();
        List<(Vector2Int, TileObjectDataType)> round= roundTrees.Select(t => (t, TileObjectDataType.ROUND_TREE)).ToList();
        List<(Vector2Int, TileObjectDataType)> tall = tallTrees.Select(t => (t, TileObjectDataType.TALL_TREE)).ToList();

        return pointy.Concat(round).Concat(tall).ToList();
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

            default:
                break;
        }
    }
}
