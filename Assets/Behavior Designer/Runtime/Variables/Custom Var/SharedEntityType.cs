using UnityEngine;
using BehaviorDesigner.Runtime;

[System.Serializable]
public class SharedEntityType : SharedVariable<EntityType>
{
	public override string ToString() { return mValue == EntityType.None ? "None" : mValue.ToString(); }
	public static implicit operator SharedEntityType(EntityType value) { return new SharedEntityType { mValue = value }; }
}