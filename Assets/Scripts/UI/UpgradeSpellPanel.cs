using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UpgradeSpellPanel : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs starterAssetsInputs;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Sprite spellIcon;
    private ThirdPersonShooterController thirdPersonShooter;
    private BuildingRaycast buildingRaycast;
    private Transform building;
    private Transform rightPanel = new RectTransform();
    private Text essenceAmount;
    
    // RIGHT PANEL DATA
    private Image icon;
    private TextMeshProUGUI upgradeName;
    private TextMeshProUGUI description;
    private TextMeshProUGUI currentLevel;
    private TextMeshProUGUI nextLevel;
    private TextMeshProUGUI currentStat;
    private TextMeshProUGUI nextStat;
    private TextMeshProUGUI upgradeCost;
    
    void Awake()
    {
        buildingRaycast = starterAssetsInputs.GetComponent<BuildingRaycast>();
        thirdPersonShooter = starterAssetsInputs.GetComponent<ThirdPersonShooterController>();
        essenceAmount = transform.Find("TopPanel").transform.Find("EssenceAmount").GetComponent<Text>();
        
        // RIGHT PANEL OBJECTS
        rightPanel = gameObject.transform.Find("RightPanel").transform;
        ShowRightPanel(false);
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

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            HidePanel();
        }
    }

    public void ShowPanel()
    {
        starterAssetsInputs.SetCursorState(false);
        starterAssetsInputs.cursorInputForLook = false;
        playerInput.actions.Disable();
        gameObject.SetActive(true);
        buildingRaycast.SetIsPormptOpen(true);
        thirdPersonShooter.SetIsPormptOpen(true);
    }

    public void HidePanel()
    {
        ShowRightPanel(false);
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

    public void SetRightPanelData(Sprite _icon, string _upgradeName, string _description, int _currentLevel, int _currentStat, int _futureStat, int _upgradeCost)
    {
        icon.sprite = _icon;
        upgradeName.text = _upgradeName;
        description.text = _description;
        currentLevel.text = _currentLevel.ToString();
        nextLevel.text = (_currentLevel + 1).ToString();
        currentStat.text = _currentStat.ToString();
        nextStat.text = _futureStat.ToString();
        upgradeCost.text = _upgradeCost + ")";
    }

    public void ShowRightPanel(bool status)
    {
        rightPanel.gameObject.SetActive(status);
        // Test Data for Right Panel
        if (status == true)
        {
            SetRightPanelData(spellIcon, "Test Upgrade", "Test description. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec faucibus ex tortor, " +
                                                         "eu congue orci.", 0, 100, 160, 500);
        }
    }

    public void SetEssence(int essence)
    {
        if (essence != 0)
        {
            essenceAmount.text = essence.ToString(); 
            return;
        }

        essenceAmount.text = "69420";
    }

    public void UpgradeSpell()
    {
        Debug.Log("Spell " + upgradeName.text + " upgraded, for " + upgradeCost.text + " in building " + building.parent.tag);
        HidePanel();
    }
}
