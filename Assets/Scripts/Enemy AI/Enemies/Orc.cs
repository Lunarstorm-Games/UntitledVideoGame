using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MeleeEnemy
{
    public override void Awake()
    {
        base.Awake();
        Tree = new OrcTree(this);
        Tree.Initialize();
    }

    public virtual void Update()
    {
        Tree.Evaluate();
        
    }
}
