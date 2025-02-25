using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipNextButton : MonoBehaviour
{
  
    public void LoadNextScene(int offset = 1) => LoadScene(offset);

    public void LoadPreviousScene(int offset = -1) => LoadScene(offset);

    private void LoadScene(int offset)
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + offset);
    }
  
    public void LoadStartScene() => SceneManager.LoadScene(0);

    public void SkipToHomeScreen() => LoadScene(2);

    public void SkipOnboardingToHomeScreen() => LoadScene(3);
}
