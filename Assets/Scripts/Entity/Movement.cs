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

    public Action<Vector2> OnDirecetionChange;
    public Action<bool> OnMovementChange;

    private bool isMoving = false;
    private Vector2 direction = Vector2.zero;

    private String[] ignorableTags = new String[1] { "Player Hitbox" };

    protected bool moveInDirection()
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
        List<RaycastHit2D> objs = new List<RaycastHit2D>();
        int count = rigidBody.Cast(direction,
            movementFilter,
            objs,
            speed * Time.fixedDeltaTime);

        if (areCollisionsIgnorable(objs)) return 0;

        return count;
    }

    private Boolean areCollisionsIgnorable(List<RaycastHit2D> objs)
    {
        if (objs.Count != 1) return false;

        RaycastHit2D obj = objs[0];
        string tag = obj.transform.gameObject.tag;

        Boolean found = false;

        foreach(String s in ignorableTags)
        {
            found = found || s == tag;
        }

        return found;
    }



    private float calcSpeed(Vector2 direction)
    {
        return SPEED * Math.Max(Math.Abs(direction.x), Math.Abs(direction.y));
    }

    protected void setIsMoving(bool b)
    {
        isMoving = b;
        OnMovementChange?.Invoke(b);
    }
    
    protected bool getIsMoving()
    {
        return isMoving;
    }

    protected void setDirection(Vector2 newDirection)
    {
        direction = newDirection;
        OnDirecetionChange?.Invoke(newDirection);
    }

    protected Vector2 getDirection()
    {
        return direction;
    }
}
