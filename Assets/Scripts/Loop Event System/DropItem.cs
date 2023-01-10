using System;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public float RotationSpeed;
    public enum PickupEffect { InstaKill }

    private GameObject[] enemies;

    [Tooltip("What the pickup does")]
    public PickupEffect Effect;

    private void Update()
    {
        // Rotating Animation
        transform.Rotate(0, RotationSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<Entity>().TakeDamage(enemy.GetComponent<Entity>().MaxHealth, Player.Instance);
            }
        }
    }
}