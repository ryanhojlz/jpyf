using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_PS4
using UnityEngine.PS4;
#endif
using System;
using UnityEngine.AI;


public class PS4_Controller : MonoBehaviour
{
    public int playerId = -1;
    public int stickID;
    private bool hasSetupGamepad = false;
    private Color lightbarColour;
#if UNITY_PS4
    private PS4Input.LoggedInUser loggedInUser;
    private PS4Input.ConnectionType connectionType;
#endif
    public GameObject _Player;
   // public GameObject Zijun_Alter;
    public GameObject cardReference;
    public GameObject ZiJun;

    private bool buttonbuffer_circle = false;
    private bool buttonbuffer_x = false;
    private bool buttonbuffer_square = false;

    public bool freecam = true;
    // Use this for initialization
    void Start ()
    {
        stickID = playerId + 1;
        ToggleGamePad(false);
        _Player = GameObject.Find("spec_cam");
    }

    // Update is called once per frame
    void Update ()
    {
#if UNITY_PS4
        if (PS4Input.PadIsConnected(playerId))
        {
            if (!hasSetupGamepad)
                ToggleGamePad(true);
            //Debug.Log("\n I am running \n");
            //Thumbsticks();
            InputButtons();
            //DPadButtons();
            TriggerShoulderButtons();
        }
        else if (hasSetupGamepad)
            ToggleGamePad(false);
#endif

    }

    void ToggleGamePad(bool active)
    {
        if (active)
        {
            // Set the lightbar colour to the start/default value
            //lightbarColour = GetPlayerColor(PS4Input.GetUsersDetails(playerId).color);

// Set 3D Text to whoever's using the pad
#if UNITY_PS4
            loggedInUser = PS4Input.RefreshUsersDetails(playerId);
#endif
            hasSetupGamepad = true;
        }
        else
        {
            // Set the lightbar to a default colour
            //lightbarColour = Color.gray;
            hasSetupGamepad = false;
        }
    }

    // Get a usable Color from an int
    Color GetPlayerColor(int colorId)
    {
        switch (colorId)
        {
            case 0:
                return Color.blue;
            case 1:
                return Color.red;
            case 2:
                return Color.green;
            case 3:
                return Color.magenta;
            default:
                return Color.black;
        }
    }

    //Thumbsticks();
    //InputButtons();
    //DPadButtons();
    //TriggerShoulderButtons();

    void Thumbsticks()
    {
        if (freecam)
        {
            // Camera Movement
            float cam_move_speed = 50.0f;
            float move_X = Input.GetAxis("leftstick" + stickID + "horizontal") * cam_move_speed;
            float move_Y = Input.GetAxis("leftstick" + stickID + "vertical") * cam_move_speed;


            Vector3 movedir = new Vector3(move_X, 0, -move_Y);
            //_Player.transform.TransformDirection(movedir.normalized);

            movedir = _Player.GetComponent<Camera>().transform.TransformDirection(movedir);
            // If 0 means no flying
            //movedir.y = 0;
            _Player.transform.position += movedir * Time.deltaTime;

            // Camera Look Controls
            float cam_speed = 100;
            //float lookX = Input.GetAxis("rightstick" + stickID + "vertical") * cam_speed * Time.deltaTime;
            //float lookY = Input.GetAxis("rightstick" + stickID + "horizontal") * cam_speed * Time.deltaTime;

            float lookX = Input.GetAxis("rightstick" + stickID + "vertical") * cam_speed * Time.deltaTime;
            float lookY = Input.GetAxis("rightstick" + stickID + "horizontal") * cam_speed * Time.deltaTime;


            Vector3 cam_rotation = _Player.transform.rotation.eulerAngles;
            cam_rotation.x += lookX;
            cam_rotation.y += lookY;
            cam_rotation.z = 0;

            _Player.transform.rotation = Quaternion.Euler(cam_rotation);

        }
    }

    void InputButtons()
    {
      
        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button0")) && !buttonbuffer_x)
        {
            buttonbuffer_x = true;
            //GameObject go = Instantiate(ZiJun) as GameObject;
            ////go.gameObject.GetComponent<Transform>().position = new Vector3(250, 7, -55);

            //Vector3 spawnPoint = new Vector3(250, 7, -55);

            //go.GetComponent<NavMeshAgent>().Warp(spawnPoint);

        }
        else if (!Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button0")) && buttonbuffer_x)
        {
            buttonbuffer_x = false;
        }

        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button1")) && !buttonbuffer_circle)
        {
            buttonbuffer_circle = true;
            for (int i = 0; i < 4; ++i)
            {
                GameObject go_ = Instantiate(cardReference) as GameObject;
                switch (i)
                {
                    case 0:
                        go_.transform.position = new Vector3(249.373f, 6, -70.095f);
                        break;
                    case 1:
                        go_.transform.position = new Vector3(249.511f, 6, -70.095f);
                        break;
                    case 2:
                        go_.transform.position = new Vector3(249.65f, 6, -70.095f);
                        break;
                    case 3:
                        go_.transform.position = new Vector3(249.789f, 6, -70.095f);
                        break;
                }
            }
        }
        else if (!Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button1")) && buttonbuffer_circle)
        {
            buttonbuffer_circle = false;
        }

        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button2")) && !buttonbuffer_square)
        {
            buttonbuffer_square = true;
            if (freecam)
                freecam = false;
            else if (!freecam)
                freecam = true;
        }
        else if (!Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button2")) && buttonbuffer_square)
        {
            buttonbuffer_square = false;
            
        }

    }

    void DPadButtons()
    {

    }

    void TriggerShoulderButtons()
    {
        //if (Input.GetAxis("joystick" + stickID + "_left_trigger") != 0)
        //{
        //    Vector3 _pos = _Player.transform.position;
        //    _pos.y -= 45 * Time.deltaTime;
        //    _Player.transform.position = _pos;
        //}
        //if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button4", true)))
        //{

        //    Vector3 _pos = _Player.transform.position;
        //    _pos.y += 45 * Time.deltaTime;
        //    _Player.transform.position = _pos;
        //}

        //if (Input.GetAxis("joystick" + stickID + "_right_trigger") != 0)
        //{
        //    Vector3 _pos = _Player.transform.position;
        //    _pos.y -= 30 * Time.deltaTime;
        //    _Player.transform.position = _pos;
        //}
        //if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button5", true)))
        //{

        //    Vector3 _pos = _Player.transform.position;
        //    _pos.y += 30 * Time.deltaTime;
        //    _Player.transform.position = _pos;
        //}

        //if (Input.GetAxis("joystick" + stickID + "_left_trigger") != 0)
        //{
        //    GameObject.Find("ViewManager").GetComponent<ViewScript>().SetDeviceView(true);
        //}
        //if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button4", true)))
        //{
        //    GameObject.Find("ViewManager").GetComponent<ViewScript>().SetDeviceView(false);
        //}
    }

}
