using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Player : Entity, IDamageable
{
    [SerializeField] private Animator animator;
    [SerializeField] private HealthBarUI healthBar;
    [SerializeField] private float maxHealth = 100;
    public ManaBar manaBar;
    public float maxMana;
    public UnityEvent OnDeath;
    private float currentHealth;
    private float currentMana;
    
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

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);
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
    
    public void UseMana(int mana)
    {
        currentMana -= mana;
        Debug.Log(currentMana);
        manaBar.SetMana(currentMana);
    }
}
