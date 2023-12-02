using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject groundEnemyPrefab;
    public GameObject skyEnemyPrefab;
    public GameObject player;
    public float initialSpawnDelay = 10; // Initial time between spawns
    public float maxSpawnRate = 0.1f; // Corresponds to 90% spawn rate
    public float spawnRateIncrease = 0.005f; // Increment rate
    public float spawnDistance = 10f; // Distance in front of the player to spawn enemies

    private float currentSpawnDelay;
    private float timeSinceLastSpawn;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        currentSpawnDelay = initialSpawnDelay;
        StartCoroutine(IncreaseSpawnRateOverTime());
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= currentSpawnDelay)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (player == null) return;

        // Randomly choose enemy type to spawn
        GameObject enemyPrefab = Random.Range(0, 2) == 0 ? groundEnemyPrefab : skyEnemyPrefab;

        // Calculate spawn position in front of the player along the X-axis
        Vector3 spawnDirection = player.transform.right; // Assuming the player's right is the forward direction along the X-axis
        Vector3 spawnPosition = player.transform.position + spawnDirection * spawnDistance;

        // Set Y to be the same as the ground enemy's Y, adjust if needed for sky enemies
        spawnPosition.y = groundEnemyPrefab.transform.position.y;
        spawnPosition.z = player.transform.position.z;

        // Instantiate the enemy at the calculated position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }


    IEnumerator IncreaseSpawnRateOverTime()
    {
        while (currentSpawnDelay > maxSpawnRate)
        {
            yield return new WaitForSeconds(1f); // Adjust the time interval as needed
            currentSpawnDelay -= spawnRateIncrease;
        }
    }
}
