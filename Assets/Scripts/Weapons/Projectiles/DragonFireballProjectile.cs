using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFireballProjectile : Projectile
{
    public bool impact = false;
    public override void Update()
    {
        if (impact) return;

        base.Update();
    }

    public override void ProjectileImpact()
    {
        base.ProjectileImpact();
    }
}
