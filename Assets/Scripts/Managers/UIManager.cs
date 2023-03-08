using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text healthText;
    public Vector2 health = new Vector2(0,0);
    public void SetCurrentHealth(float currentHealth)
    {
        health.x = currentHealth;
        SetHealthUI();
    }
    public void SetMaxHealth(float healthMax)
    {
        health.y = healthMax;
        SetHealthUI();
    }
    private void SetHealthUI()
    {
        healthText.text = health.x.ToString() + "/" + health.y.ToString();
        Debug.Log(health);
    }
}
