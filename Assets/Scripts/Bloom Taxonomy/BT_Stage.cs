using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BT_Stage : MonoBehaviour
{
    public Button[] buttons; 
    public Color disabledColor = Color.gray; 
    public Slider progressBar; 
    public TMP_Text progressText; 
    public int totalLevels = 6; 

    public string currentTopic; // Assign this dynamically when switching topics

    private string GetLevelKey()
    {
        return $"UnlockedLevel_{currentTopic}"; // ✅ Ensure each topic has its own progress
    }

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt(GetLevelKey(), 1); // ✅ Load per-topic progress
        UpdateButtonState(unlockedLevel);
        UpdateProgressBar(unlockedLevel);
    }

    public void OpenLevel(int levelId)
    {
        SceneManager.LoadScene(levelId); 
    }

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

    private void UpdateProgressBar(int unlockedLevel)
    {
        float progress = (float)unlockedLevel / totalLevels; 
        progressBar.value = progress;
    }

    public void CompleteLevel(int levelId)
    {
        int unlockedLevel = PlayerPrefs.GetInt(GetLevelKey(), 1); // ✅ Get per-topic progress

        if (levelId + 1 > unlockedLevel)  
        {
            unlockedLevel = levelId + 1;  // ✅ Unlock ONLY the next level (Fix)
            PlayerPrefs.SetInt(GetLevelKey(), unlockedLevel); // ✅ Store progress properly
        }

        PlayerPrefs.Save(); // ✅ Ensure changes are saved

        UpdateButtonState(unlockedLevel); // ✅ Update button states
        UpdateProgressBar(unlockedLevel); // ✅ Update progress bar
    }

    public void ResetProgress() 
    {
        PlayerPrefs.DeleteKey(GetLevelKey()); // ✅ Reset only current topic progress
        PlayerPrefs.SetInt(GetLevelKey(), 1);  // ✅ Reset to level 1
        UpdateButtonState(1); 
        UpdateProgressBar(1); 
    }
}
