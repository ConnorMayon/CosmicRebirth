using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject[] powerUps; // array of powerups
    public GameObject player;
    public float minSpawnDelay = 15f;
    public float maxSpawnDelay = 35f;
    public float spawnDelay = 0f;
    public float spawnDistance = 10f; // distance in front of player to spawn powerups

    private float timeSinceLastSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // get random spawn delay
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnDelay) 
        {
            SpawnPowerup();
            timeSinceLastSpawn = 0f;
        }
    }

    void SpawnPowerup()
    {
        // randomly select a powerup from the array
        GameObject randomPowerUp = powerUps[Random.Range(0, powerUps.Length)];

        // find spawn position
        Vector3 spawnDirection = player.transform.right;
        Vector3 spawnPosition = player.transform.position + spawnDirection * spawnDistance;
        spawnPosition.z = player.transform.position.z;

        // spawn powerup
        Instantiate(randomPowerUp, spawnPosition, Quaternion.identity);

        // set new random spawn delay
        spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);

    }
}
