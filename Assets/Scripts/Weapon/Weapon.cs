using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public bool IsAttacking { get; set; }
    public Animator animator;
    public float AttackDelay = 0.3f;
    public bool attackBlock;
    public int damage;
    public float knockbackStrength = 5; 
    public Vector2 direciton;
    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }
    public virtual void Attack()
    {
        Debug.Log("Attacking");
        if (attackBlock)
        {
            return;
        }
        animator.SetTrigger("Attack");
        IsAttacking = true;
        attackBlock = true;
        StartCoroutine(DelayAttack());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    public virtual void OnEnable()
    {
        attackBlock = false;
        IsAttacking = false;
    }
    public IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(AttackDelay);
        attackBlock = false;
    }
}
