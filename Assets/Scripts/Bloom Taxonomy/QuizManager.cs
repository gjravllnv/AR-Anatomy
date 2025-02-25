using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Answer
    {
        public string answerText;
        public bool isCorrect;
    }

    [System.Serializable]
    public class Question
    {
        public string[] questionVariations; // Different rephrased versions
        public Answer[] answers;
    }

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI feedbackText; // ✅ Feedback TextMeshPro
    public TextMeshProUGUI scoreFeedbackText; // ✅ Feedback TextMeshPro
    public TextMeshProUGUI scoreText; // ✅ Score TextMeshPro
    public TextMeshProUGUI quizAttemptText; // ✅ Quiz attempts counter

    public Button[] optionButtons;
    public GameObject wellDonePanel;
    public GameObject scorePanel; // ✅ Panel to show final score

    public int mainMenuSceneIndex = 0; //
    public Slider progressBar; //

    public Question[] questions;

    private int currentQuestionIndex = 0;
    private bool quizActive = true;
    private bool quizStarted = false;
    private int correctAnswerCount = 0;
    
    private Color defaultButtonColor;
    private Color correctAnswerColor = new Color(0.325f, 0.620f, 0.314f);
    private Color incorrectAnswerColor = new Color(0.859f, 0.298f, 0.278f);

    public string currentTopic; // ✅ This should be set dynamically based on the topic the user selects

    private void Start()
    {
        if (optionButtons.Length > 0)
        {
            defaultButtonColor = optionButtons[0].image.color;
        }

        UpdateQuizAttemptCounter();
        LoadQuestion();
    }

    private void LoadQuestion()
    {
        if (!quizStarted)
        {
            IncrementQuizAttempt();
            quizStarted = true;
        }

        if (currentQuestionIndex < questions.Length)
        {
            Question q = questions[currentQuestionIndex];

            questionText.text = q.questionVariations[0]; // Always show the first variation

            feedbackText.text = ""; // ✅ Reset feedback text

            foreach (Button btn in optionButtons)
            {
                btn.interactable = true;
            }

            Answer[] shuffledAnswers = ShuffleAnswers(q.answers);

            for (int i = 0; i < optionButtons.Length; i++)
            {
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = shuffledAnswers[i].answerText;
                optionButtons[i].onClick.RemoveAllListeners();
                ResetButtonColor(optionButtons[i]);

                int index = i;
                optionButtons[i].onClick.AddListener(() => SelectAnswer(optionButtons[index], shuffledAnswers[index]));
            }
        }
        else
        {
            ShowFinalScore(); // ✅ Show final score when quiz ends
        }
    }

    private Answer[] ShuffleAnswers(Answer[] answers)
    {
        Answer[] shuffled = (Answer[])answers.Clone();
        for (int i = 0; i < shuffled.Length; i++)
        {
            int randomIndex = Random.Range(0, shuffled.Length);
            (shuffled[i], shuffled[randomIndex]) = (shuffled[randomIndex], shuffled[i]);
        }
        return shuffled;
    }

    private void SelectAnswer(Button selectedButton, Answer selectedAnswer)
    {
        if (!quizActive) return;

        foreach (Button btn in optionButtons)
        {
            btn.interactable = false;
        }

        if (selectedAnswer.isCorrect)
        {
            correctAnswerCount++; // ✅ Increase score
            feedbackText.text = "You got the correct answer!";
            StartCoroutine(ProceedToNextQuestion(selectedButton, correctAnswerColor, true));
        }
        else
        {
            feedbackText.text = "Incorrect. Moving to next question...";
            StartCoroutine(ProceedToNextQuestion(selectedButton, incorrectAnswerColor, false));
        }
    }

    private IEnumerator ProceedToNextQuestion(Button button, Color highlightColor, bool isCorrect)
    {
        button.image.color = highlightColor;
        yield return new WaitForSeconds(1.5f);
        button.image.color = defaultButtonColor;

        currentQuestionIndex++;
        LoadQuestion();
    }

    private void ShowFinalScore()
    {
        quizActive = false;
        scorePanel.SetActive(true); // ✅ Show final score panel

        // ✅ Ensure both text objects are visible
        scoreText.gameObject.SetActive(true);
        scoreFeedbackText.gameObject.SetActive(true);

        // ✅ Display the final score
        scoreText.text = $"Final Score: {correctAnswerCount}/{questions.Length}";

        // ✅ Determine the feedback message based on the user's score
        if (correctAnswerCount >= 4)
        {
            scoreFeedbackText.text = "Great job! You can proceed to the next stage!";
            StartCoroutine(ProceedToNextStage());
        }
        else
        {
            scoreFeedbackText.text = "You need to review this topic. \n Returning to main menu...";
            StartCoroutine(GoToMainMenu());
        }
    }

    private IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(3f); // ✅ Wait for 3 seconds
        SceneManager.LoadScene(mainMenuSceneIndex); // ✅ Load scene based on Inspector value
    }

    private IEnumerator ProceedToNextStage()
    {
        yield return new WaitForSeconds(2f);
        wellDonePanel.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void ResetButtonColor(Button button)
    {
        button.image.color = defaultButtonColor;
    }

    // ✅ Generate a unique key for quiz attempts per topic
    private string GetQuizAttemptKey()
    {
        return $"QuizAttemptCount_{currentTopic}";
    }

    private void IncrementQuizAttempt()
    {
        int attemptCount = PlayerPrefs.GetInt(GetQuizAttemptKey(), 0);
        attemptCount++;
        PlayerPrefs.SetInt(GetQuizAttemptKey(), attemptCount);
        PlayerPrefs.Save();
        UpdateQuizAttemptCounter();
    }

    private void UpdateQuizAttemptCounter()
    {
        int attemptCount = PlayerPrefs.GetInt(GetQuizAttemptKey(), 0);
        quizAttemptText.text = "Attempt: " + attemptCount;
    }
}
