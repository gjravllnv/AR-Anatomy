using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropdownScript : MonoBehaviour, IPointerClickHandler
{
    public RectTransform container;
    public bool isOpen;

    private Vector3 closedScale = new Vector3(1, 0, 1);
    private Vector3 openScale = Vector3.one;

    public float animationSpeed = 12f;

    void Start()
    {
        // Find the container RectTransform
        container = transform.Find("Container")?.GetComponent<RectTransform>();

        if (container == null)
        {
            Debug.LogError("Container not found. Ensure there's a child named 'Container' with a RectTransform component.");
            return;
        }

        isOpen = false;

        // Initialize the scale to 0 (closed state)
        container.localScale = closedScale;
    }

    void Update()
    {
        if (container == null) return;

        // Smoothly interpolate the container's scale
        container.localScale = Vector3.Lerp(container.localScale, isOpen ? openScale : closedScale, Time.deltaTime * animationSpeed);
    }

    public void ToggleDropdown()
    {
        isOpen = !isOpen;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ToggleDropdown();
    }
}
