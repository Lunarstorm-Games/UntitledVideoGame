using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : Entity, IDamageable
{
    [SerializeField] private HealthBarUI healthBar;

    [Header("Mana System")]
    [SerializeField] private ManaBar manaBar;
    [SerializeField] private float Mana;

    private float currentMana;
    
    public static Player Instance { get; private set; }
    public override void Awake()
    {
        base.Awake();

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
        currentHealth = Health;
        healthBar.SetMaxHealth(Health);
        currentMana = Mana;
        manaBar.SetMaxMana(Mana);
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
            Animator.SetTrigger("Death");
        }
    }

    public override void DeathAnimEvent()
    {
        //Revive System
    }

    public void UseMana(int mana)
    {
        currentMana -= mana;
        manaBar.SetMana(currentMana);
    }
}
