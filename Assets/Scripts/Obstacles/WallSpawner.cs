using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour {
    public GameObject wallPrefab;
    public float startingSpawnInterval = 3f;

    public float wallSpawnY = -3f;
    public float wallSpawnYVariation = 1f;
    public float bIntervalCalc = 133;
    public float minSpawnIntervalPercent = 0.5f;

    private float spawnInterval;
    private float nextSpawnTime;
    private float timer = 0f;

    private void Start() {
        transform.position = Camera.main.ViewportToWorldPoint(new Vector2(1.2f, 0.5f));
        spawnInterval = startingSpawnInterval;
        nextSpawnTime = timer + spawnInterval;
    }

    private void Update() {
        if (GameController.Instance.paused) return;
        
        if (timer >= nextSpawnTime) {
            SpawnWall();

            // recalculate interval based on timer
            RecalculateInterval();
            Debug.Log(spawnInterval);
            nextSpawnTime += spawnInterval;
        }

        timer += Time.deltaTime;
    }

    private void SpawnWall() {
        float y = wallSpawnY + Random.Range(-wallSpawnYVariation, wallSpawnYVariation);
        Instantiate(
            wallPrefab,
            new Vector2(transform.position.x, y),
            Quaternion.identity
        );
    }

    private void RecalculateInterval() {
        float percent = Mathf.Clamp(
            1 - (Mathf.Pow(timer, 2) / Mathf.Pow(bIntervalCalc, 2)), 
            minSpawnIntervalPercent,
            1f);
        float newInterval = startingSpawnInterval * percent;

        spawnInterval = newInterval;
    }
}