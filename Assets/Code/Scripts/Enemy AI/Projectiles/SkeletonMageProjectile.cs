using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMageProjectile : Projectile
{
    public override void Update()
    {
        Debug.Log("Projectile Moving");
        base.Update();
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
    }
}
