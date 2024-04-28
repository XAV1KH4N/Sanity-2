using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracer : MonoBehaviour
{
    public Transform player;

    void FixedUpdate()
    {
        transform.position = player.transform.position + new Vector3(0, 1, -5);
    }
}