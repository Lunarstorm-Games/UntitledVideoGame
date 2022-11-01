using System;
using Assets.scripts.Monobehaviour.Essence;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UpgradeTrapPanel : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonShooterController thirdPersonShooter;
    private BuildingRaycast buildingRaycast;
    private EssenceBank essenceBank;
    private Transform building;
    private Transform rightPanel = new RectTransform();
    private TextMeshProUGUI essenceAmount;
    private Transform warning;
    
    // Trap PANEL DATA
    private Image icon;
    private TextMeshProUGUI upgradeName;
    private TextMeshProUGUI description;
    private TextMeshProUGUI currentLevel;
    private TextMeshProUGUI nextLevel;
    private TextMeshProUGUI currentStat;
    private TextMeshProUGUI nextStat;
    private TextMeshProUGUI upgradeCost;

    private void Awake()
    {
        buildingRaycast = starterAssetsInputs.GetComponent<BuildingRaycast>();
        thirdPersonShooter = starterAssetsInputs.GetComponent<ThirdPersonShooterController>();
        essenceAmount = transform.Find("TopPanel").transform.Find("EssenceAmount").GetComponent<TextMeshProUGUI>();
        essenceBank = EssenceBank.Instance;
        warning = transform.Find("Warning");

        // TRAP PANEL OBJECTS
        rightPanel = gameObject.transform.Find("RightPanel").transform;
        HideTrapPanel();
        icon = rightPanel.transform.Find("UpgradeIcon").transform.GetComponent<Image>();
        upgradeName = rightPanel.transform.Find("Name").transform.GetComponent<TextMeshProUGUI>();
        description = rightPanel.transform.Find("Description").transform.GetComponent<TextMeshProUGUI>();
        currentLevel = rightPanel.transform.Find("CurrentLevelImage").transform.Find("CurrentLevel")
            .GetComponent<TextMeshProUGUI>();
        nextLevel = rightPanel.transform.Find("NextLevelImage").transform.Find("NextLevel")
            .GetComponent<TextMeshProUGUI>();
        currentStat = rightPanel.transform.Find("CurrentLevelImage").transform.Find("CurrentValue")
            .GetComponent<TextMeshProUGUI>();
        nextStat = rightPanel.transform.Find("NextLevelImage").transform.Find("NextValue")
            .GetComponent<TextMeshProUGUI>();
        upgradeCost = rightPanel.transform.Find("UpgradeButton").transform.Find("Cost").GetComponent<TextMeshProUGUI>();
    }

    private void TrapSelect(string trap)
    {
        switch (trap)
        {
            case "light dmg":
                Debug.Log("trap upgrade - light dmg");
                break;
            
            case "light duration":
                Debug.Log("trap upgrade - light duration");
                break;
            
            case "slow dmg":
                Debug.Log("trap upgrade - slow dmg");
                break;
            
            case "slow duration":
                Debug.Log("trap upgrade - slow duration");
                break;

            default:
                Debug.LogWarning("No Such Trap Found");
                break;
        }
    }
    
    public void ShowTrapPanel(string spell)
    {
        if (rightPanel.gameObject.activeSelf)
        {
            TrapSelect(spell);
        }
        else
        {
            rightPanel.gameObject.SetActive(true);
            TrapSelect(spell);
        }
    }

    public void HideTrapPanel()
    {
        rightPanel.gameObject.SetActive(false);
    }
    
    public void SetEssence(float essence)
    {
        essenceAmount.text = essence.ToString();
    }
    
    public void ShowPanel()
    {
        starterAssetsInputs.SetCursorState(false);
        starterAssetsInputs.cursorInputForLook = false;
        playerInput.actions.Disable();
        gameObject.SetActive(true);
        SetEssence(essenceBank.EssenceAmount);
        buildingRaycast.SetIsPormptOpen(true);
        thirdPersonShooter.SetIsPormptOpen(true);
    }
    
    public void HidePanel()
    {
        gameObject.SetActive(false);
        starterAssetsInputs.SetCursorState(true);
        starterAssetsInputs.cursorInputForLook = true;
        playerInput.actions.Enable();
        buildingRaycast.SetIsPormptOpen(false);
        thirdPersonShooter.SetIsPormptOpen(false);
    }
    
    public void SetBuilding(Transform b)
    {
        building = b;
    }
}