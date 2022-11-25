using System;
using Assets.Code.Scripts.Models.Essence;
using UnityEngine;
using Assets.Scripts.SaveSystem;

namespace Assets.scripts.Monobehaviour.Essence
{
    /// <summary>
    /// Singleton Instance of the essence bank.
    /// </summary>
    public class EssenceBank : PersistableMonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeReference] [SaveField]
        private EssenceBankModel Bank = new();

        public float EssenceAmount => Bank.EssenceAmount;
        public static EssenceBank Instance { get; private set; }

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
        
        public void AddEssence(int amount)
        {
            Bank.EssenceAmount += amount;
        }

        void OnDestroy()
        {
           base.OnDestroy();
        }
        
        public bool SpendEssence(int amount)
        {
            if (Bank.EssenceAmount >= amount)
            {
                Bank.EssenceAmount-=amount;
                return true;
            }

            return false;
        }

        public override void OnLoad()
        {
        }
    }
}
