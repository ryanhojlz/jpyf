using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_PS4
using UnityEngine.PS4;
using UnityEngine.XR;
using System;
#endif
using UnityEngine.UI;
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
    //public GameObject PS4_OBJECT = null;
    public Camera CAMERA = null;

    // Rotation Variables
    public Quaternion prevRot;
    Quaternion newRotation;

    // Move Direction
    Vector3 movedir = Vector3.zero;

    // Bool
    public bool SquareDown = false;

    public static PS4_ControllerScript Instance = null;

    // Use this for initialization
    void Start ()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(this.gameObject);

        stickID = playerId + 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        AxisUpdate();
        Buttons();
    }

    void AxisUpdate()
    {
        // If running on ps4  
#if UNITY_PS4

        // Left stick
        axisLeft_x = Input.GetAxis("leftstick" + stickID + "horizontal");
        axisLeft_y = Input.GetAxis("leftstick" + stickID + "vertical");

        // Right Stick
        axisRight_x = Input.GetAxis("rightstick" + stickID + "horizontal");
        axisRight_y = Input.GetAxis("rightstick" + stickID + "vertical");

        // Clamping axis for fairness; sometimes some stick can be at 1 some be capped at 0.8
        axisLeft_x = Mathf.Clamp(axisLeft_x, -0.8f, 0.8f);
        axisLeft_y = Mathf.Clamp(axisLeft_y, -0.8f, 0.8f);

        axisRight_x = Mathf.Clamp(axisRight_x, -0.8f, 0.8f);
        axisRight_y = Mathf.Clamp(axisRight_y, -0.8f, 0.8f);
#endif

        // If running on PC
#if UNITY_EDITOR_WIN
        // Debug for PC
        float input = 0.8f;

        // Right Stick
        if (Input.GetKey(KeyCode.LeftArrow))
            axisRight_x = -input;
        else if (Input.GetKey(KeyCode.RightArrow))
            axisRight_x = input;
        else
            axisRight_x = 0;

        // Right Stick
        if (Input.GetKey(KeyCode.UpArrow))
            axisRight_y = -input;
        else if (Input.GetKey(KeyCode.DownArrow))
            axisRight_y = input;
        else
            axisRight_y = 0;

        // Left Stick
        if (Input.GetKey(KeyCode.W))
            axisLeft_y = -input;
        else if (Input.GetKey(KeyCode.S))
            axisLeft_y = input;
        else
            axisLeft_y = 0;

        // Left Stick
        if (Input.GetKey(KeyCode.A))
            axisLeft_x = -input;
        else if (Input.GetKey(KeyCode.D))
            axisLeft_x = input;
        else
            axisLeft_x = 0;
#endif
    }


    void Buttons()
    {
#if UNITY_PS4
        // If Square Button is held
        if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button2")))
        {
            SquareDown = true;
        }
        else
        {
            SquareDown = false;
        }

        // Debug For turning on and off VR view
        if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button6")))
        {
            if (UnityEngine.XR.XRSettings.showDeviceView)
                UnityEngine.XR.XRSettings.showDeviceView = false;
            else if (!UnityEngine.XR.XRSettings.showDeviceView)
                UnityEngine.XR.XRSettings.showDeviceView = true;

            Stats_ResourceScript.Instance.ResetStats();
        }



#endif

#if UNITY_EDITOR_WIN
        if (Input.GetKey(KeyCode.P))
        {
            SquareDown = true;
        }
        else
        {
            SquareDown = false;
        }
#endif
    }


#if UNITY_PS4
    public bool IsSquareDown()
    {
        return SquareDown;
    }

    public bool ReturnCrossPress()
    {
        return (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button0")));
    }

    public bool ReturnCirclePress()
    {
        return (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button1")));
    }

    public bool ReturnSquarePress()
    {
        return (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button2")));
    }

    public bool ReturnTrianglePress()
    {
        return (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button3")));
    }

 
    public bool ReturnR1Press()
    {
        return (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button5")));
    }

    public bool ReturnR1Down()
    {
        return (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button5")));
    }

    public bool ReturnL1Press()
    {
        return (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button4")));
    }

    
#endif


}
