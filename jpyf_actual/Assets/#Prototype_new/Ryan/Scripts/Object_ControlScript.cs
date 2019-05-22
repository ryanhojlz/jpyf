using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Object_ControlScript : MonoBehaviour
{
    // last min do ps
    public static Object_ControlScript Instance = null;


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
    public float m_dashSpeed = 5;


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

    public bool jump = false;
    public bool isGrounded = true;

    // Dash attack
    public bool dashAtk = false;
    float dashTimer = 0.15f;


    // Object that grabs the player away
    public Transform Gropper = null;

    // For moving
    private Vector3 tempVelocity = Vector3.zero;
    private Vector3 cancelVelocity = Vector3.zero;

    public Rigidbody objectRb = null;

    private void Awake()
    {
        isGrounded = true;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

    }

    // Use this for initialization
    void Start ()
    {
        Controller = GameObject.Find("PS4_ControllerHandler").GetComponent<PS4_ControllerScript>();
        CameraObj = GameObject.Find("ControllerCam").GetComponent<Camera>();
        handler = GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>();
        m_playerSpeed = 8;


        objectRb = CurrentObj.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Moving boolean is " + isPushingCart);
        //Debug.Log("Check cart " + checkCart);
        Interaction();
        ObjectMovement();

    }

    //private void FixedUpdate()
    //{
       
    //}

    void ObjectMovement()
    {
        if (GameEventsPrototypeScript.Instance.b_bigExplain)
            return;

        
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

        //CurrentObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;

        //CurrentObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
        //CurrentObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;

        objectRb.constraints = RigidbodyConstraints.FreezeRotationZ;
        objectRb.constraints = RigidbodyConstraints.FreezeRotationX;



        //CurrentUnit.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;

        // Set move direction
        movedir.Set(Controller.axisLeft_x * (m_playerSpeed + m_dashSpeed - m_playerSpeedDebuff), 
            0
            , -Controller.axisLeft_y * (m_playerSpeed + m_dashSpeed - m_playerSpeedDebuff));

        // Modify velocity
        //tempVelocity = CurrentObj.GetComponent<Rigidbody>().velocity;
        tempVelocity = objectRb.velocity;

        // Ensure movement based on camera view
        movedir = CameraObj.transform.TransformDirection(movedir);

        // Rotation code
        if (movedir != Vector3.zero)
        {
            tempVelocity.x = movedir.x;
            tempVelocity.z = movedir.z;

            // Current experimental
            prevRot = CurrentObj.transform.rotation;
            testTransform = CurrentObj.transform;
            // Creates a fake angle to look towards
            testTransform.LookAt(movedir, Vector3.up);
            newRotation = Quaternion.LookRotation(movedir, Vector3.up);
            newRotation.x = 0;
            newRotation.z = 0;
            // Smoothing of rotation
            CurrentObj.transform.rotation = Quaternion.Lerp(prevRot, newRotation, 0.35f);
        }
        else
        {
            tempVelocity.x = 0;
            tempVelocity.z = 0;
        }

        
        
        //CurrentObj.GetComponent<Rigidbody>().AddForce(movedir);

        if (handler.playerDead)
        {
            isPushingCart = false;
            return;
        }

        if (Gropper)
        {
            //handler.Player2_TakeDmg(1);
            return;
        }
        // Movement
        if (isPushingCart)
        {            
            //CurrentObj.GetComponent<Rigidbody>().isKinematic = true;
            //objectRb.isKinematic = true;
        }
        else if (!isPushingCart)
        {
            //CurrentObj.GetComponent<Rigidbody>().isKinematic = false;
            objectRb.isKinematic = false;
            //CurrentObj.GetComponent<Rigidbody>().AddForce(tempVelocity * 40);
            //tempVelocity = CurrentObj.GetComponent<Rigidbody>().velocity;
            //tempVelocity.x = Mathf.Clamp(tempVelocity.x, -4, 4);
            //tempVelocity.z = Mathf.Clamp(tempVelocity.z, -4, 4);
            //CurrentObj.GetComponent<Rigidbody>().velocity = tempVelocity;

            // Snappy way
            if (!dashAtk)
            {
                tempVelocity.x *= 0.8f;
                tempVelocity.z *= 0.8f;
                //CurrentObj.GetComponent<Rigidbody>().velocity = tempVelocity;
                objectRb.velocity = tempVelocity;
            }
        }

        // Dashing
        if (isGrounded)
        {
            if (jump)
            {
                //CurrentObj.GetComponent<Rigidbody>().AddForce(Vector3.up * 2500);
                objectRb.AddForce(Vector3.up * 30000);
                
                isGrounded = false;
            }
            if (dashAtk)
            {
                dashTimer -= 1 * Time.deltaTime;
                //CurrentObj.GetComponent<Rigidbody>().velocity = CurrentObj.transform.forward * 35;
                objectRb.velocity = CurrentObj.transform.forward * 35;
                
                if (jump)
                {
                    dashTimer = 0.0f;
                }
                if (dashTimer <= 0)
                {
                    dashAtk = false;
                    //cancelVelocity = CurrentObj.GetComponent<Rigidbody>().velocity;
                    cancelVelocity = objectRb.velocity;


                    cancelVelocity.z = 0;
                    cancelVelocity.x = 0;
                    //CurrentObj.GetComponent<Rigidbody>().velocity = cancelVelocity;
                    objectRb.velocity = cancelVelocity;
                    dashTimer = 0.15f;
                }
            }
            else
            {
                dashTimer = 0.15f;
            }
        }
        //if (Gropper)
        //{
        //    isPushingCart = false;
        //}

        //CurrentObj.transform.position += movedir * Time.deltaTime;
    }

    void Interaction()
    {
        if (handler.playerDead)
            return;
        // Dangerous Stuffs
        throw_item = false;
        pickup = false;
        checkCart = false;
        checkCanGatherItem = false;
        //dash = false;
        jump = false;
        

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

        //// Throw item
        //if (Controller.ReturnR1Press())
        //{
        //    throw_item = true;
        //}

        // Gather Items
        if (Controller.ReturnTrianglePress())
        {
            checkCanGatherItem = true;
        }

        // Kun Hua wants to jump so i give him
        if (Controller.ReturnCrossPress())
        {
            jump = true;
        }
        if (Controller.ReturnR1Press())
        {
            dashAtk = true;
        }


        
#endif

#if UNITY_EDITOR_WIN
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            throw_item = true;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            checkCart = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            checkCanGatherItem = true;
        }

        if (Input.GetKey(KeyCode.E) && Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("Gid");
            dashAtk = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("eeeene");
            jump = true;
        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Debug.Log("eon");
            throw_item = true;
            pickup = true;
        }

#endif

        // IF tengu grab player disable all actions
        if (Gropper)
        {
            dashAtk = false;
            jump = false;
            checkCanGatherItem = false;
            pickup = false;
            throw_item = false;
            pushCart = false;
        }

        //Debug.Log("Pickup   + " + pickup);
        //GameObject.Find("Text").GetComponent<Text>().text = "Pickup " + pickup; 
    }

    // Tengu 
    public void SetGropper(Transform obj)
    {
        Gropper = obj;
    }

    // Set debuff old
    public void SetDebuff(float debuffspeed , float debuffDuration)
    {
        m_playerSpeedDebuff = debuffspeed;
        m_debuffDuration = debuffDuration;
    }

    // Debuff duration old
    public float GetDebuffDuration()
    {
        return m_debuffDuration;
    }

    // Returner
    public float GetDebuffSpeed()
    {
        return m_playerSpeed;
    }




}
