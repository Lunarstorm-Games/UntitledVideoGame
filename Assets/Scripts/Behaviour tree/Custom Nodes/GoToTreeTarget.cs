using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GoToTreeTarget : Node
{
    protected Animator animator;
    protected NavMeshAgent agent;
    protected Enemy enemy;
    protected Entity tree;

    public GoToTreeTarget(Enemy enemy)
    {
        this.animator = enemy.Animator;
        this.agent = enemy.Agent;
        this.enemy = enemy;
        this.tree = GameObject.FindObjectOfType<TreeOfLife>();
    }

    public override NodeState Evaluate()
    {
        if (tree != null && tree.gameObject.activeInHierarchy)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);
            enemy.CurrentTarget = tree;
            agent.isStopped = false;
            agent.SetDestination(enemy.CurrentTarget.transform.position);

            state = NodeState.RUNNING;
            return state;
        }

        state = NodeState.FAILURE;
        return state;

    }
}