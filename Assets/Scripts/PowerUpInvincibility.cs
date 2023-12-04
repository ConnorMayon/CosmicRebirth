using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInvincibility : MonoBehaviour
{
    public float invincibilityDuration = 10f; // Duration of invincibility

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Granted Invincibility for 10 seconds");

        Player playerScript = null;

        if (other.CompareTag("Player"))
        {
            playerScript = other.GetComponent<Player>();
        }
        else if (other.transform.parent && other.transform.parent.CompareTag("Player"))
        {
            playerScript = other.transform.parent.GetComponent<Player>();
        }

        if (playerScript != null)
        {
            playerScript.SetInvincibility(true, invincibilityDuration);
            Destroy(gameObject);
        }
    }
}
