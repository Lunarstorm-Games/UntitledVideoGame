using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskIdle : Node
{
    protected Animator animator;
    protected NavMeshAgent agent;

    public TaskIdle(EntityAI entity)
    {
        this.animator = entity.Animator;
        this.agent = entity.Agent;
    }

    public override NodeState Evaluate()
    {
        animator.SetFloat("Speed", 0f);
        agent.isStopped = true;

        state = NodeState.SUCCESS;
        return state;
    }
}

