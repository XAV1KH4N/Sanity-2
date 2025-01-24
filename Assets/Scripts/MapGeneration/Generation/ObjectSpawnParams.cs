using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSpawnParams : MonoBehaviour
{
    [SerializeField]
    public int k_points;
    
    [SerializeField]
    public int min_dist;

    public abstract int getWidth();
    public abstract int getHeight();
}
