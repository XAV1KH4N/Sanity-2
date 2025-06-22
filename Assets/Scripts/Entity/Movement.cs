using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

abstract public class Movement : MonoBehaviour
{
    [SerializeField]
    protected float FLAT_SPEED = 3.75f;

    [SerializeField]
    protected ContactFilter2D movementFilter;

    [SerializeField]
    protected Rigidbody2D rigidBody;

    public Action<Vector2> OnLookDirectionChange;
    public Action<Vector2> OnMoveDirectionChange;
    public Action<bool> OnMovementChange;

    private bool isMoving = false;
    private Vector2 lookDirection = Vector2.zero;
    private Vector2 movementDirection = Vector2.zero;

    protected bool moveInDirection()
    {
        if (movementDirection == Vector2.zero) return false;

        float directionalSpeed = calcSpeed(movementDirection);
        (int, int) counts = countCollisionsForDirection(movementDirection, directionalSpeed);

        float x = 0;
        float y = 0;

        if (counts.Item1 == 0) x = movementDirection.x;
        if (counts.Item2 == 0) y = movementDirection.y;

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

    private bool areCollisionsIgnorable(List<RaycastHit2D> objs)
    {
        bool forall = true;

        foreach (RaycastHit2D obj in objs)
        {
            forall = forall && obj.transform.gameObject.tag.Equals("IGNORE");
        }

        return forall;
    }

    protected float calcSpeed(Vector2 direction)
    {
        return adjustedSpeed(FLAT_SPEED) * Math.Max(Math.Abs(direction.x), Math.Abs(direction.y));
    }

    virtual protected float adjustedSpeed(float speed)
    {
        return speed;
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

    protected void setMovementDirection(Vector2 newDirection)
    {
        movementDirection = newDirection;
        OnMoveDirectionChange?.Invoke(newDirection);
    }

    protected void setLookDirection(Vector2 newDirection)
    {
        Debug.Log("New Direction " + newDirection);
        lookDirection = newDirection;
        OnLookDirectionChange?.Invoke(newDirection);
    }

    protected Vector2 getLookDirection()
    {
        return lookDirection;
    }

    protected Vector2 getMoveDirection()
    {
        return movementDirection;
    }
}
