using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNPC : Guard
{
    [Header("RangeWeapon Component")]
    [SerializeField] protected Transform projectileSpawnPos;
    [SerializeField] protected Projectile projectile;

    public Transform ProjectileSpawnPos { get => projectileSpawnPos; set => projectileSpawnPos = value; }
    public Projectile Projectile { get => projectile; set => projectile = value; }

    public void FireProjectile()
    {
        Projectile projectileClone = Instantiate(projectile, projectileSpawnPos.position, Quaternion.identity);
        Vector3 TargetPos = TargetSpot.position;
        Vector3 shootDir = (TargetPos - projectileSpawnPos.position).normalized;
        projectileClone.transform.LookAt(TargetPos);
        projectileClone.damage = damage;
        projectileClone.Initialize(this, shootDir);
    }
}
