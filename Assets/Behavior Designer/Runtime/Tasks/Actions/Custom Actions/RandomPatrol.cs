using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class RandomPatrol : Action
{
    [SerializeField] private SharedFloat patrolRange;
    private Animator animator;
    private NavMeshAgent agent;
    private Vector3 startPos;

	public override void OnStart()
	{
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position;
	}

	public override TaskStatus OnUpdate()
	{
        
		if (agent.hasPath && agent.remainingDistance <= agent.stoppingDistance + 0.1f)
        {
            agent.ResetPath();
            animator.SetFloat("Speed", 0f);
            return TaskStatus.Success;
        }
        else if (!agent.hasPath)
        {
            Vector3 target = new Vector3(Random.Range(startPos.x - patrolRange.Value, startPos.x + patrolRange.Value), startPos.y, Random.Range(startPos.z - patrolRange.Value, startPos.z + patrolRange.Value));
            agent.SetDestination(target);
        }

        agent.isStopped = false;
        animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);

        return TaskStatus.Running;

    }
}