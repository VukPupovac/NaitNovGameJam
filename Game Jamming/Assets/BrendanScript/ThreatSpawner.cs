using System.Collections;
using UnityEngine;

public class ThreatSpawner : MonoBehaviour
{
    [Header("Threat Prefabs")]
    [SerializeField] private GameObject[] threatPrefabs;

    [Header("Spawn Settings")]
    [SerializeField] private float minSpawnInterval = 1f;
    [SerializeField] private float maxSpawnInterval = 5f;

    [Header("Spawn Area")]
    [SerializeField] private PolygonCollider2D spawnPolygon;

    private float spawninterval;

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
            SpawnThreat();
            spawninterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            // Debug.Log($"Waiting {spawninterval} seconds");
            yield return new WaitForSeconds(spawninterval);
        }
    }

    private void SpawnThreat()
    {
        if (threatPrefabs.Length == 0)
        {
            Debug.Log("No Prefabs");
            return;
        }

        GameObject selectedThreat = threatPrefabs[Random.Range(0, threatPrefabs.Length)];
        Vector3 spawnPosition = GetRandomPointInSpawnArea();

        Instantiate(selectedThreat, spawnPosition, Quaternion.identity);
    }

    private Vector2 GetRandomPointInSpawnArea()
    {
        Bounds bounds = spawnPolygon.bounds;

        for (int i = 0; i < 100; i++)
        {
            Vector2 point = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y));

            if (spawnPolygon.OverlapPoint(point))
            {
                return point;
            }
        }

        return spawnPolygon.bounds.center;
    }
}
