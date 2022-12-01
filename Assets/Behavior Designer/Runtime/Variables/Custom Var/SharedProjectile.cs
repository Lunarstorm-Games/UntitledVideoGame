using UnityEngine;
using BehaviorDesigner.Runtime;

[System.Serializable]
public class SharedProjectile : SharedVariable<Projectile>
{
	public override string ToString() { return mValue == null ? "null" : mValue.ToString(); }
	public static implicit operator SharedProjectile(Projectile value) { return new SharedProjectile { mValue = value }; }
}