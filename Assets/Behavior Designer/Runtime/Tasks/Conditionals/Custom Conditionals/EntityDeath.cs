using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class EntityDeath : Conditional
{
	[SerializeField] private SharedBool isDeath;
	private Animator animator;
	private NavMeshAgent agent;

    public override void OnAwake()
    {
        animator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
    }

	public override TaskStatus OnUpdate()
	{
		if (isDeath.Value)
        {
			agent.isStopped = true;
			animator.SetTrigger("Death");
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}