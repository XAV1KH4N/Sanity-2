using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class CharacterMovement : Movement
{
    [SerializeField]
    private Joystick joystick;

    void Start()
    {
        addListeners();
    }

    private void FixedUpdate()
    {
        if (getIsMoving()) moveInDirection();
    }

    private void addListeners()
    {
        joystick.OnMove += setDirectionAndMove;
        joystick.OnLook += setDirection;
        joystick.OnStop += stopMovement;
    }
    
    private void setDirectionAndMove(Vector2 newDirection)
    {
        setDirection(newDirection);
        setIsMoving(true);
    }

    private void stopMovement()
    {
        setIsMoving(false);
    }
}
