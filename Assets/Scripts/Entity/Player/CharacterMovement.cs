using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : Movement
{
    [SerializeField]
    private Joystick joystick;

    private bool isMoving = false;
    private Vector2 direction = Vector2.zero;

    public Action<Vector2> OnDirecetionChange;

    void Start()
    {
        addListeners();
    }

    private void FixedUpdate()
    {
        if (isMoving) moveInDirection(direction);
    }

    private void addListeners()
    {
        joystick.OnMove += setCurrentDirection;
        joystick.OnStop += stopMovement;
    }

    private void setCurrentDirection(Vector2 newDirection)
    {
        direction = newDirection;
        isMoving = true;
        OnDirecetionChange?.Invoke(newDirection); // Do we need a ?
    }

    private void stopMovement()
    {
        isMoving = false;
    }

}
