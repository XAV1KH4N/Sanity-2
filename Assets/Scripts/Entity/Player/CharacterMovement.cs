using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CharacterMovement : Movement
{
    [SerializeField]
    private PlayerModule playerModule;

    [SerializeField]
    private WorldModule worldModule;

    [SerializeField]
    private Joystick joystick;

    [SerializeField]
    private SwipeController swiper;

    private GroundSpeedEvaluator speedEval;

    void Start()
    {
        speedEval = new GroundSpeedEvaluator(playerModule, worldModule);
        addListeners();
    }

    override protected float adjustedSpeed(float speed)
    {
        return speedEval.ratio() * speed; 
    }

    private void FixedUpdate()
    {
        if (getIsMoving()) moveInDirection();
    }

    private void addListeners()
    {
        swiper.OnLook += setLookDirection;

        joystick.OnMove += handleMoveDirection;
        joystick.OnStop += stopMovement;
    }
    
    private void handleMoveDirection(Vector2 newDirection)
    {
        setMovementDirection(newDirection);
        setIsMoving(true);
    }

    private void stopMovement()
    {
        setIsMoving(false);
    }
}
