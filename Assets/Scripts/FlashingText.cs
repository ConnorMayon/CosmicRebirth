using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashMessage : MonoBehaviour
{
    public Text flashText;
    public float flashDuration = 2.0f;
    public float flashInterval = 0.5f;

    private void Start()
    {
        // Ensure the text is not visible at the start
        flashText.enabled = false;
    }

    public void ShowMessage(string message, float additionalTime)
    {
        StopAllCoroutines(); // Stop any previous flashing routines
        StartCoroutine(FlashRoutine(message + " +" + additionalTime.ToString() + "s"));
    }

    IEnumerator FlashRoutine(string message)
    {
        flashText.text = message;
        flashText.enabled = true;

        float elapsedTime = 0;
        bool isVisible = true;

        while (elapsedTime < flashDuration)
        {
            isVisible = !isVisible;
            flashText.enabled = isVisible;
            elapsedTime += flashInterval;
            yield return new WaitForSeconds(flashInterval);
        }

        flashText.enabled = false;
    }
}
