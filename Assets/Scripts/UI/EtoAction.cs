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
    private enum PopUpAction { Build, SpellUpgrade, TrapUpgrade };

    [SerializeField] private PopUpAction action;
    [SerializeField] private AreYouSureBuildPrompt areYouSureBuildPrompt;
    [SerializeField] private UpgradeSpellPanel upgradeSpellPanel;
    [SerializeField] private UpgradeTrapPanel upgradeTrapPanel;

    private Transform buildSpot;
    private Transform building;

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            switch (action)
            {
                case PopUpAction.Build:
                    areYouSureBuildPrompt.SetBuildSpot(buildSpot);
                    areYouSureBuildPrompt.ShowPrompt();
                    break;
                
                case PopUpAction.SpellUpgrade:
                    upgradeSpellPanel.SetBuilding(building);
                    upgradeSpellPanel.ShowPanel();
                    break;
                
                case PopUpAction.TrapUpgrade:
                    upgradeTrapPanel.SetBuilding(building);
                    upgradeTrapPanel.ShowPanel();
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