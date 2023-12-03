using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMystery : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign in the editor
    public float timeToAdd = 30f;
    public float invincibilityDuration = 10f;
    public float spawnDistance = 5f; // Distance in front of the player to spawn the enemy

    private GameObject playerObject;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerObject)
        {
            int randomEffect = Random.Range(0, 3); // 0, 1, or 2

            switch (randomEffect)
            {
                case 0:
                    AddTime();
                    break;
                case 1:
                    GrantInvincibility();
                    break;
                case 2:
                    SpawnEnemy();
                    break;
            }

            Destroy(gameObject); // Destroy the power-up
        }
    }

    private void AddTime()
    {
        CountdownTimer countdownTimer = playerObject.GetComponent<CountdownTimer>();
        if (countdownTimer != null)
        {
            countdownTimer.AddTime(timeToAdd);
        }
    }

    private void GrantInvincibility()
    {
        Player playerScript = playerObject.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.SetInvincibility(true, invincibilityDuration);
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            Vector3 spawnPosition = playerObject.transform.position + playerObject.transform.forward * spawnDistance;

            spawnPosition.y = enemyPrefab.transform.position.y;
            spawnPosition.z = playerObject.transform.position.z;

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}