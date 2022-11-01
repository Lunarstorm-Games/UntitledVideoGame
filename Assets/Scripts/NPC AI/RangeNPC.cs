using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNPC : Guard
{
    [Header("Weapon Component")]
    [SerializeField] public Transform ProjectileSpawnPos;
    [SerializeField] public Projectile Projectile;
}
