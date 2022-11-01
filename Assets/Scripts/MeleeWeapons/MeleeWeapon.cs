using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected ParticleSystem hitEffect;
    [SerializeField] protected AudioClip hitSound;

    protected Entity holder;

    public virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Entity>(out Entity entity))
        {
            if (entity == holder)
                return;

            if (!holder.ValidTarget(entity.Type))
                return;

            if (collider.TryGetComponent<IDamageable>(out IDamageable target))
            {
                Debug.Log("Hit: " + target, collider.gameObject);
                target.TakeDamage(damage, holder);
            }
        }
    }

    public virtual void Initialize(Entity holder)
    {
        this.holder = holder;
    }
}
