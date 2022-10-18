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
    [SerializeField] private Button[] buttons;
    [SerializeField] private SpellUpgrade[] spellUpgrades;
    private EssenceBank essenceBank;
    private Projectile[] spells;
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
        spells = starterAssetsInputs.GetComponent<SpellInventory>().spells;
        essenceAmount = transform.Find("TopPanel").transform.Find("EssenceAmount").GetComponent<TextMeshProUGUI>();
        essenceBank = EssenceBank.Instance;
        warning = transform.Find("Warning");
        SetLeftPaneData();

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
        buttons[0].transform.Find("Icon").GetComponent<Image>().sprite = spellUpgrades[0].Icon;
        buttons[0].transform.Find("LevelBg").GetComponentInChildren<TextMeshProUGUI>().text = spells[0].DamageLevel.ToString();        
        buttons[1].transform.Find("Icon").GetComponent<Image>().sprite = spellUpgrades[1].Icon;
        buttons[1].transform.Find("LevelBg").GetComponentInChildren<TextMeshProUGUI>().text = spells[0].SpeedLevel.ToString();
        buttons[2].transform.Find("Icon").GetComponent<Image>().sprite = spellUpgrades[2].Icon;
        buttons[2].transform.Find("LevelBg").GetComponentInChildren<TextMeshProUGUI>().text = spells[1].DamageLevel.ToString();
        buttons[3].transform.Find("Icon").GetComponent<Image>().sprite = spellUpgrades[3].Icon;
        buttons[3].transform.Find("LevelBg").GetComponentInChildren<TextMeshProUGUI>().text = spells[1].SpeedLevel.ToString();
        buttons[4].transform.Find("Icon").GetComponent<Image>().sprite = spellUpgrades[4].Icon;
        buttons[4].transform.Find("LevelBg").GetComponentInChildren<TextMeshProUGUI>().text = spells[1].GetComponent<AOEProjectile>().AOELevel.ToString();
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
                SetSpellPanelData(spellUpgrades[0].Icon, spellUpgrades[0].Name, spellUpgrades[0].Description, spells[0].DamageLevel, 
                    spells[0].damage, (spells[0].damage + spellUpgrades[0].Value), spellUpgrades[0].UpgradeCost);
                break;
            
            case "light speed":
                SetSpellPanelData(spellUpgrades[1].Icon, spellUpgrades[1].Name, spellUpgrades[1].Description, spells[0].SpeedLevel, 
                    spells[0].speed, (spells[0].speed + spellUpgrades[1].Value), spellUpgrades[1].UpgradeCost);
                break;
            
            case "fire dmg":
                SetSpellPanelData(spellUpgrades[2].Icon, spellUpgrades[2].Name, spellUpgrades[2].Description, spells[1].DamageLevel, 
                    spells[1].damage, (spells[1].damage + spellUpgrades[2].Value), spellUpgrades[2].UpgradeCost);
                break;
            
            case "fire speed":
                SetSpellPanelData(spellUpgrades[3].Icon, spellUpgrades[3].Name, spellUpgrades[3].Description, spells[1].SpeedLevel, 
                    spells[1].speed, (spells[1].speed + spellUpgrades[3].Value), spellUpgrades[3].UpgradeCost);
                break;
            
            case "fire aoe":
                SetSpellPanelData(spellUpgrades[4].Icon, spellUpgrades[4].Name, spellUpgrades[4].Description, spells[1].GetComponent<AOEProjectile>().AOELevel, 
                    spells[1].GetComponent<AOEProjectile>().AOERadius, (spells[1].GetComponent<AOEProjectile>().AOERadius + spellUpgrades[4].Value), 
                    spellUpgrades[4].UpgradeCost);
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
              if (essenceBank.SpendEssence(spellUpgrades[0].UpgradeCost))
              {
                  spells[0].UpgradeDamage(spellUpgrades[0].Value);
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
          
          case "Light Spell Speed":
              if (essenceBank.SpendEssence(spellUpgrades[1].UpgradeCost))
              {
                  spells[0].UpgradeSpeed(spellUpgrades[1].Value);
                  SetEssence(essenceBank.EssenceAmount);
                  SetLeftPaneData();
                  HideSpellPanel();
                  ShowSpellPanel("light speed");
              }
              else
              {
                  ShowWarning("You do not have enough essence to increase the speed of your Light Spell");
              }
              break;
          
          case "Fireball Damage":
              if (essenceBank.SpendEssence(spellUpgrades[2].UpgradeCost))
              {
                  spells[1].UpgradeDamage(spellUpgrades[2].Value);
                  SetEssence(essenceBank.EssenceAmount);
                  SetLeftPaneData();
                  HideSpellPanel();
                  ShowSpellPanel("fire dmg");
              }
              else
              {
                  ShowWarning("You do not have enough essence to increase the damage of your Fireball Spell");
              }
              break;
          
          case "Fireball Speed":
              if (essenceBank.SpendEssence(spellUpgrades[3].UpgradeCost))
              {
                  spells[1].UpgradeSpeed(spellUpgrades[3].Value);
                  SetEssence(essenceBank.EssenceAmount);
                  SetLeftPaneData();
                  HideSpellPanel();
                  ShowSpellPanel("fire speed");
              }
              else
              {
                  ShowWarning("You do not have enough essence to increase the damage of your Fireball Spell");
              }
              break;
          
          case "Fireball Damage Area":
              if (essenceBank.SpendEssence(spellUpgrades[4].UpgradeCost))
              {
                  spells[1].GetComponent<AOEProjectile>().UpgradeAOE(spellUpgrades[4].Value);
                  SetEssence(essenceBank.EssenceAmount);
                  SetLeftPaneData();
                  HideSpellPanel();
                  ShowSpellPanel("fire aoe");
              }
              else
              {
                  ShowWarning("You do not have enough essence to increase the damage of your Fireball Spell");
              }
              break;
          
          default:
              Debug.LogWarning("No such spell");
              break;
        }
    }
}
