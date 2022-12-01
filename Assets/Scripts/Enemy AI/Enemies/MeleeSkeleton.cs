using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkeleton : MeleeEnemy
{
    

    public override void Awake()
    {
        base.Awake();
        
    }
    private void OnEnable()
    {
        Tree = new MeleeSkeletonTree(this);
        Tree.Initialize();
    }

    public virtual void Update()
    {
        Tree.Evaluate();
    }
}
