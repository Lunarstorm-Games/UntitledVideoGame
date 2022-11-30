using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public enum ValueType
{
    Boolean,
    Int,
    Float,
    Trigger,
}

public class SetBehaviourValue : SMB_Extension<MonoBehaviour>
{
    [SerializeField] private string ParamName;
    [SerializeField] private ValueType valueType;
    [SerializeField] private bool ParamBoolValue;
    [SerializeField] private int ParamIntValue;
    [SerializeField] private float ParamFloatValue;

    public override void OnSLStatePostEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        base.OnSLStatePostEnter(animator, stateInfo, layerIndex, controller);
        switch (valueType)
        {
            case ValueType.Boolean:
                animator.SetBool(ParamName, ParamBoolValue);
                break;
            case ValueType.Int:
                animator.SetInteger(ParamName, ParamIntValue);
                break;
            case ValueType.Float:
                animator.SetFloat(ParamName, ParamFloatValue);
                break;
            case ValueType.Trigger:
                animator.SetTrigger(ParamName);
                break;
            default:
                animator.SetTrigger(ParamName);
                break;

        }
    }
}
