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
	[SerializeField] private SharedFloat attackSpeed;
    [SerializeField] private SharedInt fireballAmount;

	private Animator animator;
	private float animDuration;
	private float fireRate;
	private float currentFireRateTime;
	private int currentShotAmount;


    public override void OnStart()
	{
		targetSpot.SetValue(Player.Instance.GetEntityTargetSpot());

		animator = GetComponent<Animator>();
		animator.SetFloat("FireballAttackSpeed", attackSpeed.Value);
		animator.SetTrigger("FireballAttack");
		currentShotAmount = fireballAmount.Value;
		animDuration = animator.runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == "Fly Flame Attack").length / attackSpeed.Value;
		animDuration -= 1f / attackSpeed.Value;
		currentFireRateTime = (1f / attackSpeed.Value) * attackSpeed.Value;
		fireRate = animDuration / fireballAmount.Value;
	}

	public override TaskStatus OnUpdate()
	{
		Vector3 dir = targetSpot.Value.position - unit.Value.transform.position;
		dir.y = 0f;
		unit.Value.transform.rotation = Quaternion.LookRotation(dir);

		currentFireRateTime -= Time.deltaTime;
		if (currentFireRateTime < 0f && currentShotAmount > 0)
		{
			unit.Value.GetComponent<Boss>().FireballProjectile();
			currentShotAmount--;
			currentFireRateTime = fireRate;
			if (currentShotAmount == 0)
            {
				return TaskStatus.Success;
			}
		}
		return TaskStatus.Running;
	}
}
