using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector3 PointerPosition { get; set; }
    public SpriteRenderer charcterRenderer, weaponRenderer;
    private float IntialScale;
    private void Start()
    {
        IntialScale = transform.localScale.y;
    }
    private void Update()
    {
        Vector2 direction = (PointerPosition - transform.position).normalized;
        transform.right = direction;
        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.y = -IntialScale;
        }
        else if (direction.x > 0)
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
}
