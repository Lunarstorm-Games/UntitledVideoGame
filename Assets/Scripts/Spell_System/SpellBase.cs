using UnityEngine;
using UnityEngine.VFX;

public abstract class SpellBase : MonoBehaviour
{
    public string Name;
    public string Description;
    public Sprite Icon;

    public float StartDamage;
    public float SpellSpeed;
    public int UpgradeCost;

    public VisualEffect ImpactEffect;
}