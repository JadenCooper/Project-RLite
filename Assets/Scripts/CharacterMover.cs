using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMover : MonoBehaviour
{
    private Vector2 movementVector;
    private Vector2 movement;
    private Animator animator;
    public Rigidbody2D rb2d;
    public float Speed;
    public float currentSpeed;
    public float Acceleration;
    public float MaxSpeed;
    public float currentForewardDirection;
    private bool isMoving;
    private void Awake()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
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
            isMoving = false;
        }
        else
        {
            currentSpeed += Acceleration * Time.deltaTime;
            isMoving = true;
            CheckSide();
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, MaxSpeed);
    }

    private void CheckSide()
    {
        if (movementVector.x < 0)
        {
            //gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            //gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    private void FixedUpdate()
    {
        rb2d.velocity = movement * Time.deltaTime;
        animator.SetBool("isMoving", isMoving);
        //Debug.Log("R" + rb2d.velocity);
    }
}
