using System;
using System.Collections;
using System.Collections.Generic;
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

                Destroy(gameObject, _trapModel.duration);
            }
        }

    }           
}
