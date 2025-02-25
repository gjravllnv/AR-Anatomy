using UnityEngine;
using TMPro;

public class MessageHandler : MonoBehaviour
{
    public TextMeshProUGUI messageText; 

    // Called when the target is found
    public void OnTargetFound()
    {
        if (messageText != null)
        {
            messageText.text = " "; //If the object place on a flat surface, message will appear
            messageText.gameObject.SetActive(true); 
        }
    }

    // Called when the target is lost
    public void OnTargetLost()
    {
        if (messageText != null)
        {
            messageText.text = "Point your camera at a flat surface";
            messageText.gameObject.SetActive(true); // If the object not found message will appear
        }
    }

}
