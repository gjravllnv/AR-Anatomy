using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MaleRep_LevelUnlocker : MonoBehaviour
{
    public int levelSelectionSceneIndex = 0; // Set this to the Level Selection Scene index
    public string currentTopic = "MaleReproductive"; // Topic is "MaleReproductive"

    private string GetLevelKey()
    {
        return $"UnlockedLevel_{currentTopic}";
    }

    public void CompleteLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentLevel + 0; 

        int unlockedLevel = PlayerPrefs.GetInt(GetLevelKey(), 0); // Get stored progress for the topic

        if (nextLevel > unlockedLevel)
        {
            PlayerPrefs.SetInt(GetLevelKey(), nextLevel); // Store next level progress per topic
        }

        PlayerPrefs.Save(); // Ensure data is saved
        SceneManager.LoadScene(levelSelectionSceneIndex); // Load level selection scene
    }
}
