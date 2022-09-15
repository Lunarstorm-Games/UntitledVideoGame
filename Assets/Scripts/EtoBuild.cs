using System;
using System.Collections;
using System.Collections.Generic;
using FullscreenEditor;
using StarterAssets;
using UnityEngine;

public class EtoBuild : MonoBehaviour
{
    [SerializeField] private AreYouSurePrompt areYouSurePrompt;

    private Transform buildSpot;

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            areYouSurePrompt.ShowPrompt();
            areYouSurePrompt.SetBuildSpot(buildSpot);
        }
    }

    public Transform GetBuildSpot()
    {
        return buildSpot;
    }

    public void SetBuildSpot(Transform trans)
    {
        buildSpot = trans;
    }
    
}
