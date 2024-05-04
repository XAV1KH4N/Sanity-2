using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldModule : MonoBehaviour
{
    [SerializeField]
    private Factory factory;

    [SerializeField]
    private Tilemap collision;

    void Start()
    {
        factory.createAt(new Vector3Int(2, 3, 0), collision, TileObjectDataType.ROUND_TREE);
        factory.createAt(new Vector3Int(-5, 3, 0), collision, TileObjectDataType.TALL_TREE);
        factory.createAt(new Vector3Int(-1, 1, 0), collision, TileObjectDataType.POINTY_TREE);
    }
}
