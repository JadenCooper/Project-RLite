using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class PlayerControler : MonoBehaviour
{
    public UnityEvent<Vector2> OnMoveBody = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnMovePointer = new UnityEvent<Vector2>();
    [SerializeField]
    private InputActionReference movement, attack, pointerPosition;
    private WeaponParent weaponParent;
    // Update is called once per frame
    private void Awake()
    {
        weaponParent = GetComponentInChildren<WeaponParent>();
    }
    void Update()
    {
        GetBodyMovement();
        weaponParent.PointerPosition = GetPointerPosition();
    }
    private void GetBodyMovement()
    {
        Vector2 movementVector = movement.action.ReadValue<Vector2>();
        //Debug.Log(movementVector);
        OnMoveBody?.Invoke(movementVector);
    }

    private Vector2 GetPointerPosition()
    {
        Vector2 mousePos = pointerPosition.action.ReadValue<Vector2>();
        //Debug.Log(mousePos);
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnEnable()
    {
        attack.action.performed += PerformAttack;
    }
    private void OnDisable()
    {
        attack.action.performed -= PerformAttack;
    }
    private void PerformAttack(InputAction.CallbackContext obj)
    {
        //if(weaponParent == null)
        //{
        //    Debug.Log("weaponParent Is Null");
        //    return;
        //}
        //weaponParent.PreformAnAttack();
    }
}
