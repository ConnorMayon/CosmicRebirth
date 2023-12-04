using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private const float DeathThreshold = -10f; // Y-axis threshold for death
    private bool isInvincible = false;

    void Start()
    {
        Debug.Log("Player script is running.");
    }

    // Update is called once per frame
    void Update()
    {
        CheckForDeath();
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Only trigger when jumped on 
        if (collision.gameObject.CompareTag("Enemy") && (transform.position.y > (collision.transform.position.y + 3f)))
        {
            Destroy(collision.gameObject);
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(50); // Add 100 points for enemy kill
            }
        }

        else if(collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
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
            pos.x -= 1.8f;
            transform.position = pos;
        }
        else if (collision.gameObject.CompareTag("Enemy") && isInvincible)
        {
            Destroy(collision.gameObject);
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(100); // Add 100 points for enemy kill
            }
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
        SceneManager.LoadScene("EndScene"); // Use the exact name of your game over scene
        Debug.Log("Player Died");
    }

    public void SetInvincibility(bool invincible, float duration)
    {
        StartCoroutine(InvincibilityDuration(invincible, duration));
        Debug.Log("Invincibility set to: " + invincible);
    }

    IEnumerator InvincibilityDuration(bool invincible, float duration)
    {
        isInvincible = invincible;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }



}
