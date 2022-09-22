using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public float maxHealth = 100;
    public float currentHealth;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);  
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage, Entity entity) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
