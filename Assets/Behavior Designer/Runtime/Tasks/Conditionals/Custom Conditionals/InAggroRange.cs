using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class InAggroRange : Conditional
{
    [SerializeField] private SharedEntity _target;
    [SerializeField] private SharedFloat _aggroRange;

    private Entity unit;

    public override void OnStart()
    {
        unit = GetComponent<Entity>();
    }

    public override TaskStatus OnUpdate()
	{
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, _aggroRange.Value);
        foreach (Collider collider in hitColliders)
        {
            if (collider.TryGetComponent<Entity>(out Entity target))
            {
                if (unit.IsValidTarget(target.EntityType))
                {
                    _target.SetValue(target);
                    return TaskStatus.Success;
                }
                
            }
        }
        return TaskStatus.Failure;

    }
}