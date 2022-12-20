using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WizardBossMagicMissileProjectile : Projectile
{
    private NavMeshAgent agent;
    private Entity target;

    public override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    public override void Update()
    {
        if (target == null || agent == null)
        {
            DestroyProjectile();
            return;
        }

        agent.SetDestination(target.transform.position);
    }

    public override void OnTriggerEnter(Collider collider)
    {
        Debug.Log(this.gameObject + " - " + collider.name, collider.gameObject);
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

        ProjectileImpact();
    }

    public void SetTarget(Entity target)
    {
        this.target = target;
    }

    
}
