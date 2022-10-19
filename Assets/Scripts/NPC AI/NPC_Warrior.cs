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

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, 15);
        Gizmos.DrawWireSphere(new Vector3(Random.Range(-15, 15), this.transform.position.y, Random.Range(-15, 15)), 2f);
        
    }
}
