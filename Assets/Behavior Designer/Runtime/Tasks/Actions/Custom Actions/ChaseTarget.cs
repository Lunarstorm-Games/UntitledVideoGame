using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Unity.VisualScripting;
using UnityEngine.AI;

public class ChaseTarget : Action
{
    [SerializeField] private SharedFloat speed;
    [SerializeField] private SharedFloat stoppingDistance;
    [SerializeField] private SharedTransform targetSpot;
    [SerializeField] private SharedEntity _target;

    private NavMeshAgent agent;
    private Entity unit;
    private Animator animator;

    public override void OnStart()
    {
        agent = GetComponent<NavMeshAgent>();
        unit = GetComponent<Entity>();
        animator = GetComponent<Animator>();

        if (agent == null) return;

        if (_target.Value == null) return;

        agent.isStopped = false;
        agent.speed = speed.Value;
        agent.stoppingDistance = stoppingDistance.Value;


        targetSpot.SetValue(_target.Value.GetEntityTargetSpot());
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

        if (targetSpot == null || targetSpot.Value.IsDestroyed()) return TaskStatus.Failure;

        agent.SetDestination(targetSpot.Value.position);


        if (!agent.pathPending)
            if (!agent.isOnOffMeshLink)
                if (agent.remainingDistance <= agent.stoppingDistance)
                    return TaskStatus.Success;

        animator.SetFloat("Speed", 1f);

        return TaskStatus.Running;
    }
}