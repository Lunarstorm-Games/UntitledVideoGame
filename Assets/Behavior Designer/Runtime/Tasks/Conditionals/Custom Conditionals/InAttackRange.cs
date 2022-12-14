using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class InAttackRange : Conditional
{
    [SerializeField] private SharedEntity _target;
    [SerializeField] private SharedFloat _attackRange;
    [SerializeField] private SharedFloat speedAnimParam;


    private Animator animator;
    private float speedTransitionTime = 0.0f;

    public override void OnEnd()
    {
        speedTransitionTime = 0.0f;
    }

    public override void OnStart()
    {
        animator = GetComponent<Animator>();
    }

    public override TaskStatus OnUpdate()
	{
        if (_target == null) return TaskStatus.Failure;


        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, _attackRange.Value);
        foreach (Collider collider in hitColliders)
        {

            if (collider.TryGetComponent<Entity>(out Entity target))
            {
                if (_target.Value == target)
                {
                    Debug.Log("range attack");
                    float anim_speed = Mathf.Lerp(speedAnimParam.Value, 0f, speedTransitionTime);
                    speedTransitionTime += 0.8f * Time.deltaTime;
                    speedAnimParam.Value = anim_speed;
                    animator.SetFloat("Speed", anim_speed);
                    return TaskStatus.Success;
                }
            }
        }
        return TaskStatus.Failure;
    }

}