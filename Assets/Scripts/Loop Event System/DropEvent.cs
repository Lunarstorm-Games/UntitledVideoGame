using System;
using UnityEngine;

public class DropEvent : MonoBehaviour
{
    [Header("Time of Event Happening")]
    public int Hour;
    public int Minutes;

    [Header("Drop Prefab")] public DropMeteor Meteor;

    [Header("Drop Item")] public GameObject Item;

    public void StartEvent()
    {
        Instantiate(Meteor);
        Meteor.Item = Item;
    }
}