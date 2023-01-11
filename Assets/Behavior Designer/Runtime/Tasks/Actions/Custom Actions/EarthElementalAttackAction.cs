using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EarthElementalAttackAction : Action
{
	[SerializeField] private SharedEntity unit;
	[SerializeField] private SharedTransform targetSpot;
	[SerializeField] private SharedFloat attackSpeed, attackRange, attackDelay, damage, frontlineDamageMultiplier, currentAttackDelay;

	private Animator animator;
	private EarthElementalAttack weapon;

	public override void OnStart()
	{
		animator = GetComponent<Animator>();
		animator.SetFloat("AttackSpeed", attackSpeed.Value);
		weapon = gameObject.GetComponentInChildren<EarthElementalAttack>();
		gameObject.GetComponentInChildren<SphereCollider>().radius = attackRange.Value;
		weapon.Initialize(unit.Value, damage.Value);
		weapon.DamageMultiplier = frontlineDamageMultiplier.Value;
	}

	public override TaskStatus OnUpdate()
	{
		Vector3 dir = targetSpot.Value.position - unit.Value.transform.position;
		dir.y = 0f;
		unit.Value.transform.rotation = Quaternion.LookRotation(dir);

		currentAttackDelay.Value -= Time.deltaTime;
		if (currentAttackDelay.Value < 0f)
		{

			animator.SetTrigger("Attack");
			currentAttackDelay.SetValue(attackDelay.Value);
			return TaskStatus.Success;
		}
		else
		{
			return TaskStatus.Running;
		}
	}
}