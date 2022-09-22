using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    [SerializeField] public Projectile projectilePrefab;
    [SerializeField] public Transform projectileSpawnPos;
    [SerializeField] public float projectileSpeed;
    [SerializeField] public float projectileRange;

    public override void Awake()
    {
        base.Awake();
    }

    public override void DeathAnimFinished()
    {
        base.DeathAnimFinished();
    }

    public override void DropEssence()
    {
        base.DropEssence();
    }

    public override void TakeDamage(float damage, Entity entity)
    {
        base.TakeDamage(damage, entity);
    }

    public override void Update()
    {
        base.Update();
    }
}
