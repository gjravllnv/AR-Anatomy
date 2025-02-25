using UnityEngine;
using UnityEngine.UI;  // For the Text component
using System.Collections;
using TMPro;

public class TimedMessage : MonoBehaviour
{
    public TMP_Text messageText;  // Drag the UI Text object here in the Inspector
    private float messageDuration = 5f;  // Duration for the message to stay visible

    void Start()
    {
        ShowMessage("Point your camera at a flat surface");
    }

    // Function to show the message for a specified duration
    void ShowMessage(string message)
    {
        messageText.text = message;
        StartCoroutine(HideMessageAfterDelay());
    }

    // Coroutine to hide the message after the delay
    IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);
        messageText.text = "";  // Clears the text
    }
}
