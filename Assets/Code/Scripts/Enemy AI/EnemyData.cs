using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New enemy data", menuName = "Entity/Enemy/New Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField] public float health = 30f;
    [SerializeField] public float damage = 10f;
    [SerializeField] public float speed = 3f;
    [SerializeField] public float aggroRange = 15f;
    [SerializeField] public float attackRange = 4f;
    [SerializeField] public float attackDelay = 3f;
    [SerializeField] public float preAttackDelay = 0.6f;
    [SerializeField] public int essenceDropAmount = 10;

    [HideInInspector] public float currentAttackDelay = 0f;
    [HideInInspector] public float currentHealth = 0f;
}