using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using System.Linq;

public class DragonJumpAttack : Action
{
	[SerializeField] private SharedEntity unit;
	[SerializeField] private SharedFloat speed;
	[SerializeField] private SharedFloat stoppingDistance;
	[SerializeField] private SharedFloat attackSpeed, damage;
	[SerializeField] private SharedTransform targetSpot;

	private NavMeshAgent agent;
	private Animator animator;
	private MeleeWeapon weapon;
	private float animDuration;
	private float currentAnimDuration;
	private bool performedAttack;

	public override void OnStart()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		weapon = gameObject.GetComponentInChildren<DragonJump>();
		weapon.Initialize(unit.Value, damage.Value);

		animDuration = animator.runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == "Claw Attack").length / attackSpeed.Value;
		currentAnimDuration = animDuration;
		
		agent.isStopped = false;
		agent.stoppingDistance = 1f;
		agent.speed = speed.Value * attackSpeed.Value * 2;
		agent.ResetPath();
		animator.SetFloat("LungeAttackSpeed", attackSpeed.Value);
		animator.SetTrigger("LungeAttack");
	}

	public override TaskStatus OnUpdate()
	{
		currentAnimDuration -= Time.deltaTime;

		if (!performedAttack)
		{
			Vector3 dir = targetSpot.Value.position - unit.Value.transform.position;
			dir.y = 0f;
			unit.Value.transform.rotation = Quaternion.LookRotation(dir);

			if (currentAnimDuration <= 2f / attackSpeed.Value)
			{
				performedAttack = true;
				agent.SetDestination(targetSpot.Value.position);
			}
		}
		

		if (agent.hasPath) 
			if (!agent.pathPending)
				if (!agent.isOnOffMeshLink)
					if (agent.remainingDistance <= agent.stoppingDistance)
					{
						return TaskStatus.Success;
					}
					

		return TaskStatus.Running;
	}

	public override void OnEnd()
	{
		currentAnimDuration = animDuration;
		performedAttack = false;
		agent.isStopped = true;
		agent.stoppingDistance = stoppingDistance.Value;
		agent.speed = speed.Value;
	}

	public bool LineOfSight()
    {
		return false;
    }
}