using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_PS4
using UnityEngine.PS4;
#endif 
using System;

public class ControllerPlayer : MonoBehaviour
{

    public int playerId = -1;
    public int stickID;

    // ButtonBuffer
    private bool buffer_o = false;
    private bool buffer_x = false;
    private bool buffer_square = false;
    private bool buffer_triangle = false;

    private bool buffer_R1 = false;
    private bool buffer_L1 = false;

    // Current
    public GameObject CurrentUnit = null;
    public GameObject SpiritUnit = null;
    // old
    public GameObject PlayerControllerObject = null;
    public GameObject Spirit = null;

    // Movement Stuff
    public float move_speed = 30;
    public float x_input, y_input;
    public Vector3 direction = Vector3.zero;

    // based on camera movement
    Vector3 movedir = Vector3.zero;

    // Camera stuff
    public GameObject camRef = null;
    public GameObject camPivot = null;
    public float x_inputR, y_inputR;
    public Vector3 camRot = Vector3.zero;

    // prev rotation
    public Quaternion prevRot;

    // Some bs    
    Vector3 lastpos;
 
    // Use this for initialization
    void Start()
    {
        prevRot = CurrentUnit.transform.rotation;
      
        stickID = playerId + 1;
        camRef = GameObject.Find("_FollowCam");
        camPivot = GameObject.Find("FollowCam");
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateSpirit();
        //if (PlayerControllerObject == null && Spirit)
        //{
        //    PlayerControllerObject = Spirit;
        //    Spirit.GetComponent<MeshRenderer>().enabled = true;
        //    Spirit.GetComponent<BoxCollider>().enabled = true;
        //    Spirit.GetComponent<Rigidbody>().isKinematic = false;
        //    Spirit = null;
        //}

        Controls();
       
    }

    void Controls()
    {
        ThumbSticks();
        Buttons();
        ShoulderButtons();
        DPad();
    }

    void ThumbSticks()
    {
        UpdateAxis();
        CameraMovement();
        NewMovement();
    }

