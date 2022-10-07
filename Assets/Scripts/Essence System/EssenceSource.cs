using Assets.scripts.Monobehaviour;
using Assets.scripts.Monobehaviour.Essence;
using UnityEngine;

namespace Assets.scripts.Logic
{
    public class EssenceSourceLogic
    {
        [SerializeField, Min(0)]
        public int EssenceValue;

        private bool HasDroppedEssence = false;
        /// <summary>
        /// Adds the entities essence to the bank. Can only be run once;
        /// </summary>
        public void DropEssence()
        {
            if (HasDroppedEssence) return;
            if (EssenceBank.Instance is not null)
            {
                EssenceBank.Instance.AddEssence(EssenceValue);
            }
        }
    }
}
