using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class OnStateFinished : SMB_Extension<MonoBehaviour>
{
    [SerializeField] protected bool finished;

    public bool Finished { get => finished; set => finished = value; }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateExit(animator, stateInfo, layerIndex);
        finished = false;
    }

    public override void OnSLStateFinished(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateFinished(animator, stateInfo, layerIndex);
        finished = true;
    }
}
