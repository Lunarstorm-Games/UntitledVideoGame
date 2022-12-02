using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Projectile : Weapon
{
    
    [SerializeField] protected VisualEffect impactEffect;
    [SerializeField] protected AudioClip hitSound;
    [SerializeField] public float mana;
    
    public int DamageLevel = 1;
    public int SpeedLevel = 1;

    public float speed;
    public float damage;
    public float range;
    protected Entity shooter;
    protected Vector3 direction;

    public virtual void Start()
    {
        DestroyProjectile(range);
    }

    public virtual void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public virtual void OnTriggerEnter(Collider collider)
    {

        if (collider.TryGetComponent<Entity>(out Entity entity))
        {
            if (entity == shooter)
                return;

            if (!shooter.ValidTarget(entity.Type)) 
                return;

            if (collider.TryGetComponent<IDamageable>(out IDamageable target))
            {
                if (shooter.IsValidTarget(entity.EntityType))
                {
                    if (collider.TryGetComponent<IDamageable>(out IDamageable target))
                    {
                        target.TakeDamage(damage, shooter);
                    }
                }
            }  
        }
        
        ProjectileImpact();
    }

    public virtual void Initialize(Entity shooter, Vector3 direction)
    {
        this.shooter = shooter;
        this.direction = direction;
    }

    public void UpgradeDamage(float newDamage)
    {
        DamageLevel += 1;
        damage += newDamage;
    }

    public void UpgradeSpeed(float newSpeed)
    {
        SpeedLevel += 1;
        speed += newSpeed;
    }

    protected virtual void DestroyProjectile(float delay = 0f)
    {
        Destroy(gameObject, delay);
    }

    protected virtual void ProjectileImpact()
    {
        if (impactEffect != null)
        {
            VisualEffect impactEffectObject = Instantiate(impactEffect, this.transform.position, Quaternion.identity);
            Destroy(impactEffectObject.gameObject, 1);
        }

        DestroyProjectile();
    }
}
