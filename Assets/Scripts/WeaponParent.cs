using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector3 PointerPosition { get; set; }
    public bool IsAttacking { get; private set; }

    public SpriteRenderer charcterRenderer, weaponRenderer;
    private float IntialScale;
    public Animator animator;
    public float AttackDelay = 0.3f;
    private bool attackBlock;
    public Transform circleOrigin;
    public float radius;
    public int damage;
    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }
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

    public void Attack()
    {
        if (attackBlock)
        {
            return;
        }
        animator.SetTrigger("Attack");
        IsAttacking = true;
        attackBlock = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(AttackDelay);
        attackBlock = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        // If CircleOrigin == null = Vector3.zero else = circleOrgin.position
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }
    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            Debug.Log(collider.name);
            Health health;
            if (health = collider.GetComponent<Health>())
            {
                health.GetHit(damage, transform.parent.gameObject);
            }
        }
    }
}
