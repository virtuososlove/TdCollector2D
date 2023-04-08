using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    private float currentHealth;
    [SerializeField] float maxHealth;
    public event EventHandler onDamaged;
    public event EventHandler onDied;
    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        onDamaged?.Invoke(this, EventArgs.Empty);
        if (IsDead())
        {
            onDied?.Invoke(this, EventArgs.Empty);
        }
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public float GetHealthNormalized()
    {
        
        return (currentHealth / maxHealth);
    }
    public bool IsDead()
    {
        return currentHealth <= 0;
    }
    public void setHealthAmountMax(float healthAmountMax, bool updateHealthAmount)
    {
        this.maxHealth = healthAmountMax;
        if (updateHealthAmount)
        {
            currentHealth = maxHealth;
        }
    }
}
