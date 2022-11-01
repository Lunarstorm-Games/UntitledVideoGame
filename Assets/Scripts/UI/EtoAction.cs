using UnityEngine;

public class EtoAction : MonoBehaviour
{
    private enum PopUpAction { Build, Upgrade };

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
                    areYouSureBuildPrompt.SetBuildSpot(buildSpot);
                    areYouSureBuildPrompt.ShowPrompt();
                    break;
                
                case PopUpAction.Upgrade:
                    upgradeSpellPanel.SetBuilding(building);
                    upgradeSpellPanel.ShowPanel();
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