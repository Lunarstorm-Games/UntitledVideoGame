using System;
using System.Collections;
using System.Collections.Generic;
using FullscreenEditor;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EtoAction : MonoBehaviour
{
    public enum PopUpAction { Build, Upgrade };

    [SerializeField] private PopUpAction action;
    [SerializeField] private AreYouSureBuildPrompt areYouSureBuildPrompt;
    [SerializeField] private UpgradeSpellPanel upgradeSpellPanel;

    private Transform buildSpot;
    private Transform building;

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            switch (action)
            {
                case PopUpAction.Build:
                    areYouSureBuildPrompt.ShowPrompt();
                    areYouSureBuildPrompt.SetBuildSpot(buildSpot);
                    break;
                
                case PopUpAction.Upgrade:
                    upgradeSpellPanel.ShowPanel();
                    upgradeSpellPanel.SetBuilding(building);
                    break;
            }
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

    public void SetBuilding(Transform trans)
    {
        building = trans;
    }
}