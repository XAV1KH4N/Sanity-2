using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Factory : MonoBehaviour
{
    [SerializeField]
    private TileObjectDataModel roundTree;
    
    [SerializeField]
    private TileObjectDataModel tallTree;
    
    [SerializeField]
    private TileObjectDataModel pointyTree;
    
    [SerializeField]
    private ColliderTemplateService colliderProvider;

    private Dictionary<Vector3Int, TileData> datamap = new Dictionary<Vector3Int, TileData>();

    public Action<Vector3Int, TileData> OnObjectEntry;

    public void createAt(Vector2Int coords, Tilemap map, Tilemap onTopFeature, TileObjectDataType type)
    {
        createAt(new Vector3Int(coords.x, coords.y, 0), map, onTopFeature, type);
    }

    public void createAt(Vector3Int coords, Tilemap map, Tilemap onTopFeature, TileObjectDataType type)
    {
        switch(type)
        {
            case TileObjectDataType.TALL_TREE:
                createTiledObject(coords, map, tallTree);
                addOnTop(coords, onTopFeature, tallTree);
                break;
            
            case TileObjectDataType.POINTY_TREE:
                createTiledObject(coords, map, pointyTree);
                addOnTop(coords, onTopFeature, pointyTree);
                break;            
            
            case TileObjectDataType.ROUND_TREE:
                createTiledObject(coords, map, roundTree);
                addOnTop(coords, onTopFeature, roundTree);
                break;

            default:
                break;
        }
    }

    private void addOnTop(Vector3Int coords, Tilemap featureMap, TileObjectDataModel model)
    {
        (int, int) dimensions = model.getDimension();
        for (int y = 0; y < 2; y++)
        {
            for (int x = 0; x < dimensions.Item1; x++)
            {
                Tile tile = model.getTile(x, y);
                Vector3Int relative = new Vector3Int(coords.x + x, coords.y - y);
                featureMap.SetTile(relative, tile);
            }
        }
    }

    private void createTiledObject(Vector3Int coords, Tilemap map, TileObjectDataModel model)
    {
        createCollider(coords, map, model);
        datamap.Add(coords, new TileData(model.getType()));
        (int, int) dimensions = model.getDimension();
        for (int y = 0; y < dimensions.Item2; y++)
        {
            for (int x = 0; x < dimensions.Item1; x++)
            {
                Tile tile = model.getTile(x,y);
                Vector3Int relative = new Vector3Int(coords.x + x, coords.y - y);
                map.SetTile(relative, tile);
            }
        }
    }

    private void createCollider(Vector3Int coords, Tilemap map, TileObjectDataModel model)
    {
        if (model.getType() != TileObjectDataType.ROUND_TREE) return;

        TiledObject obj = colliderProvider.createTiledObject(model.getType());
        initTileObject(coords, obj);
        Vector3 worldPos = findCenter(coords, map, model);
        obj.transform.position = worldPos;
    }

    private void initTileObject(Vector3Int coords, TiledObject tileObj)
    {
        tileObj.init(coords);
        tileObj.OnTrigger += handleObjectEntry;
    } 

    private void handleObjectEntry(Vector3Int coords)
    {
        OnObjectEntry?.Invoke(coords, datamap[coords]);
    }

    private Vector3 findCenter(Vector3Int coords, Tilemap map, TileObjectDataModel model)
    {
        Vector3 worldPos = map.GetCellCenterWorld(coords);

        (int, int) dimensions = model.getDimension();

        int width = dimensions.Item1;
        int height = dimensions.Item2;

        float x = width/2f;
        float y = height/2f * 0.32f;
        return worldPos + new Vector3(x, -y, 0);
    }

    public TileData getTileData(Vector3Int coords)
    {
        return datamap[coords];
    }
}
