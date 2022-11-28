using BehaviorTreeVincent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich : RangeEnemy
{
    public override void Awake()
    {
        base.Awake();
        Tree = new LichTree(this);
        Tree.Initialize();
    }
    public virtual void Update()
    {
        Tree.Evaluate();
    }
}
