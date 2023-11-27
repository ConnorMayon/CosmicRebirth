using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumbling_Platform : MonoBehaviour
{
    public float crumblingDelay = 10f; // Time before the platform gets destroyed
    private bool isCrumbling = false;
    private Renderer platformRenderer;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
    }

void OnTriggerEnter(Collider other)
{
    Debug.Log("OnTriggerEnter was called with: " + other.gameObject.name);
    if (other.CompareTag("Player") && !isCrumbling)
    {
        Debug.Log("Crumbling started.");
        isCrumbling = true;
        StartCoroutine(CrumbleAndDestroy());
    }
}

    IEnumerator CrumbleAndDestroy()
    {
        // Optionally fade out or change the platform's appearance
        // Wait for the specified delay
        yield return new WaitForSeconds(crumblingDelay);

        // Destroy the platform
        Destroy(gameObject);
    }
}