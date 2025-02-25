using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class APIManager : MonoBehaviour
{
    private static APIManager _instance;
    public static APIManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to call the API
    public void GetTopicsToReview(System.Action<string> onSuccess, System.Action<string> onError)
    {
        StartCoroutine(SendRequest(onSuccess, onError));
    }

    private IEnumerator SendRequest(System.Action<string> onSuccess, System.Action<string> onError)
    {
        // Define the API endpoint
        string url = "https://thesis-backend-trd6.onrender.com/predict";

        // Create a POST request
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            // Set the request headers (if needed)
            request.SetRequestHeader("Content-Type", "application/json");

            // Set the request body (if needed)
            // Example: If the API requires a JSON payload
            // string jsonData = "{\"key\":\"value\"}";
            // byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            // request.uploadHandler = new UploadHandlerRaw(bodyRaw);

            // Set the download handler to receive the response
            request.downloadHandler = new DownloadHandlerBuffer();

            // Send the request
            yield return request.SendWebRequest();

            // Check for errors
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                onError?.Invoke(request.error);
            }
            else
            {
                // Process the response
                string responseText = request.downloadHandler.text;
                onSuccess?.Invoke(responseText);
            }
        }
    }
}