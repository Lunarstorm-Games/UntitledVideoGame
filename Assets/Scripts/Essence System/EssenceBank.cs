using Assets.Code.Scripts.Models.Essence;
using Assets.scripts.Models;
using Assets.Scripts.SaveSystem;
using UnityEngine;

namespace Assets.scripts.Monobehaviour.Essence
{
    /// <summary>
    /// Singleton Instance of the essence bank.
    /// </summary>
    public class EssenceBank : PersistableMonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeReference] [Persistance]
        private EssenceBankModel Bank = new();

        public float EssenceAmount => Bank.EssenceAmount;
        public static EssenceBank Instance { get; private set; }
        void Start()
        {

            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void AddEssence(int amount)
        {
            Bank.EssenceAmount += amount;
        }

        public void SpendEssence(int amount)
        {
            if (Bank.EssenceAmount > amount)
            {
                Bank.EssenceAmount-=amount;
            }
        }
    }
}
