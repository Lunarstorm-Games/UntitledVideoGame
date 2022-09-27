using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEProjectile : Projectile
{
    public override void Initialize(Entity shooter, Vector3 direction)
    {
        base.Initialize(shooter, direction);
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
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
}
