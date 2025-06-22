using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneratorMetaData : MonoBehaviour
{

    [SerializeField]
    public float blend = 0.15f;

    [SerializeField]
    public int treeVariance = 5;

    [SerializeField]
    public int houseVariance = 3;

    [SerializeField]
    public ObjectSpawnParams pointyTreeMetaData;

    [SerializeField]
    public ObjectSpawnParams tallTreeMetaData;

    [SerializeField]
    public ObjectSpawnParams roundTreeMetaData;

    [SerializeField]
    public ObjectSpawnParams houseMetaData;
}
