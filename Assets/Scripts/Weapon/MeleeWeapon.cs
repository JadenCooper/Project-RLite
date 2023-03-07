using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public Transform circleOrigin;
    public float radius;
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
                health.GetHit(damage, transform.parent.gameObject, knockbackStrength);
            }
        }
    }
}
