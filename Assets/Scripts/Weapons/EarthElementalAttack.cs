using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthElementalAttack : MeleeWeapon
{
    public float DamageMultiplier;

    public override void OnTriggerEnter(Collider collider)
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

}
