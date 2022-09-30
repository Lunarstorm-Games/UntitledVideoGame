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
        Debug.Log(collider.name, collider.gameObject);
        if (collider.TryGetComponent<Entity>(out Entity entity))
        {
            if (entity == holder)
                return;

            if (!holder.ValidTarget(holder.TargetsType, entity.Type))
                return;

            if (collider.TryGetComponent<IDamageable>(out IDamageable target))
            {
                target.TakeDamage(damage, holder);
            }
        }
    }

   

    //public virtual void OnTriggerExit(Collider collider)
    //{

    //}

    public virtual void Initialize(Entity holder)
    {
        this.holder = holder;
    }
}
