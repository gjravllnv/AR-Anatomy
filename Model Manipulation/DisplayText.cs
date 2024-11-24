using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Make sure this is imported to work with UI components

public class DisplayText : MonoBehaviour
{
    public Text phaseDisplayText;  // Reference to a UI Text component
    private Touch theTouch;
    private float timeTouchEnded;
    private float displayTime = 0.5f;

    void Update()
    {
        // Check for touch or mouse input
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            // Determine input position (touch or mouse)
            Vector3 inputPosition = (Input.touchCount > 0) ? (Vector3)Input.GetTouch(0).position : (Vector3)Input.mousePosition;

            // Create a ray from the camera to the input position
            Ray ray = Camera.main.ScreenPointToRay(inputPosition);
            RaycastHit hit;

            // Perform a raycast and check if it hits the 3D model
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                // If touch ended or mouse click detected
                if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
                {
                    phaseDisplayText.text = "Touched " + gameObject.name;
                    timeTouchEnded = Time.time;
                }
            }
        }

        // Clear the text after displayTime
        if (Time.time - timeTouchEnded > displayTime)
        {
            phaseDisplayText.text = "";
        }
    }
}
