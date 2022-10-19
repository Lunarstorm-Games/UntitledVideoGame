
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckTargetInAggroRange : Node
{
    protected EntityAI entity;
    protected float aggroRange;

    public CheckTargetInAggroRange(EntityAI entity)
    {
        this.entity = entity;
        this.aggroRange = entity.AggroRange;
    }

    public override NodeState Evaluate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(entity.transform.position, aggroRange);
        foreach (Collider collider in hitColliders)
        {
            if (collider.TryGetComponent<Entity>(out Entity target))
            {
                if (entity.ValidTarget(target.Type))
                {
                    entity.CurrentTarget = target;
                    state = NodeState.SUCCESS;
                    return state;
                }
            }
        }
        state = NodeState.FAILURE;
        return state;
    }
}

