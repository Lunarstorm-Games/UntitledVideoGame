using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [HideInInspector] public Vector3 targetOffset;
    [SerializeField] private Vector3 Offset = Vector3.zero;

    private void Update()
    {
        targetOffset = transform.position;
        targetOffset += Offset;
    }
}
