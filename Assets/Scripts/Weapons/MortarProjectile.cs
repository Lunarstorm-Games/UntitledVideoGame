using Assets.Scripts.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarProjectile : BufferedSpellProjectile
{
    public float AOERadius;


    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();   
    }
    public override void Initialize(Entity shooter, Vector3 direction)
    {
        base.Initialize(shooter, direction);
    }
    public void Initialize(Mortar shooter, Vector3 direction)
    {
        this.shooter = shooter;
        this.direction = direction;
        DestroyProjectile(range);
        transform.position = shooter.ShootingOrigin.position;
    }
    public override void OnTriggerEnter(Collider other)
    {
        //base.OnTriggerEnter(other);
        AOEDamage(transform.position, AOERadius);
        base.ProjectileImpact();
    }
    private void AOEDamage(Vector3 center, float radius)
    {
        Collider[] AOETargets = Physics.OverlapSphere(center, radius);
        {
            foreach (var hitTarget in AOETargets)
            {
                base.OnTriggerEnter(hitTarget);
            }
        }
    }
   
}