    void Buttons()
    {
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    IfSpirit();
        //}
        //======================================================================================================
        // X Button
        //======================================================================================================
        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button0")) && !buffer_x)
        {
            //IfSpirit();
            if (CurrentUnit.GetComponent<NewPossesionScript>())
            {
                CurrentUnit.GetComponent<NewPossesionScript>().PossesUp();
            }
            else
            {
                SpiritUnit.GetComponent<NewPossesionScript>().PossesUp();
            }
            
            buffer_x = true;
        }
        else if (!Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button0")) && buffer_x)
        {
            buffer_x = false;
        }

        //======================================================================================================
        // O Button
        //======================================================================================================

        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button1")) && !buffer_o)
        {
            // Special Attack
            if (Spirit)
            {
                Debug.Log("attacked");
                PlayerControllerObject.GetComponent<Attack_Unit>().SpecialAttack();
            }
            buffer_o = true;
        }
        else if (!Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button1")) && buffer_o)
        {
            buffer_o = false;
        }

        //======================================================================================================
        // Square Button
        //======================================================================================================
        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button2")) && !buffer_square)
        {
            // Normal Attack
            
            buffer_square = true;
        }
        else if (!Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button2")) && buffer_square)
        {
            buffer_square = false;

        }

        //======================================================================================================
        // Triangle Button
        //======================================================================================================
        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button3")) && !buffer_triangle)
        {

            //if (Spirit)
            //{
            //    Debug.Log("attacked");
            //    PlayerControllerObject.GetComponent<BasicGameOBJ>().attackValue = 1;
            //    PlayerControllerObject.GetComponent<Attack_Unit>().PlayerAutoAttack();
            //}
            buffer_triangle = true;
        }
        else if (!Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button3")) && buffer_triangle)
        {

            buffer_triangle = false;
        }
    }

    void ShoulderButtons()
    {
        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button4")) && !buffer_R1)
        {
            Debug.Log("press right");

            //if (Spirit)
            //{
            //    Spirit.GetComponent<Possesor>().UpDownIndex(true);
            //}
            //else
            //{
            //    PlayerControllerObject.GetComponent<Possesor>().UpDownIndex(true);
            //}

            //Spirit.GetComponent<Possesor>().UpDownIndex(true);
            buffer_R1 = true;
        }
        else if (!Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button4")) && buffer_R1)
        {
            buffer_R1 = false;
        }


        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button5")) && !buffer_L1)
        {
            Debug.Log("press left");

            //if (Spirit)
            //{
            //    Spirit.GetComponent<Possesor>().UpDownIndex(false);
            //}
            //else
            //{
            //    PlayerControllerObject.GetComponent<Possesor>().UpDownIndex(false);
            //}

            //Spirit.GetComponent<Possesor>().UpDownIndex(false);
            buffer_L1 = true;
        }
        else if (!Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button5")) && buffer_L1)
        {
            buffer_L1 = false;
        }

    }

    void DPad()
    {

    }


    void IfSpirit()
    {
        // Possesion
        if (!Spirit)
        {
            //Debug.Log("\npreessseed\n");
            if (PlayerControllerObject.GetComponent<Possesor>().startPossesing)
            {
                PlayerControllerObject.GetComponent<Possesor>().possesionProgress += 1;
            }
            else if (!PlayerControllerObject.GetComponent<Possesor>().startPossesing)
            {
                if (PlayerControllerObject.GetComponent<Possesor>().canPosses)
                {
                    PlayerControllerObject.GetComponent<Possesor>().startPossesing = true;
                    PlayerControllerObject.GetComponent<Possesor>().Text_Instantiate();
                }
            }
        }
        else if (Spirit)
        {
            if (Spirit.GetComponent<Possesor>().startPossesing)
            {
                Spirit.GetComponent<Possesor>().possesionProgress += 1;
            }
            else if (!Spirit.GetComponent<Possesor>().startPossesing)
            {
                if (Spirit.GetComponent<Possesor>().canPosses)
                {
                    Spirit.GetComponent<Possesor>().startPossesing = true;
                    Spirit.GetComponent<Possesor>().Text_Instantiate();
                }
            }
        }


    }

    void UpdateSpirit()
    {
        //lastpos = this.PlayerControllerObject.transform.position;
        // Normal Check
        if (Spirit)
        {
            if (PlayerControllerObject.GetComponent<BasicGameOBJ>().healthValue <= 0)
            {
                //Spirit.transform.position = lastpos;
                //Spirit.SetActive(true);

                Spirit.GetComponent<MeshRenderer>().enabled = true;
                Spirit.GetComponent<BoxCollider>().enabled = true;
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().useGravity = true;

                PlayerControllerObject = Spirit;
                Spirit = null;
            }
        }
        // Fail Safe Check
        if (PlayerControllerObject == null && Spirit)
        {
            //Spirit.transform.position = lastpos;
            //Spirit.SetActive(true);

            Spirit.GetComponent<MeshRenderer>().enabled = true;
            Spirit.GetComponent<BoxCollider>().enabled = true;
            PlayerControllerObject = Spirit;
            Spirit = null;
        }
    }


    void OldMovement()
    {
        if (!Spirit)
        {
            move_speed = 4;
        }
        else
        {
            move_speed = 5;
        }
        x_input = Input.GetAxis("leftstick" + stickID + "horizontal");
        y_input = Input.GetAxis("leftstick" + stickID + "vertical");
        x_input = Mathf.Clamp(x_input, -0.8f, 0.8f);
        y_input = Mathf.Clamp(y_input, -0.8f, 0.8f);

        GameObject.Find("DebugText").GetComponent<Text>().text = "Xinput " + x_input;
        GameObject.Find("DebugText3").GetComponent<Text>().text = "Yinput " + y_input;

        float gravity = CurrentUnit.GetComponent<Rigidbody>().velocity.y;
        direction = new Vector3(x_input, 0, -y_input);
        if (direction == Vector3.zero)
        {
            direction.y = gravity;
            direction.x = 0;
            direction.z = 0;
            //this.PlayerControllerObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //this.PlayerControllerObject.GetComponent<Rigidbody>().velocity = direction;
        }
        else
        {
            direction.y = gravity;
            direction.x *= move_speed;
            direction.z *= move_speed;
            this.CurrentUnit.GetComponent<Rigidbody>().velocity = direction;
        }

        CurrentUnit.transform.position += direction * Time.deltaTime;/** move_speed*/
    }


    void UpdateAxis()
    {

#if UNITY_PS4
        // Left stick
        x_input = Input.GetAxis("leftstick" + stickID + "horizontal");
        y_input = Input.GetAxis("leftstick" + stickID + "vertical");
        x_input = Mathf.Clamp(x_input, -0.8f, 0.8f);
        y_input = Mathf.Clamp(y_input, -0.8f, 0.8f);


        // Right stick
        x_inputR = Input.GetAxis("rightstick" + stickID + "horizontal");
        y_inputR = Input.GetAxis("rightstick" + stickID + "vertical");
        x_inputR = Mathf.Clamp(x_inputR, -0.8f, 0.8f);
        y_inputR = Mathf.Clamp(y_inputR, -0.8f, 0.8f);


        GameObject.Find("DebugText").GetComponent<Text>().text = "Xinput " + x_input;
        GameObject.Find("DebugText2").GetComponent<Text>().text = "Yinput " + y_input;

        GameObject.Find("DebugText3").GetComponent<Text>().text = "Xinput 2 " + x_inputR;
        GameObject.Find("DebugText4").GetComponent<Text>().text = "Yinput 2 " + y_inputR;
#endif

#if UNITY_EDITOR_WIN
        // Debug for PC
        float input = 0.8f;
        if (Input.GetKey(KeyCode.LeftArrow))
            x_inputR = input;
        else if (Input.GetKey(KeyCode.RightArrow))
            x_inputR = -input;
        else
            x_inputR = 0;

        if (Input.GetKey(KeyCode.UpArrow))
            y_inputR = input;
        else if (Input.GetKey(KeyCode.DownArrow))
            y_inputR = -input;
        else
            y_inputR = 0;

        if (Input.GetKey(KeyCode.W))
            y_input = -input;
        else if (Input.GetKey(KeyCode.S))
            y_input = input;
        else
            y_input = 0;

        if (Input.GetKey(KeyCode.A))
            x_input = input;
        else if (Input.GetKey(KeyCode.D))
            x_input = -input;
        else
            x_input = 0;

#endif


    }


    void NewMovement()
    {
        CurrentUnit.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        //CurrentUnit.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        move_speed = 4;

#if UNITY_PS4
        movedir = new Vector3(x_input * move_speed, 0, -y_input * move_speed);
        movedir = camRef.GetComponent<Camera>().transform.TransformDirection(movedir);
        if (movedir != Vector3.zero)
        {
            CurrentUnit.transform.LookAt(movedir, Vector3.up);
            Quaternion rot = Quaternion.LookRotation(movedir, Vector3.up);
            rot.x = 0;
            rot.z = 0;
            CurrentUnit.transform.rotation = rot;
            prevRot = CurrentUnit.transform.rotation;
            // Old rotation
            //prevRot = CurrentUnit.transform.rotation;
        }

#endif
        //movedir.y = -1;
        CurrentUnit.transform.position += movedir * Time.deltaTime;
        
    }


    void CameraMovement()
    {
        float sens = 5;
        camRot = camPivot.transform.rotation.eulerAngles;
        camRot.y += x_inputR * sens;
        camRot.x += y_inputR * sens;
        camRot.z = 0; // no ninja

        //// Clamping
        //if (camRot.x > 85)
        //    camRot.x = 85;
        //else if (camRot.x < -85)
        //    camRot.x = -85;

        camPivot.transform.rotation = Quaternion.Euler(camRot);
    }
}
