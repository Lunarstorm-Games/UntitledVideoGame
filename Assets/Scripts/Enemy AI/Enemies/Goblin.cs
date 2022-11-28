using BehaviorTreeVincent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MeleeEnemy
{
    public override void Awake()
    {
        base.Awake();
        Tree = new GoblinTree(this);
        Tree.Initialize();
    }

    public virtual void Update()
    {
        Tree.Evaluate();
    }
}
