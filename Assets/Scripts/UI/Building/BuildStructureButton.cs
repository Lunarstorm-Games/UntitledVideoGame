using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildStructureButton : MonoBehaviour
{
    public TextMeshProUGUI essenceCostLabel;

    public BuildableStructure allowedStructure;
    // Start is called before the first frame update
    void Start()
    {
        essenceCostLabel.text = allowedStructure.EssenceCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
