//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;


//public class CheckTargetInAttackRange : Node
//{
//    protected EntityAI entity;
//    protected NavMeshAgent agent;
//    protected Animator animator;
//    protected float attackRange;

//    public CheckTargetInAttackRange(EntityAI entity)
//    {
//        this.attackRange = entity.AttackRange;
//        this.agent = entity.Agent;
//        this.animator = entity.Animator;
//        this.entity = entity;
//    }

//    public override NodeState Evaluate()
//    {
//        Collider[] hitColliders = Physics.OverlapSphere(entity.transform.position, attackRange);
//        foreach (Collider collider in hitColliders)
//        {
//            if (collider.TryGetComponent<Entity>(out Entity target))
//            {
//                if (target == entity.CurrentTarget)
//                {
//                    agent.isStopped = true;
//                    state = NodeState.SUCCESS;
//                    return state;
//                }
//            }
//        }

//        entity.CurrentAttackDelay = 0f;
//        animator.SetBool("PreAttack", false);
//        state = NodeState.FAILURE;
//        return state;
//    }

//}

