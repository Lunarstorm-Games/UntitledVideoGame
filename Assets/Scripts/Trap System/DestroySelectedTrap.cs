using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Assets.scripts.Monobehaviour.Essence;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DestroySelectedTrap : MonoBehaviour, IPointerClickHandler
{
    public GameEvent trapUIClicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        trapUIClicked.Raise();
    }
}
