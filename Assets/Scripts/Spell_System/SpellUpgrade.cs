using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "Spell Upgrade", menuName = "Spell Upgrade")]
public class SpellUpgrade : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Icon;
    [Tooltip("Upgrade cost per level")]
    public int UpgradeCost;
    public enum UpgradeType { Damage, Speed, AOE}

    [Header("Upgrade Type and Amount per Level")]
    public UpgradeType type;
    [Tooltip("Amount to increase stat per level")]
    public float Value;
}