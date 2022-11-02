using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class ActivateTrap : MonoBehaviour
{
    [SerializeField] public TrapModel _trapModel;
    [SerializeField] private GameObject trap;

    private void OnTriggerEnter(Collider other)
    {
        if (trap.name.Contains("Placed"))
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                trap.SetActive(true);
                trap.transform.GetChild(0).gameObject.SetActive(true);
                if (other.TryGetComponent(out IDamageable target))
                {
                    target.TakeDamage(_trapModel.damage, null);
                }

                if (other.TryGetComponent(out ISlowable targetSlow))
                {
                    targetSlow.Slow(_trapModel.slowRate, null); 
                }

                Destroy(gameObject, _trapModel.duration);
            }
        }

    }           
}
