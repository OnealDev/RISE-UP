using System;
using Unity.VisualScripting;
using UnityEngine;

public class AryasHealthScript : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;

        if(healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
    }
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); //if health goes below zero -> set to zero, if health goes above max -> set to max


        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
        
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}