using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_PS4
using UnityEngine.PS4;
#endif 
using System;

public class MainMenuController : MonoBehaviour
{
    public MainMenuButton menuManager = null;
    public int playerID = -1;
    public int stickID = 0;

    // Buffer inputs needed for axis
    public bool buffer_axis_horizontal_L = false;
    public bool buffer_axis_horizontal_R = false;

    public bool buffer_axis_vertical_U = false;
    public bool buffer_axis_vertical_D = false;


	// Use this for initialization
	void Start ()
    {
        menuManager = transform.Find("ButtonManager").GetComponent<MainMenuButton>();
        stickID = playerID + 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Buttons();
        DPAD();
	}

    void Buttons()
    {
        // Confirm
        if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button0")))
        {
            
            menuManager.EnterSelected();
            menuManager.TitlescreenDisplay = false;
            
            Debug.Log("This is working and so are you");
        }

        // Exit
        if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + stickID + "Button1")))
        {
            Debug.Log("This is working and so are you 2222");
        }
    }

    void DPAD()
    {
        // Move right
        if (Input.GetAxis("dpad" + stickID + "_horizontal") > 0 && !buffer_axis_horizontal_R)
        {
            menuManager.MoveRight();
            buffer_axis_horizontal_R = true;
        }
        else if (Input.GetAxis("dpad" + stickID + "_horizontal") == 0 && buffer_axis_horizontal_R)
        {
            // Stick reset 
            buffer_axis_horizontal_R = false;
        }

        // Move Left
        if (Input.GetAxis("dpad" + stickID + "_horizontal") < 0 && !buffer_axis_horizontal_L)
        {
            menuManager.MoveLeft();
            buffer_axis_horizontal_L = true;
        }
        else if (Input.GetAxis("dpad" + stickID + "_horizontal") == 0 && buffer_axis_horizontal_L)
        {
            // Stick reset 
            buffer_axis_horizontal_L = false;
        }

        
        // Move Up
        if (Input.GetAxis("dpad" + stickID + "_vertical") > 0 && !buffer_axis_vertical_U)
        {
            menuManager.MoveUp();
            buffer_axis_vertical_U = true;
        }
        else if (Input.GetAxis("dpad" + stickID + "_vertical") == 0 && buffer_axis_vertical_U)
        {
            // Stick reset 
            buffer_axis_vertical_U = false;
        }

        // Move Down
        if (Input.GetAxis("dpad" + stickID + "_vertical") < 0 && !buffer_axis_vertical_D)
        {
            menuManager.MoveDown();
            buffer_axis_vertical_D = true;
        }
        else if (Input.GetAxis("dpad" + stickID + "_vertical") == 0 && buffer_axis_vertical_D)
        {
            // Stick reset 
            buffer_axis_vertical_D = false;
        }
    }










}
