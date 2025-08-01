using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Factory : MonoBehaviour
{
    [SerializeField]
    private TileObjectDataModel house;

    [SerializeField]
    private TileObjectDataModel roundTree;
    
    [SerializeField]
    private TileObjectDataModel tallTree;
    
    [SerializeField]
    private TileObjectDataModel pointyTree;
    
    [SerializeField]
    private ColliderTemplateService colliderProvider;

    [SerializeField]
    private Tilemap ground;
        
    [SerializeField]
    private Tilemap collision;
    
    [SerializeField]
    private Tilemap upperFeature;

    private Map map;

    public Action<Vector2Int, TileData> OnObjectEntry;

    public void createAt(Vector2Int coords, TileObjectDataType type)
    {
        if (map.isCoordTaken(coords) || !isSpaceAvailable(coords, type)) return;

        switch(type)
        {
            case TileObjectDataType.TALL_TREE:
                createTree(coords, tallTree);
                break;
            
            case TileObjectDataType.POINTY_TREE:
                createTree(coords, pointyTree);
                break;            
            
            case TileObjectDataType.ROUND_TREE:
                createTree(coords, roundTree);
                break;

            case TileObjectDataType.HOUSE:
                createHouse(coords, house);
                break;

            default:
                break;
        }
    }

    private TileObjectDataModel getTileObjectDataModel(TileObjectDataType type)
    {
        switch (type)
        {
            case TileObjectDataType.TALL_TREE:
                return tallTree;

            case TileObjectDataType.POINTY_TREE:
                return pointyTree;

            case TileObjectDataType.ROUND_TREE:
                return roundTree;

            case TileObjectDataType.HOUSE:
                return house;

            default:
                return null;
        }
    }

    public void setMap(Map map)
    {
        wipe();
        this.map = map;
    }

    private void wipe()
    {
        collision.ClearAllTiles();
        upperFeature.ClearAllTiles();
    }

    private bool isBottomGround(Vector2Int coords, TileObjectDataModel model)
    {
        bool isGrass = true;
        
        int height = model.getDimension().Item2;
        int width = model.getDimension().Item1;

        for (int x = 0; x < width; x++)
        {
            Vector2Int bottomCoords = new Vector2Int(coords.x + x, coords.y - height + 1);
            bool isGround = GroundTypeUtils.isGround(map.getGroundTypeAt(bottomCoords));
            isGrass = isGrass && isGround;
        }
        
        for (int x = 0; x < width; x++)
        {
            Vector2Int bottomCoords = new Vector2Int(coords.x + x, coords.y - height + 2);
            bool isGround = GroundTypeUtils.isGround(map.getGroundTypeAt(bottomCoords));
            isGrass = isGrass && isGround;
        }
        return isGrass;
    }

    private bool createTree(Vector2Int coords, TileObjectDataModel model)
    {

        if (isBottomGround(coords, model))
        {
            updateCollidersAndMap(coords, model);
            addTopToUpperFeatures(coords, model, 2);
            addBottomToCollsion(coords, model);

            return true;
        }

        return false;
    }
    
    private bool createHouse(Vector2Int coords, TileObjectDataModel model)
    {

        if (isBottomGround(coords, model))
        {
            updateCollidersAndMap(coords, model);
            addTopToUpperFeatures(coords, model, 1);
            addBottomToCollsion(coords, model);
            return true;
        }

        return false;
    }
    

    private void addTopToUpperFeatures(Vector2Int coords, TileObjectDataModel model, int yMax)
    {
        (int, int) dimensions = model.getDimension();
        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < dimensions.Item1; x++)
            {
                Tile tile = model.getTile(x, y);
                Vector3Int relative = new Vector3Int(coords.x + x, coords.y - y);
                upperFeature.SetTile(relative, tile);
            }
        }
    }

    private void addBottomToCollsion(Vector2Int coords, TileObjectDataModel model)
    {
        (int, int) dimensions = model.getDimension();
        for (int y = 0; y < dimensions.Item2; y++)
        {
            for (int x = 0; x < dimensions.Item1; x++)
            {
                Tile tile = model.getTile(x,y);
                Vector3Int relative = new Vector3Int(coords.x + x, coords.y - y);
                collision.SetTile(relative, tile);
            }
        }
    }

    private void updateCollidersAndMap(Vector2Int coords, TileObjectDataModel model)
    {
        TileData data = new TileData(model.getType());
        createCollider(coords, model);
        map.addTiledObject(coords, data);
    }

    private void createCollider(Vector2Int coords, TileObjectDataModel model)
    {
        TiledObject obj = colliderProvider.createTiledObject(model.getType());
        initTileObject(coords, obj);
        Vector3 worldPos = findCenter(coords, model);
        obj.transform.position = worldPos;
    }

    private void initTileObject(Vector2Int coords, TiledObject tileObj)
    {
        tileObj.init(coords);
        tileObj.OnTrigger += handleObjectEntry;
    } 

    private void handleObjectEntry(Vector2Int coords)
    {
        OnObjectEntry?.Invoke(coords, map.getTileData(coords));
    }

    private Vector3 findCenter(Vector2Int coords, TileObjectDataModel model)
    {
        Vector3Int coords3d = new Vector3Int(coords.x, coords.y);
        Vector3 worldPos = ground.GetCellCenterWorld(coords3d);

        (int, int) dimensions = model.getDimension();

        int width = dimensions.Item1;
        int height = dimensions.Item2;

        float x = width/2f;
        float y = height/2f * 0.32f;
        return worldPos + new Vector3(x, -y, 0);
    }

    //Maybe move this logic to the objectSpawner
    // More effiect, some sort of cache to record all taken up spaces
    private bool isSpaceAvailable(Vector2Int coords, TileObjectDataType type)
    {
        TileObjectDataModel model = getTileObjectDataModel(type);

        (int, int) dimensions = model.getDimension();
        for (int y = 0; y < dimensions.Item2; y++)
        {
            for (int x = 0; x < dimensions.Item1; x++)
            {
                Vector2Int relative = new Vector2Int(coords.x + x, coords.y - y);
                bool isCoordTaken = map.isCoordTaken(relative);
                bool isCollision = collision.GetTile(new(relative.x, relative.y, 0)) != null;
                bool isFeature = upperFeature.GetTile(new(relative.x, relative.y, 0)) != null;
                bool isWithinBounds = relative.x >= 0 && relative.y >= 0 && relative.y < map.getHeight() - 1 && relative.x < map.getWidth() - 1;
                if (isCoordTaken || isFeature || isCollision || !isWithinBounds) return false;
            }
        }
        return true;
    }
}
