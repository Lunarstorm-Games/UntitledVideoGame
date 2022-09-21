using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{

    private Entity Origin;
    private float Speed;
    private float Damage;
    private Vector3 MoveDir;

    public virtual void Update()
    {
        transform.position += MoveDir * Speed * Time.deltaTime;
    }

    public virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<IDamageable>(out IDamageable target) && !collider.CompareTag("Enemy"))
        {
            target.TakeDamage(Damage, Origin);
        }
        Destroy(this.gameObject);
    }

    public void Initialize(Entity origin, float speed, float damage, Vector3 moveDir, float lifeTime)
    {
        this.Origin = origin;
        this.Speed = speed;
        this.Damage = damage;
        this.MoveDir = moveDir;
        Destroy(this.gameObject, lifeTime);
    }
}
