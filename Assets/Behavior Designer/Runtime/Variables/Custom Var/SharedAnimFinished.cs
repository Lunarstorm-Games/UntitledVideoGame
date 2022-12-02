using UnityEngine;
using BehaviorDesigner.Runtime;

[System.Serializable]
public class SharedAnimFinished : SharedVariable<OnStateFinished>
{
	public static implicit operator SharedAnimFinished(OnStateFinished value) { return new SharedAnimFinished { mValue = value }; }
}