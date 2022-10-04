using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using StarterAssets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AreYouSureBuildPrompt : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;
    [SerializeField] private PlayerInput playerInput;
    private ThirdPersonShooterController thirdPersonShooter;
    private BuildingRaycast buildingRaycast;
    private Transform buildSpot;
    private TextMeshProUGUI text;

    void Awake()
    {
        buildingRaycast = starterAssetsInputs.GetComponent<BuildingRaycast>();
        thirdPersonShooter = starterAssetsInputs.GetComponent<ThirdPersonShooterController>();
        text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }
    
    public void ShowPrompt()
    {
        starterAssetsInputs.SetCursorState(false);
        starterAssetsInputs.cursorInputForLook = false;
        playerInput.actions.Disable();
        gameObject.SetActive(true);
        var buildingName = Regex.Replace(buildSpot.Find("BuildingModel").tag,
            "([A-Z])", " $1", RegexOptions.Compiled).Trim();
        text.text = $"Are you sure you want to build a {buildingName}?";
        buildingRaycast.SetIsPormptOpen(true);
        thirdPersonShooter.SetIsPormptOpen(true);
    }

    private void HidePrompt()
    {
        starterAssetsInputs.SetCursorState(true);
        starterAssetsInputs.cursorInputForLook = true;
        playerInput.actions.Enable();
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
            buildSpot.Find("BuildingModel").gameObject.SetActive(true);
            buildSpot.Find("BuildingSpotModel").gameObject.SetActive(false);
            HidePrompt();
        }
    }
}