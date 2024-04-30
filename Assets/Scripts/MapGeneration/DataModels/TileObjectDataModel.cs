using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileObjectDataModel : MonoBehaviour 
{
    [SerializeField]
    protected TileObjectDataType type;

    [SerializeField]
    protected int width;
    
    [SerializeField]
    protected int height;

    public (int, int) getDimension()
    {
        return (width, height);
    }
}