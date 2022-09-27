using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Projectile : MonoBehaviour
{
                     
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected float range;
    [SerializeField] protected VisualEffect impactEffect;
    [SerializeField] protected AudioClip hitSound;

    protected Entity shooter;
    protected Vector3 direction;

    public virtual void Start()
    {
        DestroyProjectile(range);
    }

    public virtual void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        // Can't shoot yourself
        if (other.gameObject == shooter)
            return;

        if (other.TryGetComponent(out IDamageable target) && other.GetComponent<Entity>().Type != shooter.Type)
        {
            target.TakeDamage(damage, shooter);
        }
        DestroyProjectile();
    }

    public virtual void Initialize(Entity shooter, Vector3 direction)
    {
        this.shooter = shooter;
        this.direction = direction;
    }

    protected virtual void DestroyProjectile(float delay = 0f)
    {
        if (hitSound != null)
            //SoundManager.Instance.PlaySoundAtLocation();
        
        if (impactEffect != null)
        {
            //VisualEffect impactEffectObject = Instantiate(impactEffect, this.transform.position, Quaternion.identity);
            //Destroy(impactEffectObject.gameObject, 1);
        }
        
       Destroy(gameObject, delay);
    }
}
