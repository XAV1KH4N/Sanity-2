using UnityEngine;

public class ColliderTemplateService : MonoBehaviour
{
    [SerializeField]
    private TiledObject roundTree;
    
    [SerializeField]
    private TiledObject tallTree;
    
    [SerializeField]
    private TiledObject pointyTree;

    public TiledObject createTiledObject(TileObjectDataType type)
    {
        GameObject template = getCollider(type).getGameObject();
        GameObject clone = Instantiate(template);
        clone.SetActive(true);
        TiledObject obj = clone.GetComponent<TiledObject>();
        return obj;
    }

    private TiledObject getCollider(TileObjectDataType type)
    {
        switch(type)
        {
            case TileObjectDataType.TALL_TREE:
                return tallTree;
            
            case TileObjectDataType.POINTY_TREE:
                return pointyTree;            
            
            case TileObjectDataType.ROUND_TREE:
            default:
                return roundTree;


        }
    }
}
