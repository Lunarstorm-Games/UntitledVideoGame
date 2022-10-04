using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMageProjectile : Projectile
{
    public override void Update()
    {
        base.Update();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Initialize(Entity shooter, Vector3 direction)
    {
        base.Initialize(shooter, direction);
    }

    protected override void DestroyProjectile(float delay = 0f)
    {
        base.DestroyProjectile(delay);
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
