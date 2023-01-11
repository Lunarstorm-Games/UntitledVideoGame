using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DragonRangeAttack : Action
{
	[SerializeField] private SharedEntity unit;
	[SerializeField] private SharedTransform targetSpot;
	[SerializeField] private SharedFloat attackSpeed, attackDelay, currentAttackDelay;
    [SerializeField] private SharedInt fireballAmount;
	[SerializeField] private SharedBool inAttack;

	private Animator animator;
	private float animDuration;
	private float fireRate;
	private float currentFireRateTime;
	private int currentShotAmount;
	private bool prepareAttack;


    public override void OnStart()
	{
		animator = GetComponent<Animator>();
		animator.SetFloat("FireballAttackSpeed", attackSpeed.Value);
		currentShotAmount = fireballAmount.Value;
		animDuration = animator.runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == "Fly Flame Attack").length / attackSpeed.Value;
		animDuration -= (1f / attackSpeed.Value);
		currentFireRateTime = 1f;
		fireRate = animDuration / fireballAmount.Value;
	}

	public override TaskStatus OnUpdate()
	{
		Vector3 dir = targetSpot.Value.position - unit.Value.transform.position;
		dir.y = 0f;
		unit.Value.transform.rotation = Quaternion.LookRotation(dir);

		if (currentAttackDelay.Value <= 0f)
		{
			if (!inAttack.Value)
			{
				animator.SetTrigger("FireballAttack");
				inAttack.Value = true;
			}

			currentFireRateTime -= Time.deltaTime;
			if (currentFireRateTime < 0f && currentShotAmount > 0)
			{
				unit.Value.GetComponent<DragonBoss>().FireballProjectile();
				currentShotAmount--;
				currentFireRateTime = fireRate;
				if (currentShotAmount == 0)
				{
					currentAttackDelay.SetValue(attackDelay.Value);
					return TaskStatus.Success;
				}
			}
			return TaskStatus.Running;
		}
		return TaskStatus.Failure;
	}

	public override void OnEnd()
	{
		animator.ResetTrigger("FireballAttack");
		inAttack.Value = false;
	}
}
