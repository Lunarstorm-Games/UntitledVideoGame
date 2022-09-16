using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.scripts.Models.WaveModels;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> SpawnPoints=new List<Transform>();
    
    [SerializeField]
    public List<EnemySpawnSetting> SpawnAblePrefabs = new List<EnemySpawnSetting>();

    [SerializeField] public float MaxEnemyStrengh = 1f;
    private string SpawnPointName = "SpawnPoint";
    // Start is called before the first frame update
    void Start()
    {
     AddSpawnPoints();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SpawnWave(float strength)
    {
     var enemies= DetermineEnemiesToSpawn(strength);
     var random = new System.Random();
     var spawnPoint = SpawnPoints[random.Next(SpawnPoints.Count)];
     SpawnWaveAtSpawnPoint(enemies,spawnPoint);
    }

    private List<EnemySpawnSetting> DetermineEnemiesToSpawn(float targetWaveStrength)
    {
        var spawnableEnemies = SpawnAblePrefabs.Where(x => x.StrengthRating <= MaxEnemyStrengh).ToList();
        
        var currentWaveStrength = 0f;
        var random = new System.Random();
        var enemiesToSpawn = new List<EnemySpawnSetting>();
        while (currentWaveStrength < targetWaveStrength)
        {
            var enemy = spawnableEnemies[random.Next(spawnableEnemies.Count)];
            enemiesToSpawn.Add(enemy);
            currentWaveStrength += enemy.StrengthRating;
        }

        return enemiesToSpawn;
    }

    private void SpawnWaveAtSpawnPoint(List<EnemySpawnSetting> enemies, Transform spawnPoint)
    {
        foreach (var enemySpawnSetting in enemies)
        {
            Instantiate(enemySpawnSetting.Prefab, transform.position, new Quaternion());
        }
    }
    /// <summary>
    /// editor button
    /// </summary>
    public void AddSpawnPoints()
    {
        SpawnPoints = new List<Transform>();
        foreach(Transform child in transform) 
        {
            if (child.name.Contains(SpawnPointName))
            {
                SpawnPoints.Add(child);
            }
        }
    }
}
