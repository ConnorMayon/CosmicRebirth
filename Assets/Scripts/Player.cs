using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private const float DeathThreshold = -10f; // Y-axis threshold for death

    // Update is called once per frame
    void Update()
    {
        CheckForDeath();
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Only trigger when jumped on 
        if (collision.gameObject.CompareTag("Enemy") && (transform.position.y > (collision.transform.position.y + 2.6f)))
        {
            Debug.Log("Player killed enemy");

            Destroy(collision.gameObject);
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(50); // Add 100 points for enemy kill
            }
        }

        else if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player was hit by enemy");
            
            CountdownTimer countdownTimer = FindObjectOfType<CountdownTimer>();
            float timeToAdd = -5;
            if (countdownTimer != null)
            {
                FlashMessage flashMessage = FindObjectOfType<FlashMessage>();
                if (flashMessage != null)
                {
                    flashMessage.ShowMessage("Time Detracted ", timeToAdd);
                }
                countdownTimer.AddTime(timeToAdd);
            }

            Vector3 pos = transform.position;
            pos.x -= 3f;
            transform.position = pos;
        }

    }

    private void CheckForDeath()
    {
        // Check if the player has fallen below the death threshold
        if (transform.position.y < DeathThreshold)
        {
            Debug.Log("Player has fallen and died");

            // Handle player death here
            HandlePlayerDeath();
        }
    }


    private void HandlePlayerDeath()
    {
        // Load the Game Over scene
        // SceneManager.LoadScene("GameOver"); // Use the exact name of your game over scene
        Debug.Log("Player Died");
    }
}
