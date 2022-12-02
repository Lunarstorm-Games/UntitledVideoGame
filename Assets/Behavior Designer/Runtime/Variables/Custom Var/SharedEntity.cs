using UnityEngine;
using BehaviorDesigner.Runtime;

[System.Serializable]
public class SharedEntity : SharedVariable<Entity>
{
	public override string ToString() { return mValue == null ? "null" : mValue.ToString(); }
	public static implicit operator SharedEntity(Entity value) { return new SharedEntity { mValue = value }; }
}