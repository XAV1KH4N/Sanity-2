using System;
using UnityEngine;

public class TiledObject : MonoBehaviour
{
    //This class is only for 
    [SerializeField]
    private Collider2D triggerCollider;
    
    [SerializeField]
    private GameObject self;

    private Vector2Int tileCoords;

    public Action<Vector2Int> OnTrigger;

    public void init(Vector2Int tileCoords)
    {
        this.tileCoords = tileCoords;
    }

    public GameObject getGameObject()
    {
        return self;
    }

    public void OnTriggerEnter2D(Collider2D o)
    {
        OnTrigger?.Invoke(tileCoords);
    }
}
