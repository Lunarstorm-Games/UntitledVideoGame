using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonProjectile : Projectile
{
    public float AOERadius;
    public int PoisonDuration;
    public int PoisonDamage;
    
    public int AOELevel;
    public int PoisonDurationLevel;
    public int PoisonDamageLevel;

    public override void OnTriggerEnter(Collider other)
    {
        AOETargets(transform.position, AOERadius);
        base.ProjectileImpact();
    }

    public void UpgradeAOE(float newAOE)
    {
        AOERadius += newAOE;
        AOELevel += 1;
    }

    private void AOETargets(Vector3 origin, float radius)
    {
        Collider[] AOETargets = Physics.OverlapSphere(origin, radius);
        {
            foreach (var hitTarget in AOETargets)
            {
                if (hitTarget.transform.root.TryGetComponent<Entity>(out Entity entity))
                {
                    if (entity != shooter)
                    {
                        if (shooter.IsValidTarget(entity.EntityType))
                        {
                            if (hitTarget.TryGetComponent<Enemy>(out Enemy enemy))
                            {
                                enemy.ApplyPoison(PoisonDuration, PoisonDamage, shooter);
                            }
                        }
                    }
                }
            }
        }
    }
}