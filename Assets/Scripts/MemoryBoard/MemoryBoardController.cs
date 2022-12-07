using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniStorm;
using UnityEngine;

public class MemoryBoardController : MonoBehaviour
{
    public GameObject TimerUI;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void TrackEnemyApperance()
    {
        int hour = UniStormSystem.Instance?.Hour??0;
        int minutes = UniStormSystem.Instance?.Minute??0;

        TimerUI.GetComponent<TextMeshProUGUI>().text = $"{hour}:{minutes.ToString().PadLeft(2, '0')}";
    }
}
