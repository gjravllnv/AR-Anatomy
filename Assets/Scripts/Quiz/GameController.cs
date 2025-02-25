using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text questionDisplayText;
    public TMP_Text scoreDisplayText;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject questionDisplay;
    public GameObject roundEndDisplay;
    public TMP_Text feedbackText;

    [Header("Timer")]
    public Slider timerSlider;
    public float gameTime;

    private RoundData currentRoundData;
    private QuestionData[] questionPool;

    private bool isRoundActive;
    private float timeRemaining;
    private int questionIndex;
    private int playerScore;

    private string roundType;

    private List<QuestionData> incorrectAnswers = new List<QuestionData>();

    void Start()
    {
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;

        roundType = PlayerPrefs.GetString("CurrentRound", "Pre-Test");
        LoadRoundData();

        List<QuestionData> questionList = new List<QuestionData>(currentRoundData.questions);
        QuestionData.ShuffleQuestions(questionList);
        questionPool = questionList.ToArray();

        timeRemaining = currentRoundData.timeLimitInSeconds;

        playerScore = 0;
        questionIndex = 0;

        ShowQuestion();
        isRoundActive = true;

        UpdateScoreDisplay();
    }

    private void LoadRoundData()
    {
        DataController dataController = FindObjectOfType<DataController>();
        Debug.Log($"Loading round data for: {roundType}");
        
        if (roundType == "Pre-Test")
        {
            currentRoundData = dataController.preTestData;
        }
        else if (roundType == "Post-Test")
        {
            currentRoundData = dataController.postTestData;
        }

        if (currentRoundData == null || currentRoundData.questions.Length == 0)
        {
            Debug.LogError($"No data found for {roundType}. Check DataController or RoundData assignment.");
        }
    }

    private void ShowQuestion()
    {
        if (questionIndex < questionPool.Length)
        {
            QuestionData questionData = questionPool[questionIndex];
            questionDisplayText.text = questionData.questionText;

            foreach (Transform child in answerButtonParent)
            {
                Destroy(child.gameObject);
            }

            foreach (AnswerData answer in questionData.answers)
            {
                GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
                answerButtonGameObject.transform.SetParent(answerButtonParent);

                AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
                answerButton.Setup(answer);
            }
        }
        else
        {
            EndRound();
        }
    }

    public void AnswerButtonClicked(bool isCorrect)
    {
        if (isCorrect)
        {
            playerScore += currentRoundData.pointsAddedForCorrectAnswer;
        }
        else
        {
            // Add the current question to the incorrectAnswers list
            incorrectAnswers.Add(questionPool[questionIndex]);
        }

        questionIndex++;

        if (questionIndex < questionPool.Length)
        {
            ShowQuestion();
        }
        else
        {
            EndRound();
        }

        UpdateScoreDisplay();
    }


    public void EndRound()
    {
        isRoundActive = false;
        questionDisplay.SetActive(false);
        roundEndDisplay.SetActive(true);

        if (roundType == "Pre-Test")
        {
            PlayerPrefs.SetInt("PreTestScore", playerScore);
            PlayerPrefs.SetString("CurrentRound", "Post-Test");
            PlayerPrefs.Save();

            feedbackText.text = $"Pre-Test Score: {playerScore} / {questionPool.Length * currentRoundData.pointsAddedForCorrectAnswer}";

            ProvideTopicFeedback();
        }
        else if (roundType == "Post-Test")
        {
            PlayerPrefs.SetInt("PostTestScore", playerScore);
            PlayerPrefs.Save();

            CompareScores();

            feedbackText.text = $"Post-Test Score: {playerScore} / {questionPool.Length * currentRoundData.pointsAddedForCorrectAnswer}";
                                

            ProvideTopicFeedback();
        }
    }

   private void ProvideTopicFeedback() // Recommendation after Pre and Post Test
    {
        HashSet<string> topicsToFocusOn = new HashSet<string>();

        // Collect unique topics from incorrect answers
        foreach (var question in incorrectAnswers)
        {
            topicsToFocusOn.Add(question.topic);
        }

        // Display feedback on topics to focus on
        if (topicsToFocusOn.Count > 0)
        {
            feedbackText.text += "\n\nTopics to Review:\n";
            foreach (var topic in topicsToFocusOn)
            {
                feedbackText.text += $"• {topic},";
           
            }
            feedbackText.text += "\n\nYou should study more.";
        }
        else
        {
            feedbackText.text += "\n\nGreat job! No mistakes!";
        }
    }

    // public void RestartGame()
    // {
    //     Debug.Log("Restarting the quiz...");

    //     PlayerPrefs.SetString("CurrentRound", "Pre-Test");
    //     PlayerPrefs.DeleteKey("PreTestScore");
    //     PlayerPrefs.DeleteKey("PostTestScore");
    //     PlayerPrefs.Save();

    //     SceneManager.LoadScene("StartScene"); // Replace with your start scene name
    // }

    private void CompareScores()
    {
        int preTestScore = PlayerPrefs.GetInt("PreTestScore", 0);
        int postTestScore = PlayerPrefs.GetInt("PostTestScore", 0);
        int improvement = postTestScore - preTestScore;

        if (improvement > 0)
        {
            feedbackText.text += $"\nGreat job! You improved by {improvement} points.";
        }
        else if (improvement == 0)
        {
            feedbackText.text += $"\nYour score stayed the same. Keep practicing!";
        }
        else
        {
            feedbackText.text += $"\nDon't worry; you'll do better next time!";
        }
    }

    private void UpdateScoreDisplay()
    {
        scoreDisplayText.text = $"Score: {playerScore}";
    }

    void Update()
    {
        if (isRoundActive)
        {
            timeRemaining -= Time.deltaTime;
            timerSlider.value = timeRemaining;

            if (timeRemaining <= 0)
            {
                EndRound();
            }
        }
    }
}
