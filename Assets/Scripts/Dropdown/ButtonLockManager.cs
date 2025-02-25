using UnityEngine;
using UnityEngine.UI;

public class ButtonLockManager : MonoBehaviour
{
    public Button button1; // Reference to Button1
    public Button button2; // Reference to Button2
    private CanvasGroup button2CanvasGroup; // For optional visual feedback

    void Start()
    {
        // Get the CanvasGroup component from Button2 if available
        button2CanvasGroup = button2.GetComponent<CanvasGroup>();

        // Check saved state and initialize Button2
        if (PlayerPrefs.GetInt("Button2Unlocked", 0) == 1)
        {
            UnlockButton2();
        }
        else
        {
            LockButton2();
        }

        // Add a listener to Button1
        button1.onClick.AddListener(OnButton1Clicked);
    }

    void OnButton1Clicked()
    {
        // Unlock Button2 when Button1 is clicked
        UnlockButton2();

        // Save the state persistently
        PlayerPrefs.SetInt("Button2Unlocked", 1);
        PlayerPrefs.Save();
    }

    void LockButton2()
    {
        // Disable interaction and set visual feedback
        button2.interactable = false;
        if (button2CanvasGroup != null)
        {
            button2CanvasGroup.alpha = 5.0f; // Make it appear faded
            button2CanvasGroup.blocksRaycasts = false;
        }
    }

    void UnlockButton2()
    {
        // Enable interaction and reset visual feedback
        button2.interactable = true;
        if (button2CanvasGroup != null)
        {
            button2CanvasGroup.alpha = 5f; // Make it fully visible
            button2CanvasGroup.blocksRaycasts = true;
        }
    }
}
