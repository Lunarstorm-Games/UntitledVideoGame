using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkeleton : MeleeEnemy
{
    public MeleeSkeletonTree Tree { get; protected set; }

    public override void Awake()
    {
        base.Awake();
        Tree = new MeleeSkeletonTree(this);
        Tree.Initialize();
    }

    public virtual void Update()
    {
        Tree.Evaluate();
    }

    public override void TakeDamage(float damage, Entity entity)
    {
        base.TakeDamage(damage, entity);
    }

    public override void DeathAnimEvent()
    {
        base.DeathAnimEvent();
    }

    public override void DropEssence()
    {
        base.DropEssence();
    }
}
