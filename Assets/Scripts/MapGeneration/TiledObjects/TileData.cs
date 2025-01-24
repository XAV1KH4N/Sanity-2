using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData
{
    private TileObjectDataType type;

    public TileData(TileObjectDataType type)
    {
        this.type = type;
    }

    public TileObjectDataType getType()
    {
        return type;
    }
}
