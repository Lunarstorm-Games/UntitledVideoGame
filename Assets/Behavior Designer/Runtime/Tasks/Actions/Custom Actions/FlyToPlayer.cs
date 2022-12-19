using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class FlyToPlayer : Action
{
    [SerializeField] private SharedFloat speed;
    [SerializeField] private SharedFloat stoppingDistance;
    [SerializeField] private SharedTransform targetSpot;
    [SerializeField] private SharedEntity target;
    [SerializeField] private SharedEntity unit;

    private Entity player;
    private NavMeshAgent agent;
    private Animator animator;


    public override void OnStart()
    {
        unit = GetComponent<Entity>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = Player.Instance;

        if (!agent) return;

        if (player == null) return;

        if (!unit.Value.IsValidTarget(player.EntityType)) return;

        target.SetValue(player);

        agent.isStopped = false;
        agent.speed = speed.Value;
        agent.stoppingDistance = stoppingDistance.Value;

        targetSpot.SetValue(player.GetEntityTargetSpot());

        agent.SetDestination(targetSpot.Value.position);

        
    }



    public override TaskStatus OnUpdate()
    {
        if (!agent)
        {
            Debug.LogWarning("NavAgent is null");
            return TaskStatus.Failure;
        }
        if (player == null)
        {
            Debug.LogWarning("Target is null");
            return TaskStatus.Failure;
        }

        agent.SetDestination(targetSpot.Value.position);

        //float anim_speed = Mathf.Lerp(0f, 1f, speedTransitionTime);
        //speedTransitionTime += 0.8f * Time.deltaTime;
        animator.SetFloat("Speed", 1f);

        if (!agent.pathPending)
            if (!agent.isOnOffMeshLink)
                if (agent.remainingDistance <= agent.stoppingDistance)
                    return TaskStatus.Success;

        return TaskStatus.Running;
    }
}