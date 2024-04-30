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
        factory.createAt(new Vector3Int(3, 5, 0), collision, TileObjectDataType.TREE);
    }
}
