using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class AreYouSurePrompt : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonShooterController thirdPersonShooter;
    private BuildingRaycast buildingRaycast;
    private Transform buildSpot = new RectTransform();

    void Awake()
    {
        buildingRaycast = starterAssetsInputs.GetComponent<BuildingRaycast>();
        thirdPersonShooter = starterAssetsInputs.GetComponent<ThirdPersonShooterController>();
    }
    
    public void ShowPrompt()
    {
        starterAssetsInputs.SetCursorState(false);
        starterAssetsInputs.cursorInputForLook = false;
        gameObject.SetActive(true);
        buildingRaycast.SetIsPormptOpen(true);
        thirdPersonShooter.SetIsPormptOpen(true);
    }

    private void HidePrompt()
    {
        starterAssetsInputs.SetCursorState(true);
        starterAssetsInputs.cursorInputForLook = true;
        gameObject.SetActive(false);
        buildingRaycast.SetIsPormptOpen(false);
        thirdPersonShooter.SetIsPormptOpen(false);
    }
    
    public Transform GetBuildSpot()
    {
        return buildSpot;
    }
    
    public void SetBuildSpot(Transform trans)
    {
        buildSpot = trans;
    }

    public void BuildBuilding()
    {
        if (buildSpot)
        {
            buildSpot.transform.Find("Building").gameObject.SetActive(true);
            buildSpot.transform.Find("BuildingSpotModel").gameObject.SetActive(false);
            HidePrompt();
        }
    }
}
