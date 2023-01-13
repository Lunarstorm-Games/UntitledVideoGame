using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeWeapon : Weapon
{

    [SerializeField] protected ParticleSystem hitEffect;
    [SerializeField] protected AudioClip hitSound;

    protected float damage;
    protected Entity holder;

    public virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Entity>(out Entity entity))
        {
            if (entity == holder)
                return;

            if (!holder.IsValidTarget(entity.EntityType))
                return;

            if (collider.TryGetComponent<IDamageable>(out IDamageable target))
            {
                target.TakeDamage(damage, holder);
            }
        }
    }

    public virtual void Initialize(Entity holder, float damage)
    {
        this.holder = holder;
        this.damage = damage;
    }
}
