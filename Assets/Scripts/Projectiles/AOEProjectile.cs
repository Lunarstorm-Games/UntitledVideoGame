using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEProjectile : Projectile
{
    [SerializeField] private float AOERadius;

    public override void Initialize(Entity shooter, Vector3 direction)
    {
        base.Initialize(shooter, direction);
    }

    public override void OnTriggerEnter(Collider other)
    {
        //base.OnTriggerEnter(other);
        AOEDamage(transform.position, AOERadius);
        base.ProjectileImpact();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    protected override void DestroyProjectile(float delay = 0f)
    {
        base.DestroyProjectile(delay);
    }

    protected override void ProjectileImpact()
    {
        base.ProjectileImpact();
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
