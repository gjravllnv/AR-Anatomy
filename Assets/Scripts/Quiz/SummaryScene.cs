using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SummaryScene : MonoBehaviour
{
    public TMP_Text summaryText;

    // This should be populated with the list of incorrect answers from the quiz
    private List<QuestionData> incorrectAnswers = new List<QuestionData>(); 

    void Start()
    {
        int preTestScore = PlayerPrefs.GetInt("PreTestScore", 0);
        int postTestScore = PlayerPrefs.GetInt("PostTestScore", 0);
        int improvement = postTestScore - preTestScore;

        // Display summary scores
        summaryText.text = $"Pre-Test Score: {preTestScore}\n" +
                           $"Post-Test Score: {postTestScore}\n";

        if (improvement > 0)
        {
            summaryText.text += $"Great job! You improved by {improvement} points.\n";
        }
        else if (improvement == 0)
        {
            summaryText.text += "Your score stayed the same. Keep practicing!\n";
        }
        else
        {
            summaryText.text += "Don't worry; you'll do better next time!\n";
        }

        // Add feedback for topics to focus on
        ProvideTopicFeedback();
    }

    private void ProvideTopicFeedback()
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
            summaryText.text += "\nTopics to focus on:\n";
            foreach (var topic in topicsToFocusOn)
            {
                summaryText.text += $"- {topic}\n";
            }
            summaryText.text += "\nYou should study more.";
        }
        else
        {
            summaryText.text += "\nGreat job! No mistakes!";
        }
    }
}
