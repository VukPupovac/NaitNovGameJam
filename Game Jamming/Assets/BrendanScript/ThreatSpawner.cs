using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatSpawner : MonoBehaviour
{
    [Header("Threats to Spawn")]
    [SerializeField] private ThreatSO[] threats;

    [Header("Spawn Settings")]
    [SerializeField] private float minSpawnInterval = 1f;
    [SerializeField] private float maxSpawnInterval = 5f;
    [SerializeField] private PolygonCollider2D spawnArea;

    private float spawnInterval;

    private Dictionary<ThreatType, float> nextSpawnTimes = new();

    private Coroutine spawnRoutine;

    private void Start() // Replace with Game manager
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        if (spawnRoutine == null)
        {
            spawnRoutine = StartCoroutine(SpawnRoutine());
        }
    }

    public void StopSpawning()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnRandomAvailableThreat();
            spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnRandomAvailableThreat()
    {
        List<ThreatSO> availableThreats = new();

        foreach (ThreatSO threat in threats)
        {
            if (CanSpawn(threat.threatType))
            {
                availableThreats.Add(threat);
            }
        }

        if (availableThreats.Count == 0)
        {
            return;
        }

        ThreatSO selectedThreat = availableThreats[Random.Range(0, availableThreats.Count)];

        Vector2 spawnPosition = GetRandomPointInSpawnArea();

        Instantiate(
            selectedThreat.prefab,
            spawnPosition,
            Quaternion.identity);

        nextSpawnTimes[selectedThreat.threatType] = Time.time + selectedThreat.cooldown;
    }

    private bool CanSpawn(ThreatType threatType)
    {
        if (!nextSpawnTimes.TryGetValue(threatType, out float nextSpawnTime))
        {
            return true;
        }

        return Time.time >= nextSpawnTime;
    }

    private Vector2 GetRandomPointInSpawnArea()
    {
        if (spawnArea == null)
        {
            return transform.position;
        }

        Bounds bounds = spawnArea.bounds;

        for (int i = 0; i < 100; i++)
        {
            Vector2 point = new(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y));

            if (spawnArea.OverlapPoint(point))
            {
                return point;
            }
        }

        return bounds.center;
    }
}
