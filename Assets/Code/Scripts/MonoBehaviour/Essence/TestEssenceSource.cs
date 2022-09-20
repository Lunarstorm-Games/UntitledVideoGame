using Assets.scripts.Logic;
using UnityEngine;

namespace Assets.scripts.Monobehaviour.Essence
{
    public class TestEssenceSource : MonoBehaviour
    {
        [SerializeReference]
        public EssenceSourceLogic EssenceSource = new EssenceSourceLogic();
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void OnDestroy()
        {
            EssenceSource.DropEssence();
        }
    }
}
