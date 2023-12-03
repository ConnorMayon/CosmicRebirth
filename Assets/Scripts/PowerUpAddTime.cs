using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAddTime : MonoBehaviour
{
    public float timeToAdd = 30f; // The amount of time this power-up adds

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Add time logic
            CountdownTimer countdownTimer = FindObjectOfType<CountdownTimer>();
            if (countdownTimer != null)
            {
                countdownTimer.AddTime(timeToAdd);
            }

            // Optionally, add visual/sound effect here

            Destroy(gameObject); // Destroy the power-up
        }
    }
}
