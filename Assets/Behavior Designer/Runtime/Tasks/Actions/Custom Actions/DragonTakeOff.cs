using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using System.Linq;

public class DragonTakeOff : Action
{
	[SerializeField] private SharedBool canSwitch;
	[SerializeField] private SharedBool flying;

	private Animator animator;
	private NavMeshAgent agent;
	private float animDuration;
	private float currentAnimDuration;

	public override void OnStart()
	{
		animator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		agent.isStopped = true;
		canSwitch.Value = false;
		animator.SetTrigger("TakeOff");

		flying.Value = true;
		animDuration = animator.runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == "Take Off").length;
		currentAnimDuration = animDuration;
	}

	public override TaskStatus OnUpdate()
	{
		currentAnimDuration -= Time.deltaTime;
		
		if (currentAnimDuration <= 0.8f)
        {
			animator.SetFloat("Flying", 1f);
			return TaskStatus.Success;
		}
		return TaskStatus.Running;
	}

	public override void OnEnd()
	{
		animator.ResetTrigger("TakeOff");
		currentAnimDuration = animDuration;
		agent.isStopped = false;
	}
}