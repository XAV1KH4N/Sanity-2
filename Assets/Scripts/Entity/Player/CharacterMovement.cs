using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class CharacterMovement : Movement
{
    [SerializeField]
    private PlayerModule playerModule;

    [SerializeField]
    private WorldModule worldModule;

    [SerializeField]
    private Joystick joystick;

    private GroundSpeedEvaluator speedEval;

    void Start()
    {
        speedEval = new GroundSpeedEvaluator(playerModule, worldModule);
        addListeners();
    }

    override protected float adjustedSpeed(float speed)
    {
        Debug.Log(speedEval.ratio() + " " + speed + " " + speedEval.ratio() * speed);
        return speedEval.ratio() * speed; 
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
