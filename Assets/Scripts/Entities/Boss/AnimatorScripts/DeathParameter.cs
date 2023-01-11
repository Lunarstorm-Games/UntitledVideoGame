using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DeathParameter : SMB_Extension<MonoBehaviour>
{
    [SerializeField] private string ParamName;
    [SerializeField] private float ParamFloatValue;

    public override void OnSLStateFinished(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        animator.ResetTrigger("Death");
    }
}
