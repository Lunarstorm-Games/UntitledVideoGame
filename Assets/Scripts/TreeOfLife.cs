using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreeOfLife : Entity
{
    [SerializeField] private HealthBarUI healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
    }

    public override void TakeDamage(float damage, Entity origin)
    {
        if (!Killable)
            return;

        currentHealth -= damage;
        healthBar?.SetHealth(currentHealth);

        if (currentHealth <= 0 && !Death)
        {
            Death = true;
            OnDeath?.Invoke();
        }
    }

}
