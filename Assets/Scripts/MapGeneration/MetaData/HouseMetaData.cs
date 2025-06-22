using UnityEngine;

public class HouseMetaData : ObjectSpawnParams
{
    public static int Width = 6;
    public static int Height = 6;

    public override int getWidth() { return Width; }
    public override int getHeight() { return Height; }
}
