using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckDeath : Node
{
    protected EntityAI entity;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected Collider collider;

    public CheckDeath(EntityAI entity)
    {
        this.entity = entity;
        this.agent = entity.Agent;
        this.animator = entity.Animator;
    }

    public override NodeState Evaluate()
    {
        if (entity.Death)
        {
            agent.isStopped = true;
            animator.SetTrigger("Death");

            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}


