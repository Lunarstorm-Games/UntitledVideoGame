using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] public float Damage;
    public override void Awake()
    {
        base.Awake();
    }

    public override void DeathAnimEvent()
    {
        base.DeathAnimEvent();
    }

    public override void DropEssence()
    {
        base.DropEssence();
    }

    public override void TakeDamage(float damage, Entity entity)
    {
        base.TakeDamage(damage, entity);
    }
}
