
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
    public RoundData preTestData;
    public RoundData postTestData;

    // public RoundData GetCurrentRoundData(string roundType)
    // {
    //     return roundType == "Pre-Test" ?  postTestData : preTestData;
    // }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log("DataController initialized. Waiting for user to start the game.");
    }

    public void StartGame(int sceneIndex)
    {
        LoadSceneByIndex(sceneIndex);
    }

    public void LoadNextScene(int offset = 1)
    {
        LoadSceneWithOffset(offset);
    }

    public void LoadPreviousScene(int offset = -1)
    {
        LoadSceneWithOffset(offset);
    }

    public void LoadStartScene()
    {
        LoadSceneByIndex(0);
    }

    public void RestartGame()
    {
        Debug.Log("Restarting the game...");
        
        // Reset round and scores
        PlayerPrefs.SetString("CurrentRound", "Pre-Test");
        PlayerPrefs.DeleteKey("PreTestScore");
        PlayerPrefs.DeleteKey("PostTestScore");
        PlayerPrefs.Save();

        SceneManager.LoadScene("StartScene"); // Replace with your start scene name
    }

    private void LoadSceneWithOffset(int offset)
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int targetIndex = currentIndex + offset;

        if (IsValidSceneIndex(targetIndex))
        {
            Debug.Log($"Loading scene {targetIndex}...");
            SceneManager.LoadScene(targetIndex);
        }
        else
        {
            Debug.LogError($"Invalid scene offset: {offset}. Target index {targetIndex} is out of range.");
        }
    }

    private void LoadSceneByIndex(int index)
    {
        if (IsValidSceneIndex(index))
        {
            Debug.Log($"Loading scene {index}...");
            SceneManager.LoadScene(index);
        }
        else
        {
            Debug.LogError($"Invalid scene index: {index}. Check your build settings.");
        }
    }

    private bool IsValidSceneIndex(int index)
    {
        return index >= 0 && index < SceneManager.sceneCountInBuildSettings;
    }
}
