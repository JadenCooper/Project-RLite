using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int currentHealth, maxHealth;
    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;
    private KnockbackFeedback knockbackFeedback;
    [SerializeField]
    private bool isDead;
    private void Start()
    {
        knockbackFeedback = gameObject.GetComponent<KnockbackFeedback>();
    }
    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }
    public void GetHit(int damageTaken, GameObject sender, float knockback)
    {
        Debug.Log(sender);
        if (isDead)
        {
            return;
        }
        if (sender.layer == gameObject.layer)
        {
            return;
        }
        currentHealth -= damageTaken;
        if (currentHealth > 0)
        {
            knockbackFeedback.PlayFeedback(sender, knockback);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            gameObject.SetActive(false);
        }
    }
}
