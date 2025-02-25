using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class webscript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject webCameraPlane;


    // Use this for initialization
    void Start()
    {
        //Optimized Camera For Mobile Platform
        if (Application.isMobilePlatform)
        {
            GameObject cameraParent = new GameObject("camParent");
            cameraParent.transform.position = this.transform.position;
            this.transform.parent = cameraParent.transform;
            // cameraParent.transform.Rotate(Vector3.right, 90);
        }

        // Input.gyro.enabled = false;

        WebCamTexture webCameraTexture = new WebCamTexture(1280, 720);
        webCameraPlane.GetComponent<MeshRenderer>().material.mainTexture = webCameraTexture;
        webCameraTexture.Play();

        // Adjust the scale of the plane to match the camera's aspect ratio
        float aspectRatio = (float)webCameraTexture.width / (float)webCameraTexture.height;
        webCameraPlane.transform.localScale = new Vector3(webCameraPlane.transform.localScale.x, webCameraPlane.transform.localScale.x / aspectRatio, webCameraPlane.transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        // //Control Camera Using Gyroscope
        // Quaternion cameraRotation = new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.y, -Input.gyro.attitude.z, -Input.gyro.attitude.w);
        // this.transform.localRotation = cameraRotation;

    }
}
