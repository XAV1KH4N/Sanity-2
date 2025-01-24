using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxTracer : MonoBehaviour
{
    public Transform player;

    void FixedUpdate()
    {
        transform.position = player.transform.position;
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.gameObject.tag + " : onHit");
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        Debug.Log(o.tag + " : 2d");
    }
}