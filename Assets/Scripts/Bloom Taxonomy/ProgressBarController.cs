using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    [Header("UI References")]
    public Slider progressBar;
    public Button nextButton;

    [Header("Panel Navigation")]
    public GameObject[] panels;
    private int currentIndex = 0;

    [Header("Progress Settings")]
    [Range(0.1f, 1.0f)]
    public float increaseAmount = 0.2f; // Adjust based on panel count

    private Vector2 touchStartPos; // Stores the start position of a swipe

    void Start()
    {
        ShowPanel(currentIndex);

        if (progressBar != null)
            progressBar.value = 0;

        if (nextButton != null)
            nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    void Update()
    {
        HandleSwipe();
    }

    private void HandleSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    float swipeDistance = touch.position.x - touchStartPos.x;

                    if (Mathf.Abs(swipeDistance) > 100) // Minimum swipe distance
                    {
                        if (swipeDistance < 0) // Swipe Left (Next Panel)
                        {
                            NextPanel();
                        }
                        else if (swipeDistance > 0) // Swipe Right (Previous Panel)
                        {
                            PreviousPanel();
                        }
                    }
                    break;
            }
        }
    }

    public void IncreaseProgress()
    {
        if (progressBar != null)
        {
            progressBar.value += increaseAmount;
            progressBar.value = Mathf.Clamp01(progressBar.value);
        }
    }

    public void DecreaseProgress()
    {
        if (progressBar != null)
        {
            progressBar.value -= increaseAmount;
            progressBar.value = Mathf.Clamp01(progressBar.value);
        }
    }

    void OnNextButtonClicked()
    {
        NextPanel(); // Move to next panel
    }

    private void NextPanel()
    {
        if (currentIndex < panels.Length - 1)
        {
            currentIndex++;
            ShowPanel(currentIndex);
            IncreaseProgress(); // Increase progress when moving forward
        }
    }

    private void PreviousPanel()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            ShowPanel(currentIndex);
            DecreaseProgress(); // Decrease progress when moving backward
        }
    }

    private void ShowPanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == index);
        }
    }
}
