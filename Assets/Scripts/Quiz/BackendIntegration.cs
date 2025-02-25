// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Networking;
// using Newtonsoft.Json;

// public class BackendIntegration : MonoBehaviour
// {
//     // URL of your Flask backend
//     private string backendUrl = "https://thesis-backend-trd6.onrender.com/predict";

//     // Method to send a JSON payload
//     public void SendQuizResults(List<Answer> answers)
//     {
//         // Create the payload object
//         var payload = new Payload
//         {
//             answers = answers
//         };

//         // Serialize the payload to JSON
//         string jsonPayload = JsonConvert.SerializeObject(payload);

//         // Start the coroutine to send the request
//         StartCoroutine(PostRequest(backendUrl, jsonPayload));
//     }

//     private IEnumerator PostRequest(string url, string jsonPayload)
//     {
//         // Create UnityWebRequest with JSON payload
//         var request = new UnityWebRequest(url, "POST");
//         byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
//         request.uploadHandler = new UploadHandlerRaw(bodyRaw);
//         request.downloadHandler = new DownloadHandlerBuffer();
//         request.SetRequestHeader("Content-Type", "application/json");

//         // Send the request
//         yield return request.SendWebRequest();

//         // Handle response
//         if (request.result == UnityWebRequest.Result.Success)
//         {
//             Debug.Log("Response: " + request.downloadHandler.text);

//             // Deserialize the response JSON
//             var response = JsonConvert.DeserializeObject<Response>(request.downloadHandler.text);

//             // Process the response (you can customize this)
//             Debug.Log("Model Accuracy: " + response.ModelAccuracy);
//             Debug.Log("Topics to Review: " + JsonConvert.SerializeObject(response.TopicsToReview));
//         }
//         else
//         {
//             Debug.LogError("Error: " + request.error);
//         }
//     }

//     // Classes to represent the JSON structure
//     [System.Serializable]
//     public class Answer
//     {
//         public string QuestionID;
//         public string Answer;
//     }

//     [System.Serializable]
//     public class Payload
//     {
//         public List<Answer> answers;
//     }

//     [System.Serializable]
//     public class Response
//     {
//         public string ModelAccuracy;
//         public List<TopicToReview> TopicsToReview;
//         public List<WrongAnswer> WrongAnswers;
//     }

//     [System.Serializable]
//     public class TopicToReview
//     {
//         public string Topic;
//         public float AverageAccuracy;
//     }

//     [System.Serializable]
//     public class WrongAnswer
//     {
//         public string QuestionID;
//         public string Question;
//         public string Topic;
//         public string Answer;
//         public string CorrectAnswer;
//     }
// }
