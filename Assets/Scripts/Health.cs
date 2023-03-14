using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int currentHealth, maxHealth;
    public UnityEvent<GameObject> OnDeathWithReference;
    public UnityEvent<float> ChangeHealth, SetHealth;
    private KnockbackFeedback knockbackFeedback;
    [SerializeField]
    private bool isDead;
    public bool IsBlocking { get; set; } = false;
    private void Start()
    {
        knockbackFeedback = gameObject.GetComponent<KnockbackFeedback>();
        SetHealth?.Invoke(maxHealth);
        ChangeHealth?.Invoke(currentHealth);
    }
    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }
    public void GetHit(int damageTaken, GameObject sender, float knockback)
    {
        //Debug.Log(sender);
        if (isDead)
        {
            return;
        }
        if (sender.layer == gameObject.layer)
        {
            return;
        }
        if (!IsBlocking)
        {
            currentHealth -= damageTaken;
            if (currentHealth <= 0)
            {
                OnDeathWithReference?.Invoke(sender);
                isDead = true;
                Destroy(gameObject);
            }
        }
        knockbackFeedback.PlayFeedback(sender, knockback);
        ChangeHealth?.Invoke(currentHealth);
    }
}
