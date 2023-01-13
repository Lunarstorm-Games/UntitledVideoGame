using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEProjectile : Projectile
{
    public float AOERadius;
    public int AOELevel;

    public override void OnTriggerEnter(Collider other)
    {
        //base.OnTriggerEnter(other);
        AOEDamage(transform.position, AOERadius);
        base.ProjectileImpact();
    }
    
    public void UpgradeAOE(float newAOE)
    {
        AOERadius += newAOE;
        AOELevel += 1;
    }

    private void AOEDamage(Vector3 center, float radius)
    {
        Collider[] AOETargets = Physics.OverlapSphere(center, radius);
        {
            foreach(var hitTarget in AOETargets)
            {
                base.OnTriggerEnter(hitTarget);
            }
        }
    }
}
