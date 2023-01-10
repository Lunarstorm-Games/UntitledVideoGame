using System;
using System.Collections.Generic;
using System.Threading;
using UniStorm;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public DropEvent[] DropEvents;

    private Dictionary<string, DropEvent> eventsAndTimes = new Dictionary<string, DropEvent>();

    private void Start()
    {
        if (DropEvents.Length > 0)
        {
            foreach (var de in DropEvents)
            {
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