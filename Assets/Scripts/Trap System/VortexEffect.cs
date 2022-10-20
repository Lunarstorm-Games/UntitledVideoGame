using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexEffect : MonoBehaviour
{
    public float VortexStrength = 1000f;

    public float SwirlStrength = 5f;

    [SerializeField] private GameObject _gameObject;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
       
    }

    private void OnTriggerExit(Collider other)
    {
        throw new NotImplementedException();
    }
}
