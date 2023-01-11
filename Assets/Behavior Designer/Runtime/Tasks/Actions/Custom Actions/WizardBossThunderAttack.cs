using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.VFX;

public class WizardBossThunderAttack : Action
{
	[SerializeField] private SharedEntity unit;
	[SerializeField] private SharedTransform targetSpot;
	[SerializeField] private SharedFloat attackSpeed, attackDelay, currentAttackDelay, fireRate, summonRadius;
	[SerializeField] private SharedInt missileAmount;
	[SerializeField] private SharedBool inAttack;

	private Animator animator;
	private NavMeshAgent agent;
	private int currentMissileAmount;
	private bool summonEnd;
	private List<Projectile> summonedProjectile = new List<Projectile>();

	public override void OnStart()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		animator.SetFloat("LightningAttackSpeed", attackSpeed.Value);
	}

	public override TaskStatus OnUpdate()
	{
		if (currentAttackDelay.Value <= 0f)
		{
			if (!inAttack.Value)
			{
				inAttack.Value = true;
				agent.isStopped = true;
				animator.SetTrigger("LightningAttack");
				StartCoroutine(SummonProjectiles());
				return TaskStatus.Running;
			}
			else if (summonEnd)
			{
				animator.SetTrigger("LightningEnd");
				agent.isStopped = false;
				inAttack.Value = false;
				summonEnd = false;
				currentAttackDelay.SetValue(attackDelay.Value);
				return TaskStatus.Success;
			}
			else
			{
				return TaskStatus.Running;
			}
		}
		return TaskStatus.Failure;
	}

	public IEnumerator SummonProjectiles()
	{
		currentMissileAmount = missileAmount.Value;
		while (currentMissileAmount > 0)
		{
			Vector3 targetPos = targetSpot.Value.position;	
			Vector3 spawnPos = new Vector3(Random.Range(targetPos.x, targetPos.x + summonRadius.Value), targetSpot.Value.gameObject.transform.position.y + 11f, Random.Range(targetPos.z, targetPos.z + summonRadius.Value));

			summonedProjectile.Add(unit.Value.GetComponent<WizardBoss>().ThunderProjectile(spawnPos));
			currentMissileAmount--;
			yield return new WaitForSeconds((fireRate.Value / attackSpeed.Value));

		}

		foreach (WizardBossLightningProjectile item in summonedProjectile)
		{
			if (item != null)
            {
				item.ThunderEffect.enabled = true;
				item.enabled = true;
				item.ThunderColl.enabled = true;

			}
				
		}
		summonedProjectile.Clear();
		summonEnd = true;

		

	}
}