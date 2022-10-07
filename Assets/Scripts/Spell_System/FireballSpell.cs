using UnityEngine;

namespace Spell_System
{
    public class FireballSpell : SpellBase
    {
        public float StartAOERadius;
        public float CurrentDamage;
        public float CurrentSpeed;
        public float CurrentAOE;
        public float DamageGrowthAmount;
        public float SpeedGrowthAmount;
        public float AOEGrowthAmount;
        
        [Header("Info Only Attributes")]
        public int DamageLevel = 1;
        public int SpeedLevel = 1;
        public int AOELevel = 1;
        
        void Awake()
        {
            CurrentDamage = StartDamage;
            CurrentSpeed = SpellSpeed;
            CurrentAOE = StartAOERadius;
        }

        public void UpgradeDamage()
        {
            DamageLevel += 1;
            CurrentDamage += DamageGrowthAmount;
        }

        public void UpgradeProjectileSpeed()
        {
            SpeedLevel += 1;
            CurrentSpeed += SpeedGrowthAmount;
        }
        
        public void UpgradeAreaOfEffect()
        {
            AOELevel += 1;
            CurrentAOE += AOEGrowthAmount;
        }
    }
}