using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class InAttackRange : Conditional
{
    [SerializeField] private SharedEntity _target;
    [SerializeField] private SharedFloat _attackRange;

	public override TaskStatus OnUpdate()
	{
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, _attackRange.Value);
        foreach (Collider collider in hitColliders)
        {

            if (collider.TryGetComponent<Entity>(out Entity target))
            {
                if (_target.Value == target)
                {
                    return TaskStatus.Success;
                }
            }
        }
        return TaskStatus.Failure;
    }
}