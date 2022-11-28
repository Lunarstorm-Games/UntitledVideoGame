using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkToMainObjective : Action
{
    [SerializeField] private float speed;
    [SerializeField] private float stoppingDistance;
    
    private GameObject test;
    private float oldSpeed;
    private float oldStoppingDistance;
    private NavMeshAgent agent;
    



    public override void OnAwake()
    {
        agent = GetComponent<NavMeshAgent>();
        test = GameObject.FindGameObjectWithTag("MainObjective");
    }

    public override void OnStart()
    {
        if (agent == null) return;

        if (test == null) return;

        oldSpeed = agent.speed;
        oldStoppingDistance = agent.stoppingDistance;

        agent.speed = speed;
        agent.stoppingDistance = stoppingDistance;

        agent.SetDestination(test.transform.position);
    }

    public override void OnEnd()
    {
        agent.speed = oldSpeed;
        agent.stoppingDistance = oldStoppingDistance;
    }



    public override TaskStatus OnUpdate()
    {
        if (agent == null)
        {
            Debug.LogWarning("NavAgent is null");
            return TaskStatus.Failure;
        }
        if (test == null)
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
