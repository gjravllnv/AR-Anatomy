using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    private Touch touch;

    private Vector2 touchPosition;

    private Quaternion rotationY;

    private float rotateSpeedModifier = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler (
                    0f,
                    - touch.deltaPosition.x * rotateSpeedModifier,
                    0f);

                    transform.rotation = rotationY * transform.rotation;
            }
        }
    }
}
