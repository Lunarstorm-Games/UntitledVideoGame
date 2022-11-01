using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskRandomPatroll : Node
{
    protected Animator animator;
    protected NavMeshAgent agent;
    protected Vector3 startingPos;

    public TaskRandomPatroll(EntityAI entity)
    {
        this.animator = entity.Animator;
        this.agent = entity.Agent;
        this.startingPos = entity.transform.position;
    }

    public override NodeState Evaluate()
    {
        if (agent.hasPath && agent.remainingDistance <= agent.stoppingDistance + 0.1f)
        {
            agent.ResetPath();
            state = NodeState.SUCCESS;
            return state;
        }
        else if (!agent.hasPath)
        {
            Vector3 target = new Vector3(Random.Range(startingPos.x - 15, startingPos.x + 15), startingPos.y, Random.Range(startingPos.z - 15, startingPos.z + 15));
            agent.SetDestination(target);
        }

        agent.isStopped = false;
        animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);

        state = NodeState.RUNNING;
        return state; 
    }


}


