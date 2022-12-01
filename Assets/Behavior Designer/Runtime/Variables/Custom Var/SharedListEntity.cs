using UnityEngine;
using BehaviorDesigner.Runtime;
using System.Collections.Generic;

[System.Serializable]
public class SharedListEntity : SharedVariable<List<Entity>>
{
	public override string ToString() { return mValue == null ? "null" : mValue.ToString(); }
	public static implicit operator SharedListEntity(List<Entity> value) { return new SharedListEntity { mValue = value }; }
}