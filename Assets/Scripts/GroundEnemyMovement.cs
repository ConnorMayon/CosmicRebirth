using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GroundEnemyMovement : MonoBehaviour
{
    public float enemySpeed = 1f;
    public float maxSpeed = 5f; // Maximum speed
    public float speedIncrease = 0.2f; // Speed increase per second
    public float switchProbability = 0.01f; // Probability to switch direction each frame

    // Update is called once per frame
    void Update()
    {
        // Speed up the enemy over time, but don't exceed maxSpeed
        enemySpeed = Mathf.Min(enemySpeed + speedIncrease * Time.deltaTime, maxSpeed);

        // Randomly decide whether to switch direction
        if (Random.value < switchProbability)
        {
            enemySpeed *= -1f;
        }

        // Move the enemy
        Vector3 pos = transform.position;
        pos.x -= enemySpeed * Time.deltaTime;
        transform.position = pos;
    }
}