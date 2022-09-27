using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    
    [SerializeField] protected float damage;
    [SerializeField] protected ParticleSystem hitEffect;
    [SerializeField] protected AudioClip hitSound;

    protected Entity holder;

    public virtual void OnTriggerEnter(Collider collider)
    {
        // Can't stab yourself
        if (collider.gameObject == holder)
            return;

        if (collider.GetComponent<Entity>().Type == holder.Type)
            return;

        if (collider.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(damage, holder);
        }
    }

    public virtual void Initialize(Entity shooter, float damage)
    {
        this.holder = shooter;
        this.damage = damage;
    }
}
