using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreeOfLife : Entity
{
    [SerializeField] private HealthBarUI healthBar;
    [SerializeField] private UnityEvent onDeath;
    public float hpPercentageForParticles = 0.1f;
    public float startEffectDuration = 10f;
    public GameObject DeathParticles;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
        // DeathParticles.SetActive(false);
        StartCoroutine(StartEffect());
    }

    IEnumerator StartEffect()
    {
        yield return new WaitForSeconds(startEffectDuration);
        DeathParticles.SetActive(false);
    }

    public override void TakeDamage(float damage, Entity origin)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth < maxHealth * hpPercentageForParticles && !DeathParticles.activeSelf)
        {
            DeathParticles.SetActive(true);
        }

        if (currentHealth <= 0 && !Death)
        {
            Death = true;
            onDeath?.Invoke();
        }
    }
}