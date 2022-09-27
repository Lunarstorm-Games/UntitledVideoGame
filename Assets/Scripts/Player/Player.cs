using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Player : Entity, IDamageable
{
    public static Player Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    [SerializeField] private float maxHealth = 100;
    //[SerializeField] private Animator animator;
    [SerializeField] private HealthBarUI healthBar;
    [SerializeField] public UnityEvent OnDeath;

    protected float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar?.SetMaxHealth(currentHealth);
    }

    public void TakeDamage(float damage, Entity origin)
    {
        currentHealth -= damage;
        healthBar?.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
            //animator.SetTrigger("Death");
        }
        
    }

    
}
