using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthElemental : Enemy
{
    [Header("Earth Elemental specific stats")]
    [Tooltip("Mutliplies the damage value for targets in front")]
    [SerializeField] protected float frontlineDamageMultiplier = 1.2f;

    public float FrontlineDamageMultiplier { get => frontlineDamageMultiplier; set => frontlineDamageMultiplier = value; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
