using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector3 PointerPosition { get; set; }
    public bool IsAttacking { get; private set; }
    public List<Weapon> weapons;
    public int WeaponIndex = 0;
    public Weapon EquipedWeapon;
    public SpriteRenderer charcterRenderer, weaponRenderer;
    public Vector2 facedDirection;
    private float IntialScale;
    private void Start()
    {
        IntialScale = transform.localScale.y;
    }
    private void Update()
    {
        if (IsAttacking)
        {
            return;
        }
        facedDirection = (PointerPosition - transform.position).normalized;
        transform.right = facedDirection;
        Vector2 scale = transform.localScale;
        if (facedDirection.x < 0)
        {
            scale.y = -IntialScale;
        }
        else if (facedDirection.x > 0)
        {
            scale.y = IntialScale;
        }
        transform.localScale = scale;

        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            weaponRenderer.sortingOrder = charcterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = charcterRenderer.sortingOrder + 1;
        }
    }

    public void Attack()
    {
        EquipedWeapon.direciton = facedDirection;
        EquipedWeapon.Attack();
    }
    public void Reload()
    {
        if (EquipedWeapon.tag == "Ranged")
        {
            EquipedWeapon.Reload();
        }
    }
    public void SwapWeapon(float direction)
    {
        if (direction > 0)
        {
            // Next Weapon
            Debug.Log("Next");
            WeaponIndex++;
            if (WeaponIndex == weapons.Count)
            {
                WeaponIndex = 0;
            }
        }
        else
        {
            // Previous Weapon
            Debug.Log("Previous");
            WeaponIndex--;
            if (WeaponIndex < 0)
            {
                WeaponIndex = weapons.Count - 1;
            }
        }
        EquipedWeapon.gameObject.SetActive(false);
        EquipedWeapon = weapons[WeaponIndex];
        EquipedWeapon.gameObject.SetActive(true);
    }
}
