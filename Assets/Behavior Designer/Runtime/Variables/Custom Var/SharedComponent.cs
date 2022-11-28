using UnityEngine;
using BehaviorDesigner.Runtime;

[System.Serializable]
public class SharedComponent : SharedVariable<Component>
{
	public override string ToString() { return mValue == null ? "null" : mValue.ToString(); }
	public static implicit operator SharedComponent(Component value) { return new SharedComponent { mValue = value }; }
}