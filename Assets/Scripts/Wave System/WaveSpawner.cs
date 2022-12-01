using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.scripts.Models.WaveModels;
using Assets.Scripts.Interfaces;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class WaveSpawner : MonoBehaviour
{
    [Header("General Settings")]
    [Space]
    [SerializeField]
    private List<Transform> SpawnPoints;
    [SerializeField]
    public List<EnemySpawnSetting> SpawnAblePrefabs = new List<EnemySpawnSetting>();
    public bool DebugInfo = false;
    [Space]
    
    
    public float MaxEnemyStrengh = 1f;
    public AnimationCurve MaxEnemyStrengthCurve = new AnimationCurve();
    [Space]
    [Header("Large Wave Settings")]
    [Space]
    public float SecondsPerLargeWave = 10;
    public float WaveStrengthMultiplier = 10;
    public AnimationCurve LargeWaveAnimationCurve = new AnimationCurve();
    [SerializeField] private int XStep;

    [Space]
    [Header("Trickle spawn settings")]
    [Space]
    public float SecondsBetweenTrickleSpawns = 2;
    public float TrickleSpawnStrengthMultiplier = 1f;
    public AnimationCurve TrickleWaveCurve = new AnimationCurve();
    [Space]
    public int SpawnPerFixedUpdate = 3;

    [ReadOnly] public bool IsActivated;



    [SerializeField] private float roundTime = 0;
    [Space]
    [Header("Object pooling")]

    private Dictionary<string, List<GameObject>> inactiveObjectPool = new();
    //private Dictionary<string, List<GameObject>> activeObjectPool = new();
    public int initialPoolSize = 30;

    private string SpawnPointName = "SpawnPoint";
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        AddSpawnPoints();
        InitializeObjectPool();
        //MoveSpawnPointsToNavmesh();
       
    }

    // Update is called once per frame
    void Update()
    {
        roundTime += Time.deltaTime;
        MaxEnemyStrengh = MaxEnemyStrengthCurve.Evaluate(XStep);

        
    }
    void InitializeObjectPool()
    {
        var position = SpawnPoints.First().position;
        foreach (var prefab in SpawnAblePrefabs)
        {
            var newList = new List<GameObject>();
            
            inactiveObjectPool.Add(prefab.Prefab.name, newList);

            for (int i = 0; i < initialPoolSize; i++)
            {
                var newObject = Instantiate(prefab.Prefab,position,Quaternion.identity);
                var script= newObject.GetComponent(typeof(IPoolableObject)) as IPoolableObject;
                script.PrefabName = prefab.Prefab.name;
                script.OnSetInactive += OnSetEnemyInactive;
                newObject.SetActive(false);
                newList.Add(newObject);
            }
        }
    }
    /// <summary>
    /// starts waves
    /// </summary>
    public void StartWaves()
    {
        if (IsActivated) return;
        IsActivated = true;
        StartCoroutine(TrickleCoroutine());
        StartCoroutine(LargeWaveCoroutine());
    }

    private void MoveSpawnPointsToNavmesh()
    {
        foreach (var spawnPoint in SpawnPoints)
        {
            if (Physics.Raycast(new Ray(transform.position, Vector3.down), out RaycastHit hit))
            {
                spawnPoint.position = hit.point;
            }
        }
    }

    public IEnumerator LargeWaveCoroutine()
    {

        while (true)
        {

            var curveX = XStep;
            var largeWaveStrength = LargeWaveAnimationCurve.Evaluate(curveX) * WaveStrengthMultiplier;
            Debug.Log($"spawning large wave of strength: {largeWaveStrength}");
            SpawnWave(largeWaveStrength);
            XStep++;
            yield return new WaitForSeconds(SecondsPerLargeWave);
        }
    }
    public IEnumerator TrickleCoroutine()
    {
        while (true)
        {

            var curveX = XStep;
            var trickle = TrickleWaveCurve.Evaluate(curveX) * TrickleSpawnStrengthMultiplier;
            SpawnWave(trickle);
            yield return new WaitForSeconds(SecondsBetweenTrickleSpawns);
        }
    }
    public void SpawnWave(float strength)
    {
        var enemies = DetermineEnemiesToSpawn(strength);
        var random = new System.Random();
        var spawnPoint = SpawnPoints[random.Next(SpawnPoints.Count)];
        if (DebugInfo)
        {
            Debug.Log($"Spawning wave of {enemies.Count} (strenght {strength}) at {spawnPoint.name}");
        }
        StartCoroutine(SpawnWaveAtSpawnPoint(enemies, spawnPoint));
    }

    public void SpawnAtAllSpawnPoints(float strength)
    {
        var enemies = DetermineEnemiesToSpawn(strength);
        var random = new System.Random();
        foreach (var spawnPoint in SpawnPoints)
        {
            StartCoroutine(SpawnWaveAtSpawnPoint(enemies, spawnPoint));
        }
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

    private IEnumerator SpawnWaveAtSpawnPoint(List<EnemySpawnSetting> enemies, Transform spawnPoint)
    {
        foreach (var enemySpawnSetting in enemies)
        {
            var i = 0;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPoint.position, out hit, 100f, NavMesh.AllAreas))
            {
                var newEnemy = GetObjectFromPool(enemySpawnSetting);
                newEnemy.Initialize(hit.position, Quaternion.identity);
                
                i++;
            }

            if (i >= SpawnPerFixedUpdate)
            {
                i = 0;
                yield return new WaitForFixedUpdate();
            }
        }

    }
    IPoolableObject GetObjectFromPool(EnemySpawnSetting spawnSetting)
    {
        var pooledGameObject=   inactiveObjectPool[spawnSetting.Prefab.name].FirstOrDefault();
        if (pooledGameObject == null)
        {
            pooledGameObject= Instantiate(spawnSetting.Prefab);
        }
        inactiveObjectPool[spawnSetting.Prefab.name].Remove(pooledGameObject);
       var result =  pooledGameObject.GetComponent(typeof(IPoolableObject)) as IPoolableObject;
        return result;

    }
    /// <summary>
    /// editor button
    /// </summary>
    public void AddSpawnPoints()
    {
        SpawnPoints = new List<Transform>();
        foreach (Transform child in transform)
        {
            if (child.name.Contains(SpawnPointName))
            {
                SpawnPoints.Add(child);
            }
        }
    }
    void OnSetEnemyInactive(GameObject source)
    {
        var script = source.GetComponent(typeof(IPoolableObject)) as IPoolableObject;
        inactiveObjectPool[script.PrefabName].Add(source);

    }
}
