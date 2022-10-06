using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreeOfLife : Entity, IDamageable
{
    [SerializeField] private float maxHealth = 100;

    [SerializeField] public UnityEvent OnDeath;

    protected float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage, Entity origin)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}
