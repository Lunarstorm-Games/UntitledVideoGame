using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DragonFireballProjectile : Projectile
{
    [SerializeField] private Collider fireballColl;
    [SerializeField] private VisualEffect fireEffect, fireballEffect;
    [SerializeField] private Transform AOE_Center;

    public float AOE_Damage;
    public float AOE_Time;
    public float FireAOETickRate;
    public float FireAOERadius;

    private float fireAOETickRateTime;
    private bool impact = false;
    public override void Update()
    {
        if (impact)
        {
            fireballColl.enabled = false;
            fireballEffect.enabled = false;
            fireEffect.enabled = true;
            if (AOE_Time > 0)
            {
                OnAOETriggerStay();
                AOE_Time -= Time.deltaTime;
            }
            else DestroyProjectile();

            return;
        }

        base.Update();
    }

    public override void ProjectileImpact()
    {
        base.ProjectileImpact();
    }

    public override void OnTriggerEnter(Collider collider)
    {
        DealDamage(collider, damage);

        impact = true;
    }

    public void OnAOETriggerStay()
    {
        if (fireAOETickRateTime < 0)
        {
            Collider[] AOETargets = Physics.OverlapSphere(AOE_Center.position, FireAOERadius);
            {
                foreach (Collider hitTarget in AOETargets)
                {
                    DealDamage(hitTarget, AOE_Damage);
                }
            }
            fireAOETickRateTime = FireAOETickRate;
        }
        else fireAOETickRateTime -= Time.deltaTime;

    }

    public void DealDamage(Collider collider, float damage)
    {
        if (collider.transform.root.TryGetComponent<Entity>(out Entity entity))
        {
            if (entity != shooter)
            {
                if (shooter.IsValidTarget(entity.EntityType))
                {
                    if (collider.transform.root.TryGetComponent<IDamageable>(out IDamageable target))
                    {
                        target.TakeDamage(damage, shooter);
                    }
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AOE_Center.position, FireAOERadius);
    }
}
