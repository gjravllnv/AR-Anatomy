using UnityEngine;
using TMPro;

public class ARObjectTouch : MonoBehaviour
{
    public TMP_Text partNameText;
    public TMP_Text functionText;
    public Color highlightColor = Color.blue;

    private Color originalColor;
    private Renderer modelRenderer;

    // Keep track of the currently selected object
    private static ARObjectTouch currentSelectedObject = null;

    void Start()
    {
        // Get the Renderer component of the object for highlighting
        modelRenderer = GetComponent<Renderer>();
        if (modelRenderer != null)
        {
            originalColor = modelRenderer.material.color;
        }
        else
        {
            Debug.LogError("No Renderer found on the object.");
        }

        if (partNameText != null)
            partNameText.gameObject.SetActive(false);
        else
            Debug.LogError("Part name TextMeshPro UI not assigned!");

        if (functionText != null)
            functionText.gameObject.SetActive(false);
        else
            Debug.LogError("Function TextMeshPro UI not assigned!");

        if (GetComponent<Collider>() == null)
        {
            Debug.LogError("No Collider found on the object. Please add a Collider.");
        }
    }

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // Perform raycast to detect the touched object
                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the object touched is this object
                    if (hit.transform == this.transform)
                    {
                        SelectObject();
                    }
                    else if (currentSelectedObject != null)
                    {
                        // If touching a different area, reset the current selected object
                        currentSelectedObject.DeselectObject();
                    }
                }
            }
        }
    }

    // Method to select and highlight this object
    void SelectObject()
    {
        // If there's another selected object, deselect it first
        if (currentSelectedObject != null && currentSelectedObject != this)
        {
            currentSelectedObject.DeselectObject();
        }

        // Set this object as the currently selected one
        currentSelectedObject = this;

        // Show the part name and function
        if (partNameText != null)
            partNameText.gameObject.SetActive(true);

        if (functionText != null)
            functionText.gameObject.SetActive(true);

        // Highlight the object
        if (modelRenderer != null)
        {
            modelRenderer.material.color = highlightColor;
        }
    }

    // Method to deselect and reset this object
    public void DeselectObject()
    {
        // Hide the text
        if (partNameText != null)
            partNameText.gameObject.SetActive(false);

        if (functionText != null)
            functionText.gameObject.SetActive(false);

        // Reset the highlight
        if (modelRenderer != null)
        {
            modelRenderer.material.color = originalColor;
        }

        // Clear the current selected object if this one is being deselected
        if (currentSelectedObject == this)
        {
            currentSelectedObject = null;
        }
    }
} 