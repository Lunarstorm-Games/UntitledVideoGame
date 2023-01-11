using System;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UniStorm;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public TextMeshProUGUI PopUp;
    public Image EffectIcon;
    public DropEvent[] DropEvents;

    private Dictionary<string, DropEvent> eventsAndTimes = new Dictionary<string, DropEvent>();

    private void Start()
    {
        if (DropEvents.Length > 0)
        {
            foreach (var de in DropEvents)
            {
                de.Item.GetComponentInChildren<DropItem>().EffectIconImage = EffectIcon;
                de.Item.GetComponentInChildren<DropItem>().PopUp = PopUp;
                string tempTime = de.Hour + ":" + de.Minutes;
                eventsAndTimes.Add(tempTime, de);
            }
        }
    }

    private void Update()
    {
        int hour = UniStormSystem.Instance.Hour;
        int minutes = UniStormSystem.Instance.Minute;

        string tempTime = hour + ":" + minutes;

        if (eventsAndTimes.ContainsKey(tempTime))
        {
            eventsAndTimes[tempTime].StartEvent();
            eventsAndTimes.Remove(tempTime);
        }
    }
}