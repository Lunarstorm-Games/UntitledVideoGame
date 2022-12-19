using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SwitchStateBehavior : SMB_Extension<MonoBehaviour>
{
    [SerializeField] private string ParamName;
    [SerializeField] private float ParamFloatValue;

    public override void OnSLStateFinished(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        base.OnSLStateFinished(animator, stateInfo, layerIndex, controller);
        BehaviorTree behaviorTree = animator.GetComponent<BehaviorTree>();
        behaviorTree.SetVariableValue("Switching States", false);
        behaviorTree.SetVariableValue("CanSwitchState", false);
    }

    public override void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        base.OnSLStatePostEnter(animator, stateInfo, layerIndex, controller);
        animator.SetFloat(ParamName, ParamFloatValue);
        BehaviorTree behaviorTree = animator.GetComponent<BehaviorTree>();
        behaviorTree.SetVariableValue("Switching States", true);

    }
}
