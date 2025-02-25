using UnityEngine;
using TMPro; // For TextMeshPro support
using System.Collections;

public class ShowModelAfterDelay : MonoBehaviour
{
    public GameObject modelToShow; // The model to show after delay
    public float delayInSeconds = 2f; // Delay time, set in Inspector

    // Text fields for each part's name and function (assign in the Inspector)
    public TextMeshProUGUI[] partNameTexts;
    public TextMeshProUGUI[] partFunctionTexts;

    private bool modelShown = false; // Flag to ensure interactions happen only after the model is shown

    private void Start()
    {
        // Ensure the model and all texts are hidden at the start
        if (modelToShow != null)
            modelToShow.SetActive(false);
        HideAllText();

        // Start coroutine to show the model after delay
        StartCoroutine(ShowModelWithDelay());
    }

    private IEnumerator ShowModelWithDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (modelToShow != null)
            modelToShow.SetActive(true);
        modelShown = true; // Enable interactions
    }

    // Call this function when a part of the model is touched
    public void OnPartTouched(int partNumber, string partName, string functionDescription)
    {
        if (!modelShown || partNumber < 1 || partNumber > partNameTexts.Length) return;

        // Update text and make it visible for the corresponding part
        partNameTexts[partNumber - 1].text = partName;
        partFunctionTexts[partNumber - 1].text = functionDescription;

        ShowText(partNameTexts[partNumber - 1], partFunctionTexts[partNumber - 1]);
    }

    private void HideAllText()
    {
        for (int i = 0; i < partNameTexts.Length; i++)
        {
            if (partNameTexts[i] != null)
                partNameTexts[i].gameObject.SetActive(false);

            if (partFunctionTexts[i] != null)
                partFunctionTexts[i].gameObject.SetActive(false);
        }
    }

    private void ShowText(TextMeshProUGUI partNameText, TextMeshProUGUI partFunctionText)
    {
        if (partNameText != null)
            partNameText.gameObject.SetActive(true);

        if (partFunctionText != null)
            partFunctionText.gameObject.SetActive(true);
    }
}
