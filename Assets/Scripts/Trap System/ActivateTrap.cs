using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrap : MonoBehaviour
{
    [SerializeField] private GameObject trap;
    [SerializeField] protected float damage;
    [SerializeField] public float duration = 2f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            trap.SetActive(true);               
            if (other.TryGetComponent(out IDamageable target))
            {
                target.TakeDamage(damage, null);
            }
            Destroy(trap,duration);
        }
    }           
}
