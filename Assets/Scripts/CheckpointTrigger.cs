using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public float timeToAdd = 90f; // Time to add when the checkpoint is reached
    private Map_Generation mapGenerator;

    private void Start()
    {
        mapGenerator = FindObjectOfType<Map_Generation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your player has the tag "Player"
        {
            CountdownTimer countdownTimer = FindObjectOfType<CountdownTimer>();
            if (countdownTimer != null)
            {
                ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
                if (scoreManager != null)
                {
                    scoreManager.AddScore(100); // Add 100 points for checkpoint
                }
                FlashMessage flashMessage = FindObjectOfType<FlashMessage>();
                if (flashMessage != null)
                {
                    flashMessage.ShowMessage("Time Added", timeToAdd);
                }
                countdownTimer.AddTime(timeToAdd);
                // Optionally, deactivate the checkpoint to prevent re-triggering
                gameObject.SetActive(false);
                mapGenerator.DespawnPreviousPlatforms(transform.position.x);

                // Switch the aesthetic theme - map will already generate with theme-specific enemies and platforms
                Themes.themes.updateCurrentTheme();

            }
        }
    }
}
