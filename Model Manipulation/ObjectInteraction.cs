using System.Collections;
using System.Collections.Generic;
using UnityEngine;  // This is required for Unity-related types like GameObject
using UnityEngine.UI;  // Required for UI components

public class ObjectInteraction : MonoBehaviour
{
    public GameObject canvas;  // GameObject is a Unity type for referencing objects in the scene
    public Text displayText;   // Text is for handling UI text components

    private void Start()
    {
        canvas.SetActive(false); // Ensure the canvas is hidden initially
    }

    private void Update()
    {
        // Check if a valid camera exists
        if (Camera.main == null)
        {
            Debug.LogWarning("Main camera not found!");
            return;
        }

        // Determine input position based on platform (touch or mouse)
        Vector3 inputPosition = Vector3.zero;

        if (Input.touchCount > 0)
        {
            // Touch input
            inputPosition = Input.GetTouch(0).position;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            // Mouse input
            inputPosition = Input.mousePosition;
        }
        else
        {
            // If no input, skip further processing
            return;
        }

        // Check if inputPosition is valid (no infinity or NaN values)
        if (float.IsInfinity(inputPosition.x) || float.IsInfinity(inputPosition.y))
        {
            Debug.LogWarning("Input position is invalid.");
            return;
        }

        // Raycast to determine if the touch or click hits the object
        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider != null && hit.collider.gameObject == gameObject)
        {
            // Activate the canvas and set the text when the object is clicked/touched
            canvas.SetActive(true);
            displayText.text = "Ovary";  // Change this to the desired message
        }
    }
}
