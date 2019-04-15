using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    // Third Person Camera

    // Controller is parent
    public PS4_ControllerScript Controller = null;
    // Camera
    public Transform m_Camera = null;

    // Camera rotation vector3
    public Vector3 camRot = Vector3.zero;

    // Camera Sensitivity;
    public float sens = 2;


    // Use this for initialization
    void Start ()
    {
        Controller = GameObject.Find("PS4_ControllerHandler").GetComponent<PS4_ControllerScript>();
        m_Camera = transform.GetChild(0);
        // Camera offset

        var newPos = m_Camera.transform.position;
        newPos.x += 0.55f;
        newPos.y += 1;
        newPos.z -= 4;

        m_Camera.transform.position = newPos;

    }

    // Update is called once per frame
    void Update ()
    {
        CameraMovement();
	}

    void CameraMovement()
    {
        camRot = this.transform.rotation.eulerAngles;
        camRot.y += Controller.axisRight_x * sens;
        camRot.x += Controller.axisRight_y * sens;
        camRot.z = 0; // Prevent Cartwheels
        this.transform.rotation = Quaternion.Euler(camRot);
    }
}
