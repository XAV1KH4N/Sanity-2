using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallTreeMetaData : ObjectSpawnParams
{
    public static int Width = 3;
    public static int Height = 4;

    public override int getWidth() { return Width; }
    public override int getHeight() { return Height; }
}
