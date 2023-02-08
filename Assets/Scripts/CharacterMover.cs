using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMover : MonoBehaviour
{
    private Vector2 movementVector;
    private Vector2 movement;
    public Rigidbody2D rb2d;
    public float Speed;
    public float currentSpeed;
    public float Acceleration;
    public float MaxSpeed;
    public float currentForewardDirection;
    private void Awake()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    public void Move(Vector2 movementVector)
    {
        //Debug.Log(movementVector);
        this.movementVector = movementVector;
        CalculateSpeed();
        movementVector *= currentSpeed;
        movement = movementVector;
        //Debug.Log(movementVector);
    }
    private void CalculateSpeed()
    {
        if (MathF.Abs(movementVector.y) == 0 && MathF.Abs(movementVector.x) == 0)
        {
            currentSpeed += -Acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed += Acceleration * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, MaxSpeed);
    }
    private void FixedUpdate()
    {
        rb2d.velocity = movement * Time.deltaTime;
        //Debug.Log("R" + rb2d.velocity);
    }
}
