using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    public EnemyTree Tree { get; protected set; }

    public override void Awake()
    {
        base.Awake();
        Tree = new EnemyTree(this);
        Tree.Initialize();
    }

    public override void Update()
    {
        base.Update();
        Tree.Evaluate();
    }

    public override void TakeDamage(float damage, Entity entity)
    {
        base.TakeDamage(damage, entity);
    }
}
