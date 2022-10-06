using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public float maxHealth = 100;
    public float currentHealth;
    public float currentMana;
    public float maxMana;
    public HealthBar healthBar;
    public ManaBar manaBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseMana(10);
        }
    }

    public void TakeDamage(float damage, Entity entity) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void UseMana(int mana)
    {
        currentMana -= mana;
        manaBar.SetMana(currentMana);
    }
}
