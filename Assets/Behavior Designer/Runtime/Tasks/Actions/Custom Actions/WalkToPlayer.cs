using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class WalkToPlayer : Action
{
    [SerializeField] private SharedFloat speed;
    [SerializeField] private SharedFloat stoppingDistance;
    [SerializeField] private SharedTransform targetSpot;
    [SerializeField] private SharedEntity target;
    [SerializeField] private SharedFloat speedAnimParam;
    [SerializeField] private SharedFloat test;

    private Entity player;
    private NavMeshAgent agent;
    private Entity unit;
    private Animator animator;
    private float speedTransitionTime = 0.0f;


    public override void OnStart()
    {
        unit = GetComponent<Entity>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = Player.Instance;

        if (!agent) return;

        if (player == null) return;

        if (!unit.IsValidTarget(player.EntityType)) return;

        target.SetValue(player);

        agent.isStopped = false;
        agent.speed = speed.Value;
        agent.stoppingDistance = stoppingDistance.Value;

        targetSpot.SetValue(player.GetEntityTargetSpot());

        
    }

    public override void OnEnd()
    {
        speedTransitionTime = 0.0f;
        if (agent)
            agent.isStopped = true;
    }


    public override TaskStatus OnUpdate()
    {
        Debug.Log(test.Value);
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


        float anim_speed = Mathf.Lerp(speedAnimParam.Value, 1f, speedTransitionTime);
        speedTransitionTime += 0.8f * Time.deltaTime;
        speedAnimParam.Value = anim_speed;
        animator.SetFloat("Speed", anim_speed);

        if (!agent.pathPending)
            if (!agent.isOnOffMeshLink)
                if (agent.remainingDistance <= agent.stoppingDistance)
                    return TaskStatus.Success;

        return TaskStatus.Running;
    }
}