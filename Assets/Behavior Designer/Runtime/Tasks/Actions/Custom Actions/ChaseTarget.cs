using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class ChaseTarget : Action
{
    [SerializeField] private SharedFloat speed;
    [SerializeField] private SharedFloat stoppingDistance;
    [SerializeField] private SharedEntity _target;

    private NavMeshAgent agent;
    private Entity unit;




    public override void OnAwake()
    {
        agent = GetComponent<NavMeshAgent>();
        unit = GetComponent<Entity>();
    }

    public override void OnStart()
    {
        if (agent == null) return;

        if (_target.Value == null) return;

        agent.isStopped = false;
        agent.speed = speed.Value;
        agent.stoppingDistance = stoppingDistance.Value;

        Transform targetSpot = _target.Value.GetEntityTargetSpot();

        agent.SetDestination(targetSpot.position);
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
        if (_target.Value == null)
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