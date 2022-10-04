using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Assets.scripts.Monobehaviour.Essence;
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
    [SerializeField] private LightSpell lightSpell;
    [SerializeField] private Button[] buttons;
    private EssenceBank essenceBank;
    private ThirdPersonShooterController thirdPersonShooter;
    private BuildingRaycast buildingRaycast;
    private Transform building;
    private Transform rightPanel = new RectTransform();
    private TextMeshProUGUI essenceAmount;
    private Transform warning;
    
    // SPELL PANEL DATA
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
        essenceAmount = transform.Find("TopPanel").transform.Find("EssenceAmount").GetComponent<TextMeshProUGUI>();
        SetLeftPaneData();
        essenceBank = GameObject.Find("WaveController").GetComponent<EssenceBank>();
        warning = transform.Find("Warning");

        // SPELL PANEL OBJECTS
        rightPanel = gameObject.transform.Find("RightPanel").transform;
        HideSpellPanel();
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

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            HidePanel();
        }
    }

    private void SetLeftPaneData()
    {
        buttons[0].transform.Find("Icon").GetComponent<Image>().sprite = lightSpell.Icon;
        buttons[0].transform.Find("LevelBg").GetComponentInChildren<TextMeshProUGUI>().text = lightSpell.DamageLevel.ToString();
    }
    
    private void SetSpellPanelData(Sprite _icon, string _upgradeName, string _description, int _currentLevel, float _currentStat, float _futureStat, int _upgradeCost)
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

    private void SpellSelect(string spell)
    {
        switch (spell)
        {
            case "light dmg":
                SetSpellPanelData(lightSpell.Icon, lightSpell.Name + " Damage", lightSpell.Description, lightSpell.DamageLevel, 
                    lightSpell.CurrentDamage, (lightSpell.CurrentDamage + lightSpell.DamageGrowthAmount), lightSpell.UpgradeCost);
                break;
            
            default:
                Debug.LogWarning("No Such Spell Found");
                break;
        }
    }

    private void ShowWarning(string warningText)
    {
        warning.gameObject.SetActive(true);
        warning.Find("Text").GetComponent<TextMeshProUGUI>().text = warningText;
        StartCoroutine(HideWarning(2f));
    }

    private IEnumerator HideWarning(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        warning.gameObject.SetActive(false);
    }

    private void HideWarningQuick()
    {
        warning.gameObject.SetActive(false);
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
        HideSpellPanel();
        HideWarningQuick();
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

    public void ShowSpellPanel(string spell)
    {
        if (rightPanel.gameObject.activeSelf)
        {
            SpellSelect(spell);
        }
        else
        {
            rightPanel.gameObject.SetActive(true);
            SpellSelect(spell);
        }
    }

    public void HideSpellPanel()
    {
        rightPanel.gameObject.SetActive(false);
    }

    public void SetEssence(float essence)
    {
        essenceAmount.text = essence.ToString();
    }

    public void UpgradeSpell()
    {
        switch (upgradeName.text)
        {
          case "Light Spell Damage":
              if (essenceBank.SpendEssence(lightSpell.UpgradeCost))
              {
                  lightSpell.UpgradeDamage();
                  SetEssence(essenceBank.EssenceAmount);
                  SetLeftPaneData();
                  HideSpellPanel();
                  ShowSpellPanel("light dmg");
              }
              else
              {
                  ShowWarning("You do not have enough essence to increase the damage of your Light Spell");
              }
              break;
          
          default:
              Debug.LogWarning("No such spell");
              break;
        }
    }
}
