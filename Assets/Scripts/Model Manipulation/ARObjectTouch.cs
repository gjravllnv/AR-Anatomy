using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ARObjectTouch : MonoBehaviour
{   
    [Header("Text Elements")]
    public TMP_Text partNameText; 
    public TMP_Text functionText; 

    [Header("Panel Elements")]
    public GameObject panel; 
    public Color highlightColor = Color.green; 
    private Color originalColor;
    private Renderer modelRenderer;
    private static ARObjectTouch currentSelectedObject; // Track currently selected object
    private LayerMask touchableLayer; 
   
    void Start()
    {
        modelRenderer = GetComponent<Renderer>();
        if (modelRenderer)
            originalColor = modelRenderer.material.color;

        if (!partNameText || !functionText || !panel)
            Debug.LogError("UI elements (Text or Panel) are not assigned!");


        HideUI();

        if (!GetComponent<Collider>())
            Debug.LogError($"No Collider on {gameObject.name}. Please add one.");

        
        touchableLayer = LayerMask.GetMask("Interactable");
        int interactableLayer = LayerMask.NameToLayer("Interactable");
        if (interactableLayer == -1)
        {
            Debug.LogError("Layer 'Interactable' does not exist. Please create it in Unity's Layer Manager.");
        }
        else
        {
            if (gameObject.layer != interactableLayer)
            {
                Debug.LogWarning($"{gameObject.name} is not on the 'Interactable' layer. Setting it now.");
                gameObject.layer = interactableLayer;
            }
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                CheckTouch(touch.position);
        }
    }

    private void CheckTouch(Vector2 touchPosition)
    {
         Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, touchableLayer))
        {
            Debug.Log($"Hit object: {hit.transform.name}");
            var touched = hit.transform.GetComponent<ARObjectTouch>();
            if (touched)
            {
                Debug.Log($"Selected object: {touched.name}");
                touched.Select();
            }
        }
        else
        {
            Debug.Log("No object hit");
            currentSelectedObject?.Deselect();
        }
    }

    public void Select()
    {
        if (currentSelectedObject && currentSelectedObject != this)
            currentSelectedObject.Deselect();

        currentSelectedObject = this;
         if (modelRenderer)
            modelRenderer.material.color = highlightColor;

        ShowUI(gameObject.name); 
    }

    public void Deselect()
    {
        if (modelRenderer)
            modelRenderer.material.color = originalColor;

        HideUI();
        if (currentSelectedObject == this)
            currentSelectedObject = null;
    }

    private void ShowUI(string name) // Show the panel
    {
        partNameText.text = name;

        partNameText.gameObject.SetActive(true);
        functionText.gameObject.SetActive(true);
        panel.SetActive(true); 
    }

    private void HideUI() // Hide the panel
    {
        partNameText?.gameObject.SetActive(false);
        functionText?.gameObject.SetActive(false);
        panel?.SetActive(false); 
    }
}
