using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumbling_Platform : MonoBehaviour
{
    public float crumblingDelay = 3f; // Time before the platform gets destroyed
    private bool isCrumbling = false;
    private Renderer platformRenderer;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
    Debug.Log("OnTriggerEnter was called with: " + collision.gameObject.name);
    if (collision.gameObject.CompareTag("Player") && !isCrumbling)
    {
        Debug.Log("Crumbling started.");
        isCrumbling = true;
        StartCoroutine(CrumbleAndDestroy());
    }
}

    IEnumerator CrumbleAndDestroy()
    {
        float elapsedTime = 0;

        // Get the initial color of the platform
        Color originalColor = platformRenderer.material.color;
        Color targetColor = new Color(0, 0, 0, originalColor.a); // Target color is black with the original alpha

        while (elapsedTime < crumblingDelay)
        {
            // Interpolate color from original to darker color over time
            platformRenderer.material.color = Color.Lerp(originalColor, targetColor, elapsedTime / crumblingDelay);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Destroy the platform
        Destroy(gameObject);
    }

}