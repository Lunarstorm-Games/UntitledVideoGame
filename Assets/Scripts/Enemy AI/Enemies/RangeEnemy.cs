using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    [Header("Weapon Component")]
    [SerializeField] public Transform ProjectileSpawnPos;
    [SerializeField] public Projectile Projectile;
}
