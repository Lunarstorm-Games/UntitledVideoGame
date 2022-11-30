using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BaseRangeAttack : Action
{
	[SerializeField] private SharedEntity unit;
	[SerializeField] private SharedTransform targetSpot;
	[SerializeField] private SharedFloat attackSpeed, attackDelay;

	private Animator animator;
	private float currentAttackDelay;

	public override void OnAwake()
	{
		animator = GetComponent<Animator>();
		animator.SetFloat("AttackSpeed", attackSpeed.Value);
	}

	public override TaskStatus OnUpdate()
	{
		Vector3 dir = targetSpot.Value.position - unit.Value.transform.position;
		dir.y = 0f;
		unit.Value.transform.rotation = Quaternion.LookRotation(dir);

		currentAttackDelay -= Time.deltaTime;
		if (currentAttackDelay < 0f)
		{
			animator.SetTrigger("Attack");
			currentAttackDelay = attackDelay.Value;
			return TaskStatus.Success;
		}
		else
		{
			return TaskStatus.Running;
		}
	}
}