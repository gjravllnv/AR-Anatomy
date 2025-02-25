using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class QuestionData
{
    public string questionText; // The question text
    public string topic; // The topic associated with the question
    public AnswerData[] answers; // List of options

    /// <summary>
    /// Shuffle a list of questions using the Fisher-Yates shuffle algorithm.
    /// </summary>
    /// <param name="questions">The list of questions to shuffle.</param>
    public static void ShuffleQuestions(List<QuestionData> questions)
    {
        Debug.Log("Shuffling questions...");
        for (int i = questions.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            // Swap the questions
            QuestionData temp = questions[i];
            questions[i] = questions[randomIndex];
            questions[randomIndex] = temp;
        }

        Debug.Log("Questions shuffled:");
        foreach (var question in questions)
        {
            Debug.Log(question.questionText);
        }
    }
}
