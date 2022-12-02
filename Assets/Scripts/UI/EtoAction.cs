using System;
using UnityEngine;

public class EtoAction : MonoBehaviour
{
    private enum PopUpAction { Build, SpellUpgrade, TrapUpgrade };

    [SerializeField] private PopUpAction action;
    [SerializeField] private UpgradeSpellPanel upgradeSpellPanel;
    
    private Transform building;

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            switch (action)
            {
                case PopUpAction.SpellUpgrade:
                    upgradeSpellPanel.SetBuilding(building);
                    upgradeSpellPanel.ShowPanel();
                    break;
            }
        }
    }

    public void SetBuilding(Transform trans)
    {
        building = trans;
    }
}