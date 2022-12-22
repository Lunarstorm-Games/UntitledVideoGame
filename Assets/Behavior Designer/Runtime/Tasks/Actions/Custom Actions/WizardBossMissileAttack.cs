using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;

public class WizardBossMissileAttack : Action
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
		animator.SetFloat("MagicMissileAttackSpeed", attackSpeed.Value);
	}

	public override TaskStatus OnUpdate()
	{
		if (currentAttackDelay.Value <= 0f)
		{
			if (!inAttack.Value)
			{
				inAttack.Value = true;
				agent.isStopped = true;
				animator.SetTrigger("MagicMissileAttack");
				StartCoroutine(SummonProjectiles());
				return TaskStatus.Running;
			}
			else if (summonEnd)
            {
				animator.SetTrigger("MagicMissileEnd");
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
			Vector3 spawnPos = new Vector3(
				transform.position.x + summonRadius.Value * Mathf.Cos(2 * Mathf.PI * currentMissileAmount / missileAmount.Value), 
				transform.position.y + 3f, 
				transform.position.z + summonRadius.Value * Mathf.Sin(2 * Mathf.PI * currentMissileAmount / missileAmount.Value));

			summonedProjectile.Add(unit.Value.GetComponent<WizardBoss>().MissileProjectile(spawnPos));
			currentMissileAmount--;
			yield return new WaitForSeconds((fireRate.Value / attackSpeed.Value));
			
		}
		foreach (Projectile item in summonedProjectile)
		{
			if (item != null)
				item.GetComponent<NavMeshAgent>().enabled = true;
		}
		summonedProjectile.Clear();
		summonEnd = true;
        
	}
}