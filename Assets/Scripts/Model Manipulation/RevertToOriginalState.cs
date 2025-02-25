using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Required for using UI elements like Button

public class RevertToOriginalState : MonoBehaviour
{
    private Vector3 initialScale;
    private Quaternion initialRotation;

    void Start()
    {
        // Store the initial scale and rotation of the object
        initialScale = transform.localScale;
        initialRotation = transform.rotation;
        Debug.Log("Initial Scale: " + initialScale);
        Debug.Log("Initial Rotation: " + initialRotation.eulerAngles);
    }

    // This method will be called by the button to revert the object
    public void RevertObject()
    {
        Debug.Log("Revert button clicked");
        StartCoroutine(RevertToOriginalStateCoroutine());
    }

    private IEnumerator RevertToOriginalStateCoroutine()
    {
        Debug.Log("Starting revert...");
        
        float duration = 0.5f; // Duration of the revert
        float elapsedTime = 0f;

        Vector3 currentScale = transform.localScale;
        Quaternion currentRotation = transform.rotation;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(currentScale, initialScale, elapsedTime / duration);
            transform.rotation = Quaternion.Lerp(currentRotation, initialRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            Debug.Log($"Reverting... {elapsedTime}s");
            yield return null;
        }

        // Ensure the final position and scale are exactly the original values
        transform.localScale = initialScale;
        transform.rotation = initialRotation;

        Debug.Log("Reverted to original state");
    }
}