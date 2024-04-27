using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float SPEED = 0.75f;

    [SerializeField]
    protected ContactFilter2D movementFilter;

    [SerializeField]
    protected Rigidbody2D rigidBody;
   

    protected bool moveInDirection(Vector2 direction)
    {
        if (direction == Vector2.zero) return false;

        float directionalSpeed = calcSpeed(direction);
        (int, int) counts = countCollisionsForDirection(direction, directionalSpeed);

        float x = 0;
        float y = 0;

        if (counts.Item1 == 0) x = direction.x;
        if (counts.Item2 == 0) y = direction.y;
        
        Vector2 finalDirection = new Vector2(x, y);
        Vector2 newPos = rigidBody.position + finalDirection * directionalSpeed * Time.fixedDeltaTime;
        rigidBody.MovePosition(newPos);
        
        return finalDirection != Vector2.zero;
    }

    private (int, int) countCollisionsForDirection(Vector2 direction, float speed)
    {
        int countX = countCollisions(new Vector2(direction.x, 0), speed);
        int county = countCollisions(new Vector2(0, direction.y), speed);
        return (countX, county);
    }

    private int countCollisions(Vector2 direction, float speed)
    {
        int count = rigidBody.Cast(direction,
            movementFilter,
            new List<RaycastHit2D>(),
            speed * Time.fixedDeltaTime);
        return count;
    }


    private float calcSpeed(Vector2 direction)
    {
        return SPEED * Math.Max(Math.Abs(direction.x), Math.Abs(direction.y));
    }
}
