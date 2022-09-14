using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    public override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        base.Attack();
    }

    protected override void Chase()
    {
        base.Chase();
    }

    protected override bool InAttackRange()
    {
        return base.InAttackRange();
    }

    protected override bool LineOfSight()
    {
        return base.LineOfSight();
    }

    protected override void Patrol()
    {
        base.Patrol();
    }
}
