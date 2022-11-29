//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class BossNodeTest : Node
//{
//    protected Animator animator;
//    protected NavMeshAgent agent;
//    protected Entity tree;
//    protected Boss boss;

//    public BossNodeTest(Boss boss)
//    {
//        this.animator = boss.Animator;
//        this.agent = boss.Agent;
//        this.boss = boss;
//        this.tree = GameObject.FindObjectOfType<TreeOfLife>();
//    }

//    public override NodeState Evaluate()
//    {
//        if (tree != null && tree.gameObject.activeInHierarchy && boss.ValidTarget(tree.EntityType))
//        {
//            animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);
//            boss.CurrentTarget = tree;
//            agent.isStopped = false;
            
//            agent.SetDestination(boss.CurrentTarget.transform.position);

//            state = NodeState.RUNNING;
//            return state;
//        }

//        state = NodeState.FAILURE;
//        return state;
//    }
//}
