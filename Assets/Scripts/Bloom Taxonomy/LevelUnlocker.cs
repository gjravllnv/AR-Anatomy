using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUnlocker : MonoBehaviour
{
    public int levelSelectionSceneIndex = 0; // Set this to the Level Selection Scene index
    public string currentTopic; // ✅ Set dynamically when selecting a topic

    private string GetLevelKey()
    {
        return $"UnlockedLevel_{currentTopic}"; // ✅ Track each topic's progress separately
    }

    public void CompleteLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int unlockedLevel = PlayerPrefs.GetInt(GetLevelKey(), 1); // ✅ Load correct level progress

        if (currentLevel >= unlockedLevel) // ✅ Only unlock next level
        {
            PlayerPrefs.SetInt(GetLevelKey(), unlockedLevel + 1); // ✅ Properly unlock the next level
        }

        PlayerPrefs.Save(); // ✅ Ensure progress is saved
        SceneManager.LoadScene(levelSelectionSceneIndex); // ✅ Load Level Selection Scene
    }
}
