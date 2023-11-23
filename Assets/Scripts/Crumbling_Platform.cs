using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumbling_Platform : MonoBehaviour
{
    public float crumblingDuration = 3f; // Time it takes for the platform to crumble
    private bool isCrumbling = false;
    private Renderer platformRenderer;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCrumbling)
        {
            isCrumbling = true;
            StartCoroutine(Crumble());
        }
    }

    IEnumerator Crumble()
    {
        float elapsedTime = 0;
        Color originalColor = platformRenderer.material.color;
        Color targetColor = new Color(0, 0, 0, 0); // Assuming you want to fade to transparent black

        while (elapsedTime < crumblingDuration)
        {
            elapsedTime += Time.deltaTime;
            platformRenderer.material.color = Color.Lerp(originalColor, targetColor, elapsedTime / crumblingDuration);
            yield return null;
        }

        Destroy(gameObject);
    }
}