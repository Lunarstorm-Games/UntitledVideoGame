using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DragonBiteAttack : Action
{
	[SerializeField] private SharedEntity unit;
	[SerializeField] private SharedTransform targetSpot;
	[SerializeField] private SharedFloat attackSpeed, attackDelay, damage, currentAttackDelay;
	[SerializeField] private SharedBool inAttack;

	private Animator animator;
	private MeleeWeapon weapon;
	private float animDuration;
	private float currentAnimDuration;


	public override void OnStart()
	{
		animator = GetComponent<Animator>();
		animator.SetFloat("BiteAttackSpeed", attackSpeed.Value);
		weapon = gameObject.GetComponentInChildren<DragonBite>();
		weapon.Initialize(unit.Value, damage.Value);
		animDuration = animator.runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == "Basic Attack").length / attackSpeed.Value;
		currentAnimDuration = animDuration;
	}

	public override TaskStatus OnUpdate()
	{
		if (targetSpot == null || targetSpot.Value.IsDestroyed()) return TaskStatus.Failure;
		Vector3 dir = targetSpot.Value.position - unit.Value.transform.position;
		dir.y = 0f;
		unit.Value.transform.rotation = Quaternion.LookRotation(dir);

		if (currentAttackDelay.Value <= 0f)
		{
			currentAnimDuration -= Time.deltaTime;
			animator.SetTrigger("BiteAttack");
			inAttack.Value = true;


			if (currentAnimDuration <= 0f / attackSpeed.Value)
			{
				currentAttackDelay.SetValue(attackDelay.Value);
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
			
		}
		return TaskStatus.Failure;

		
	}

	public override void OnEnd()
	{
		animator.ResetTrigger("BiteAttack");
		inAttack.Value = false;

	}
}
