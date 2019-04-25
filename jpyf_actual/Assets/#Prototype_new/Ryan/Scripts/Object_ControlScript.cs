using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Object_ControlScript : MonoBehaviour
{
    // Current Obj reference
    public GameObject CurrentObj = null;

    // PS4 Controller Reference
    public PS4_ControllerScript Controller = null;

    //
    public Stats_ResourceScript handler = null;
    
    // Camera Reference
    public Camera CameraObj = null;
    
    // Movement direction
    public Vector3 movedir = Vector3.zero;

    // Quaternion for rotation value
    Quaternion prevRot;
    Quaternion newRotation;
    Transform testTransform;
    Vector3 tempRotation;

    //Player movespeed
    public float m_playerSpeed = 0;
    public float m_playerSpeedDebuff = 0;
    public float m_debuffDuration = 0;

    // Object Interaction
    // Cart Interaction
    public bool pushCart = true;
    public bool checkCart = false;

    public bool isPushingCart = false;

    // Item Interaction
    public bool pickup = false;
    public bool throw_item = false;

    public bool checkCanGatherItem = false;
    public bool isGathering = false;

    // Object that grabs the player away
    public Transform Gropper = null;

    // For moving
    private Vector3 tempVelocity = Vector3.zero;
    
    // Use this for initialization
    void Start ()
    {
        Controller = GameObject.Find("PS4_ControllerHandler").GetComponent<PS4_ControllerScript>();
        CameraObj = GameObject.Find("ControllerCam").GetComponent<Camera>();
        handler = GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>();
        m_playerSpeed = 8;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log("Moving boolean is " + isPushingCart);
        //Debug.Log("Check cart " + checkCart);

        ObjectMovement();
        Interaction();
	}

    void ObjectMovement()
    {

        // Update Debuff speed
        if (m_debuffDuration > 0)
        {
            m_debuffDuration -= 1 * Time.deltaTime;
            if (m_debuffDuration <= 0)
            {
                m_debuffDuration = 0;
            }
        }
        else
        {
            m_playerSpeedDebuff = 0;
        }

        CurrentObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        //CurrentUnit.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;

        // Set move direction
        movedir.Set(Controller.axisLeft_x * (m_playerSpeed - m_playerSpeedDebuff), 0, -Controller.axisLeft_y * (m_playerSpeed - m_playerSpeedDebuff));

        // Modify velocity
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

        if (Gropper)
        {
            handler.Player2_TakeDmg(1);
            return;
        }
        // Movement
        if (isPushingCart)
        {
            CurrentObj.GetComponent<Rigidbody>().isKinematic = true;
        }
        else if (!isPushingCart)
        {
            CurrentObj.GetComponent<Rigidbody>().isKinematic = false;
            CurrentObj.GetComponent<Rigidbody>().velocity = tempVelocity;
        }
        //if (Gropper)
        //{
        //    isPushingCart = false;
        //}

        //CurrentObj.transform.position += movedir * Time.deltaTime;
    }

    void Interaction()
    {
        throw_item = false;
        pickup = false;
        checkCart = false;
        checkCanGatherItem = false;

#if UNITY_PS4
        // Hold square to push cart
        if (Controller.IsSquareDown())
            pushCart = true;
        else
            pushCart = false;

        if (Controller.ReturnSquarePress())
        {
            checkCart = true;
        }
        // Pick up

        if (Controller.ReturnCirclePress())
        {
            throw_item = true;
            pickup = true;
        }

        // Throw item
        if (Controller.ReturnR1Press())
        {
            throw_item = true;
        }

        if (Controller.ReturnTrianglePress())
        {
            checkCanGatherItem = true;
        }


#endif

#if UNITY_EDITOR_WIN
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pickup = true;
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            throw_item = true;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            checkCart = true;
        }

#endif


        //Debug.Log("Pickup   + " + pickup);
        GameObject.Find("Text").GetComponent<Text>().text = "Pickup " + pickup; 
    }


    public void SetGropper(Transform obj)
    {
        Gropper = obj;
    }

    public void SetDebuff(float debuffspeed , float debuffDuration)
    {
        m_playerSpeedDebuff = debuffspeed;
        m_debuffDuration = debuffDuration;
    }

    public float GetDebuffDuration()
    {
        return m_debuffDuration;
    }

    public float GetDebuffSpeed()
    {
        return m_playerSpeed;
    }




}
