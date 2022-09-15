using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class AreYouSurePrompt : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;
    private PlayerRaycast playerRaycast;
    private Transform buildSpot = new RectTransform();

    void Awake()
    {
        playerRaycast = starterAssetsInputs.GetComponent<PlayerRaycast>();
    }
    
    public void ShowPrompt()
    {
        starterAssetsInputs.SetCursorState(false);
        starterAssetsInputs.cursorInputForLook = false;
        gameObject.SetActive(true);
        playerRaycast.SetIsPormptOpen(true);
    }

    private void HidePrompt()
    {
        starterAssetsInputs.SetCursorState(true);
        starterAssetsInputs.cursorInputForLook = true;
        gameObject.SetActive(false);
        playerRaycast.SetIsPormptOpen(false);
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
