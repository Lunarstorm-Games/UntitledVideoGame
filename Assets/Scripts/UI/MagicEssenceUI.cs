using Assets.scripts.Monobehaviour.Essence;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MagicEssenceUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI magicEssence;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        magicEssence.text = EssenceBank.Instance?.EssenceAmount.ToString();
    }
}
