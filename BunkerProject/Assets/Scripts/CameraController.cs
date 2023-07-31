using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Variable Creation
    float MouseX;
    float MouseY;
    float xRot = 0f;
    float mouseSensitivity = 400;

    // Object References
    public GameObject player;

    void Update()
    {
        // Mouse Movement
        MouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        MouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRot -= MouseY; //Applies MouseY movement
        xRot = Mathf.Clamp(xRot, -90f, 90f); //Limits Cam Rotation

        //Cam and Player Rotation
        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        player.transform.Rotate(Vector3.up * MouseX);
    }
}
