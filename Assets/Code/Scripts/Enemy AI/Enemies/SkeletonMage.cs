using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMage : RangeEnemy
{
    public SkeletonMageTree Tree { get; protected set; }

    public override void Awake()
    {
        base.Awake();
        Tree = new SkeletonMageTree(this);
        Tree.Initialize();
    }
    public override void Update()
    {
        base.Update();
        Tree.Evaluate();
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
}
