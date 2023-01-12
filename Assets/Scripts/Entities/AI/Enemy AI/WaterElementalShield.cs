using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterElementalShield : MonoBehaviour, IDamageable
{
    [SerializeField] private float currentHealth;
    
    public void TakeDamage(float damage, Entity origin)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void SetHealth(float value)
    {
        currentHealth = value;
    }
}
