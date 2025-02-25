using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour {

    public void StartGame()
    {
        SceneManager.LoadScene("PreTestScene");

        PlayerPrefs.SetString("CurrentRound", "Pre-Test");
        PlayerPrefs.Save();
    }

    public void PostTestScene()
    {
        SceneManager.LoadScene("PostTestScene");
    }

}