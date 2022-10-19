using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskGoToTarget : Node
{
    protected Animator animator;
    protected NavMeshAgent agent;
    protected EntityAI entity;

    public TaskGoToTarget(EntityAI entity)
    {
        this.animator = entity.Animator;
        this.agent = entity.Agent;
        this.entity = entity;
    }

    public override NodeState Evaluate()
    {
        if (!animator.GetBool("Attacking"))
            agent.isStopped = false;

        agent.SetDestination(entity.CurrentTarget.transform.position);

        animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);

        state = NodeState.RUNNING;
        return state;
    }
}

