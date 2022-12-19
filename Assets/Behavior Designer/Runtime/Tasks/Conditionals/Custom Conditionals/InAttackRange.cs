using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class InAttackRange : Conditional
{
    [SerializeField] private SharedEntity _target;
    [SerializeField] private SharedFloat _attackRange;
    [SerializeField] private SharedBool inAttack;

    private Animator animator;
    private float speedTransitionTime;
    private float animSpeed;
    private float currentAnimSpeed;


    public override void OnStart()
    {
        animator = GetComponent<Animator>();
        speedTransitionTime = 0f;
    }

    public override TaskStatus OnUpdate()
	{
        if (_target == null) return TaskStatus.Failure;

        if (inAttack.Value) return TaskStatus.Running;

        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, _attackRange.Value);
        foreach (Collider collider in hitColliders)
        {

            if (collider.TryGetComponent<Entity>(out Entity target))
            {
                if (_target.Value == target)
                {
                    //animSpeed = Mathf.Lerp(1, 0f, speedTransitionTime);
                    //speedTransitionTime += 0.95f * Time.deltaTime;
                    //animator.SetFloat("Speed", animSpeed);
                    animator.SetFloat("Speed", 0f);
                    return TaskStatus.Success;
                }
            }
        }

        return TaskStatus.Failure;
    }

}