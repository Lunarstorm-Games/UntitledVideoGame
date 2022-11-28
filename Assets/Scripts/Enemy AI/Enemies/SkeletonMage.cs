using BehaviorTreeVincent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMage : RangeEnemy
{

    public override void Awake()
    {
        base.Awake();
        Tree = new SkeletonMageTree(this);
        Tree.Initialize();
    }
    public virtual void Update()
    {
        Tree.Evaluate();
    }
}
