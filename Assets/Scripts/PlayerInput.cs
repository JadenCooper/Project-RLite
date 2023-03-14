using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent<float> OnWeaponSwapInput;
    public UnityEvent OnAttack, OnReload, OnAltWeaponAction;
    [SerializeField]
    private InputActionReference movement, attack, pointerPosition, swapWeapon, reload, AltWeaponAction;
    private void Update()
    {
        OnMovementInput?.Invoke(movement.action.ReadValue<Vector2>().normalized);
        OnPointerInput?.Invoke(GetPointerPosition());
    }
    private Vector2 GetPointerPosition()
    {
        Vector2 mousePos = pointerPosition.action.ReadValue<Vector2>();
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnEnable()
    {
        attack.action.performed += PerformAttack;
        swapWeapon.action.performed += PreformWeaponSwap;
        reload.action.performed += PreformReload;
        AltWeaponAction.action.performed += PreformAltWeaponAction;
    }
    private void OnDisable()
    {
        attack.action.performed -= PerformAttack;
        swapWeapon.action.performed -= PreformWeaponSwap;
        reload.action.performed -= PreformReload;
        AltWeaponAction.action.performed -= PreformAltWeaponAction;
    }

    private void PreformAltWeaponAction(InputAction.CallbackContext obj)
    {
        OnAltWeaponAction?.Invoke();
    }

    private void PerformAttack(InputAction.CallbackContext obj)
    {
        OnAttack?.Invoke();
    }
    private void PreformWeaponSwap(InputAction.CallbackContext obj)
    {
        OnWeaponSwapInput?.Invoke(swapWeapon.action.ReadValue<float>());
    }
    private void PreformReload(InputAction.CallbackContext obj)
    {
        OnReload?.Invoke();
    }
}
