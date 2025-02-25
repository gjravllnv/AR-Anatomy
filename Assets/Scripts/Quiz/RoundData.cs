using UnityEngine;
using System.Collections;
using TMPro;

[System.Serializable]
public class RoundData 
{
   
    public string roundName; // Pre-Test or Post-Test 
    public QuestionData[] questions;
    public int pointsAddedForCorrectAnswer;
    public float timeLimitInSeconds;
}
