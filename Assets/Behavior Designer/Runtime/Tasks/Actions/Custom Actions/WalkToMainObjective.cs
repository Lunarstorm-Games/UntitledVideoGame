using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkToMainObjective : Action
{
    [SerializeField] private SharedFloat speed;
    [SerializeField] private SharedFloat stoppingDistance;
    [SerializeField] private SharedTransform targetSpot;
    [SerializeField] private SharedEntity target;
    
    private Entity mainObjective;
    private NavMeshAgent agent;
    private Entity unit;
    private Animator animator;



    public override void OnAwake()
    {
        unit = GetComponent<Entity>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        mainObjective = GameObject.FindGameObjectWithTag("MainObjective").GetComponent<Entity>();
    }

    public override void OnStart()
    {
        if (agent == null) return;

        if (mainObjective == null) return;

        if (!unit.IsValidTarget(mainObjective.EntityType)) return;

        target.SetValue(mainObjective);

        agent.isStopped = false;
        agent.speed = speed.Value;
        agent.stoppingDistance = stoppingDistance.Value;

        targetSpot.SetValue(mainObjective.GetEntityTargetSpot());

        agent.SetDestination(targetSpot.Value.position);

        animator.SetFloat("Speed", 1f);
    }

    public override void OnEnd()
    {
        agent.isStopped = true;
    }



    public override TaskStatus OnUpdate()
    {
        if (agent == null)
        {
            Debug.LogWarning("NavAgent is null");
            return TaskStatus.Failure;
        }
        if (mainObjective == null)
        {
            Debug.LogWarning("Target is null");
            return TaskStatus.Failure;
        }

        if (!agent.pathPending)
            if (!agent.isOnOffMeshLink)
                if (agent.remainingDistance <= agent.stoppingDistance)
                    return TaskStatus.Success;

        return TaskStatus.Running;
    }
}
