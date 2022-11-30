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
    [SerializeField] private float manaRechargeRate = 3.5f;
    [SerializeField] public float currentMana;
    [SerializeField] public UnityEvent OnDeath;
    
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
        currentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
        currentMana = Mana;
        manaBar.SetMaxMana(Mana);
    }

    void Update()
    {
        RegenerateMana();
    }

    public override void TakeDamage(float damage, Entity origin)
    {

        currentHealth -= damage;
        healthBar?.SetHealth(currentHealth);

        if (currentHealth <= 0 && !Death)
        {
            Death = true;
            OnDeath?.Invoke();
        }
    }

    public void UseMana(float mana)
    {
        currentMana -= mana;
        manaBar.SetMana(currentMana);
    }

    public void RegenerateMana()
    {
        if (currentMana < Mana)
        {
            currentMana += manaRechargeRate * Time.deltaTime * 10f;
            manaBar.SetMana(currentMana);

        }
    }
}
