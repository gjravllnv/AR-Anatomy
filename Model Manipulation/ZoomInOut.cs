using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomInOut : MonoBehaviour
{
    private Touch touch;
    private Vector2 touchPosition;
    private float initialDistance;
    private Vector3 initialScale;

    private float zoomSpeedModifier = 0.01f; // Adjust this value to control zoom speed

    // Update is called once per frame
    void Update()
    {
        // Check if there are any touches on the screen
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);

            // Handle single-touch rotation if you still want to keep that feature
            if (touch.phase == TouchPhase.Moved)
            {
                // Handle rotation code here if needed
            }
        }
        // Check for two-finger touch to handle zoom
        else if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Check if either of the two touches just began
            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                initialScale = transform.localScale;
            }
            // Check if the touches are moving
            else if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
            {
                // Calculate the current distance between touches
                float currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                // If there's a significant change in distance, zoom in or out
                if (Mathf.Approximately(initialDistance, 0))
                {
                    return; // If initial distance is zero, return
                }

                // Calculate the scaling factor based on the change in distance
                float scaleFactor = currentDistance / initialDistance;

                // Apply the scale factor to the object
                transform.localScale = initialScale * scaleFactor;
            }
        }
    }
}
