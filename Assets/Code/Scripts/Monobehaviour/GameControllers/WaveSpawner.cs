using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.scripts.Models.WaveModels;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> SpawnPoints;

    [SerializeField]
    public List<EnemySpawnSetting> SpawnAblePrefabs = new List<EnemySpawnSetting>();

    public float MaxEnemyStrengh = 1f;
    public float SecondsPerLargeWave = 10;
    public bool DebugInfo = false;
    [SerializeField] private int XStep;
    public float SecondsBetweenTrickleSpawns = 2;
    public float TrickleSpawnStrengthMultiplier = 1f;
    public float WaveStrengthMultiplier = 10;
    public int SpawnPerFixedUpdate = 3;
    public AnimationCurve LargeWaveAnimationCurve = new AnimationCurve();

    public AnimationCurve MaxEnemyStrengthCurve = new AnimationCurve();

    public AnimationCurve TrickleWaveCurve = new AnimationCurve();
    [SerializeField] private float roundTime = 0;
    private string SpawnPointName = "SpawnPoint";
    // Start is called before the first frame update
    void Start()
    {
        AddSpawnPoints();
        //MoveSpawnPointsToNavmesh();
        StartCoroutine(TrickleCoroutine());
        StartCoroutine(LargeWaveCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        roundTime += Time.deltaTime;
        MaxEnemyStrengh = MaxEnemyStrengthCurve.Evaluate(XStep);
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
                Instantiate(enemySpawnSetting.Prefab, hit.position, Quaternion.identity);
                i++;
            }

            if (i >= SpawnPerFixedUpdate)
            {
                i = 0;
                yield return new WaitForFixedUpdate();
            }
        }

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
}