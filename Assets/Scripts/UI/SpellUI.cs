using System;
using UnityEngine;

public class SpellUI : MonoBehaviour
{
    [SerializeField] private Transform spell1Panel;
    [SerializeField] private Transform spell2Panel;

    private Transform border1;
    private Transform border2;

    private void Awake()
    {
        border1 = spell1Panel.Find("Border1");
        border2 = spell2Panel.Find("Border2");
        
        border1.gameObject.SetActive(true);
        border2.gameObject.SetActive(false);
    }

    public void SetActiveSpell(int number)
    {
        switch (number)
        {
            case 1:
                border1.gameObject.SetActive(true);
                border2.gameObject.SetActive(false);
                break;
            
            case 2:
                border1.gameObject.SetActive(false);
                border2.gameObject.SetActive(true);
                break;
            
            default:
                Debug.LogWarning("No such spell.");
                break;
        }
    }
}