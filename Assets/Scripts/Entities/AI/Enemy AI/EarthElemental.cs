using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthElemental : Enemy
{
    [Header("Earth Elemental specific stats")]
    [Tooltip("Mutliplies the damage value for target in front")]
    [SerializeField] protected float FrontlineDamageMultiplier = 1.2f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
