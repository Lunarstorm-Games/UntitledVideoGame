using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DummyStructure : Entity, IDamageable
{
    [SerializeField] private float maxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public override void TakeDamage(float damage, Entity origin)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
