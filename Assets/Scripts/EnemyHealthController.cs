using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float health;

    private void Start()
    {
        health = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.StartsWith("BasicSpellProjectile"))
        {
            if (health <= 0)
                Destroy(gameObject);
        }
    }
}
