//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class TaskGoToTarget : Node
//{
//    protected Animator animator;
//    protected NavMeshAgent agent;
//    protected EntityAI entity;

//    public TaskGoToTarget(EntityAI entity)
//    {
//        this.animator = entity.Animator;
//        this.agent = entity.Agent;
//        this.entity = entity;
//    }

//    public override NodeState Evaluate()
//    {
//        if (!animator.GetBool("Attacking"))
//            agent.isStopped = false;

//        entity.TargetSpot = entity.CurrentTarget.GetEntityTargetSpot();
//        agent.SetDestination(entity.TargetSpot.position);
//        animator.SetFloat("Speed", 1f);

//        state = NodeState.RUNNING;
//        return state;
//    }
//}

