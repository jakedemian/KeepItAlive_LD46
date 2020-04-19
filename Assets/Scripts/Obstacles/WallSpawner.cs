using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour {
    public GameObject wallPrefab;
    public float startingSpawnInterval = 3f;

    public float wallSpawnY = -3f;
    public float wallSpawnYVariation = 1f;

    private float spawnInterval;
    private float nextSpawnTime;
    private float timer = 0f;

    void Start() {
        transform.position = Camera.main.ViewportToWorldPoint(new Vector2(1.2f, 0.5f));
        spawnInterval = startingSpawnInterval;
        nextSpawnTime = timer + spawnInterval;
    }

    void Update() {
        if (timer >= nextSpawnTime) {
            SpawnWall();

            // recalculate interval based on timer
            RecalculateInterval();
            nextSpawnTime += spawnInterval;
        }

        timer += Time.deltaTime;
    }

    void SpawnWall() {
        float y = wallSpawnY + Random.Range(-wallSpawnYVariation, wallSpawnYVariation);
        Instantiate(
            wallPrefab,
            new Vector2(transform.position.x, y),
            Quaternion.identity
        );
    }

    void RecalculateInterval() {
        
    }
}