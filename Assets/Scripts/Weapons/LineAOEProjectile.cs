using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LineAOEProjectile : Projectile
{
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public override void Start()
    {
        float zSize = transform.localScale.z;
        transform.position += direction * zSize / 2;
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void DestroyProjectile(float delay = 0f)
    {
        base.DestroyProjectile(delay);
    }

    public override void ProjectileImpact()
    {
        base.ProjectileImpact();
    }
}
