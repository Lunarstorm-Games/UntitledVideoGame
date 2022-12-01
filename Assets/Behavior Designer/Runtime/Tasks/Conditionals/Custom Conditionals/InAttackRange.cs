using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class InAttackRange : Conditional
{
    [SerializeField] private SharedEntity _target;
    [SerializeField] private SharedFloat _attackRange;

    private Animator animator;

    public override void OnStart()
    {
        animator = GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
	{
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, _attackRange.Value);
        foreach (Collider collider in hitColliders)
        {

            if (collider.TryGetComponent<Entity>(out Entity target))
            {
                if (_target.Value == target)
                {
                    animator.SetFloat("Speed", 0f);
                    return TaskStatus.Success;
                }
            }
        }
        return TaskStatus.Failure;
    }
}