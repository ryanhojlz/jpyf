using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_ControlScript : MonoBehaviour
{
    // Current Obj reference
    public GameObject CurrentObj = null;
    // PS4 Controller Reference
    public PS4_ControllerScript Controller = null;
    // Camera Reference
    public Camera CameraObj = null;
    // Move direction
    public Vector3 movedir = Vector3.zero;

    // Quaternion for rotation value
    Quaternion prevRot;
    Quaternion newRotation;
    Transform testTransform;
    Vector3 tempRotation;

    // Object Interaction
    public bool pushCart = true;
    public bool isPushingCart = false;

    // For moving
    private Vector3 tempVelocity = Vector3.zero;
    
    // Use this for initialization
    void Start ()
    {
        Controller = GameObject.Find("PS4_ControllerHandler").GetComponent<PS4_ControllerScript>();
        CameraObj = GameObject.Find("ControllerCam").GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        ObjectMovement();
        Interaction();
	}

    void ObjectMovement()
    {
        
        CurrentObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        //CurrentUnit.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;

        movedir.Set(Controller.axisLeft_x * 8, 0, -Controller.axisLeft_y * 8);

        tempVelocity = CurrentObj.GetComponent<Rigidbody>().velocity;

        movedir = CameraObj.transform.TransformDirection(movedir);

        if (movedir != Vector3.zero)
        {
            tempVelocity.x = movedir.x;
            tempVelocity.z = movedir.z;

            // Current experimental
            prevRot = CurrentObj.transform.rotation;
            testTransform = CurrentObj.transform;
            testTransform.LookAt(movedir, Vector3.up);
            newRotation = Quaternion.LookRotation(movedir, Vector3.up);
            newRotation.x = 0;
            newRotation.z = 0;
            CurrentObj.transform.rotation = Quaternion.Lerp(prevRot, newRotation, 0.35f);
        }
        else
        {
            tempVelocity.x = 0;
            tempVelocity.z = 0;
        }
       
        //CurrentObj.GetComponent<Rigidbody>().AddForce(movedir);
        if (!isPushingCart)
        {
            CurrentObj.GetComponent<Rigidbody>().velocity = tempVelocity;
        }

        //CurrentObj.transform.position += movedir * Time.deltaTime;
    }

    void Interaction()
    {
        if (Controller.IsSquareDown())
            pushCart = true;
        else
            pushCart = false;
    }


    
}
