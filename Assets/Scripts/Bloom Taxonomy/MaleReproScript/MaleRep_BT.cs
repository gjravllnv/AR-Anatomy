using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this for Button and Slider
using UnityEngine.SceneManagement;
using TMPro;

public class MaleRep_BT : MonoBehaviour
{
    public Button[] buttons; // Buttons for each level
    public Color disabledColor = Color.gray; // Color for disabled buttons
    public Slider progressBar; // Progress bar to show progress
    public TMP_Text progressText; // Text to display progress
    public int totalLevels = 6; // Total number of levels for this topic

    private string currentTopic = "MaleReproductive"; // Topic name for the Male Reproductive system

    // Key used to track progress in PlayerPrefs
    private string GetLevelKey()
    {
        return $"UnlockedLevel_{currentTopic}";
    }

    private void Awake()
    {
        // Load the current unlocked level for the Male Reproductive topic
        int unlockedLevel = PlayerPrefs.GetInt(GetLevelKey(), 1);
        UpdateButtonState(unlockedLevel);
        UpdateProgressBar(unlockedLevel);
    }

    // Open the corresponding level when a button is clicked
    public void OpenLevel(int levelId)
    {
        SceneManager.LoadScene(levelId); 
    }

    // Update the button state based on the unlocked level
    private void UpdateButtonState(int unlockedLevel)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            bool isUnlocked = i < unlockedLevel;
            buttons[i].interactable = isUnlocked;

            ColorBlock cb = buttons[i].colors;
            cb.disabledColor = disabledColor;
            buttons[i].colors = cb;
        }
    }

    // Update the progress bar based on the current unlocked level
    private void UpdateProgressBar(int unlockedLevel)
    {
        float progress = (float)unlockedLevel / totalLevels; 
        progressBar.value = progress;
        // progressText.text = $"Level {unlockedLevel}/{totalLevels}";
    }

    // Called when a level is completed to unlock the next one
    public void CompleteLevel(int levelId)
    {
        int unlockedLevel = PlayerPrefs.GetInt(GetLevelKey(), 1);

        if (levelId + 1 > unlockedLevel) 
        {
            unlockedLevel = levelId + 1;
            PlayerPrefs.SetInt(GetLevelKey(), unlockedLevel);
        }

        PlayerPrefs.Save(); // Save progress
        UpdateButtonState(unlockedLevel); // Update button states
        UpdateProgressBar(unlockedLevel); // Update progress bar
    }

    // Reset progress for the Male Reproductive topic
    public void ResetProgress() 
    {
        PlayerPrefs.DeleteKey(GetLevelKey()); // Delete the progress key
        PlayerPrefs.SetInt(GetLevelKey(), 1); // Reset to level 1
        UpdateButtonState(1); // Update buttons
        UpdateProgressBar(1); // Update progress bar
    }
}
