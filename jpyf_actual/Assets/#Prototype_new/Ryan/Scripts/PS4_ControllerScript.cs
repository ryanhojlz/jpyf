using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_PS4
using UnityEngine.PS4;
#endif
public class PS4_ControllerScript : MonoBehaviour
{
    // This script is for the new prototype;

    public int playerId = -1;
    public int stickID;

    // Axis
    public float axisLeft_x = 0;
    public float axisLeft_y = 0;

    public float axisRight_x = 0;
    public float axisRight_y = 0;


    // PS4 OBJECTS
    public GameObject PS4_OBJECT = null;
    public Camera CAMERA = null;

    // Rotation Variables
    public Quaternion prevRot;
    Quaternion newRotation;

    // Move Direction
    Vector3 movedir = Vector3.zero;

    // Use this for initialization
    void Start ()
    {
        stickID = playerId + 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        AxisUpdate();
    }

    void AxisUpdate()
    {
#if UNITY_PS4

        axisLeft_x = Input.GetAxis("leftstick" + stickID + "horizontal");
        axisLeft_y = Input.GetAxis("leftstick" + stickID + "vertical");

        axisRight_x = Input.GetAxis("rightstick" + stickID + "horizontal");
        axisRight_y = Input.GetAxis("rightstick" + stickID + "vertical");

        // Clamping axis for fairness; sometimes some stick can be at 1 some be capped at 0.8
        axisLeft_x = Mathf.Clamp(axisLeft_x, -0.8f, 0.8f);
        axisLeft_y = Mathf.Clamp(axisLeft_y, -0.8f, 0.8f);

        axisRight_x = Mathf.Clamp(axisRight_x, -0.8f, 0.8f);
        axisRight_y = Mathf.Clamp(axisRight_y, -0.8f, 0.8f);

#endif
    }


    void Buttons()
    {

    }



    void ObjectMovement()
    {
        PS4_OBJECT.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        //CurrentUnit.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;

        movedir.Set(axisLeft_x * 8, 0, -axisLeft_y * 8);
        //movedir = new Vector3(x_input * move_speed, 0, -y_input * move_speed);
        movedir = CAMERA.transform.TransformDirection(movedir);
        if (movedir != Vector3.zero)
        {
            // Current experimental
            prevRot = PS4_OBJECT.transform.rotation;
            var testTransform = PS4_OBJECT.transform;
            testTransform.LookAt(movedir, Vector3.up);
            newRotation = Quaternion.LookRotation(movedir, Vector3.up);
            newRotation.x = 0;
            newRotation.z = 0;
            PS4_OBJECT.transform.rotation = Quaternion.Lerp(prevRot, newRotation, 0.25f);
        }
        //movedir.y = -1;
        PS4_OBJECT.transform.position += movedir * Time.deltaTime;
    }
}
