using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public float range;
    [SerializeField] protected VisualEffect impactEffect;
    [SerializeField] protected AudioClip hitSound;
    
    public int DamageLevel = 1;
    public int SpeedLevel = 1;

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
        Debug.Log("before");
        // Can't shoot yourself
        if (other.gameObject == shooter.gameObject)
            return;
        Debug.Log("after");
        if (other.TryGetComponent(out IDamageable target) && other.GetComponent<Entity>().Type != shooter.Type)
        {
            target.TakeDamage(damage, shooter);
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
        if (hitSound != null)
        {
            //SoundManager.Instance.PlaySoundAtLocation();
        }

        if (impactEffect != null)
        {
            VisualEffect impactEffectObject = Instantiate(impactEffect, this.transform.position, Quaternion.identity);
            Destroy(impactEffectObject.gameObject, 1);
        }

        //Destroy(gameObject);
    }
}
