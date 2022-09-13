using Assets.scripts.Models;
using UnityEngine;

namespace Assets.scripts.Monobehaviour.Essence
{
    /// <summary>
    /// Singleton Instance of the essence bank.
    /// </summary>
    public class EssenceBank : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeReference]
        private EssenceBankModel Bank = new EssenceBankModel();

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
