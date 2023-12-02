using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public float timeToAdd = 30f; // Time to add when the checkpoint is reached

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your player has the tag "Player"
        {
            CountdownTimer countdownTimer = FindObjectOfType<CountdownTimer>();
            if (countdownTimer != null)
            {
                FlashMessage flashMessage = FindObjectOfType<FlashMessage>();
                if (flashMessage != null)
                {
                    flashMessage.ShowMessage("Time Added", timeToAdd);
                }
                countdownTimer.AddTime(timeToAdd);
                // Optionally, deactivate the checkpoint to prevent re-triggering
                gameObject.SetActive(false);
            }
        }
    }
}
