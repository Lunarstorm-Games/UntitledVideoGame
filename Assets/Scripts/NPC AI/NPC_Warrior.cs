using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Warrior : MeleeNPC
{

    public override void Awake()
    {
        base.Awake();
        Tree = new NPC_Warrior_Tree(this);
        Tree.Initialize();
    }

    public virtual void Update()
    {
        Tree.Evaluate();
    }
}
