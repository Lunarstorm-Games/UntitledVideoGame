using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;

public class LichSummon : Action
{
    [SerializeField] private SharedListEntity minions;
	[SerializeField] private SharedFloat summonDelay;
	[SerializeField] private SharedInt summonAmount;

	private float currentSummonDelay;
	private Animator animator;


	public override void OnStart()
	{
		animator = GetComponent<Animator>();
		currentSummonDelay = summonDelay.Value;
		animator.SetTrigger("Summon");
	}

	public override TaskStatus OnUpdate()
	{
		if (minions == null || minions.Value.Count == 0)
			return TaskStatus.Failure;

		currentSummonDelay -= Time.deltaTime;
		if (currentSummonDelay < 0f)
		{
			for (int i = 0; i < summonAmount.Value; i++)
			{
				GameObject.Instantiate(minions.Value[Random.Range(0, minions.Value.Count)], this.transform.position, Quaternion.identity);
			}
			currentSummonDelay = summonDelay.Value;
			return TaskStatus.Success;
		}
		else
		{
			return TaskStatus.Running;
		}
		
		

	}
}