using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public Transform circleOrigin;
    public float radius;
    public WeaponStats weaponStats;
    public bool CanBlock = true;
    public float blockTime = 0.5f;
    public float blockDelay = 0.5f;
    private WeaponParent weaponParent;
    private void Awake()
    {
        weaponParent= GetComponentInParent<WeaponParent>();
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
            //Debug.Log(collider.name);
            Health health;
            if (health = collider.GetComponent<Health>())
            {
                health.GetHit(weaponStats.damage, transform.parent.gameObject, weaponStats.knockbackStrength);
            }
        }
    }

    public override IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(weaponStats.AttackDelay);
        attackBlock = false;
    }

    public override void Reload()
    {
        Debug.Log("This Weapon Is Melee Not Ranged");
    }

    public override void AltWeaponAction()
    {
        //Block 
        if (CanBlock)
        {
            StartCoroutine(BlockTime());
        }
    }
    public  IEnumerator BlockTime()
    {
        Debug.Log("Blocking");
        weaponParent.SetBlock();
        CanBlock = false;
        yield return new WaitForSeconds(blockTime);
        StartCoroutine(BlockDelay());
    }
    public  IEnumerator BlockDelay()
    {
        weaponParent.SetBlock();
        yield return new WaitForSeconds(weaponStats.AttackDelay);
        CanBlock = true;
        Debug.Log("Can Block Again");
    }
}
