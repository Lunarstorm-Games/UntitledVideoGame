using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropEvent : MonoBehaviour
{
    [Header("Time of Event Happening")]
    public int Hour;
    public int Minutes;

    [Header("Drop Prefab & Data")] 
    public DropMeteor Meteor;
    public Vector3 StartingPosition;
    public Vector3 TargetPosition;

    [Header("Drop Item")] public GameObject Item;

    public void StartEvent()
    {
        Meteor.TargetPos = TargetPosition;
        Meteor.Item = Item;
        Instantiate(Meteor, StartingPosition, Quaternion.identity);
    }
}