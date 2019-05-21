﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementPS4ControlsScript : MonoBehaviour
{
    Achievement_List handler = null;
	// Use this for initialization
	void Start ()
    {
        handler = GameObject.Find("Achi_Manager").GetComponent<Achievement_List>();    	
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Please refer to zijuns achievement manager
        if (PS4_ControllerScript.Instance.ReturnCrossPress())
        {
            
        }

        if (PS4_ControllerScript.Instance.ReturnDpadLeft())
        {
            handler.PanelMoveLeft();
        }
        else if (PS4_ControllerScript.Instance.ReturnDpadRight())
        {
            handler.PanelMoveRight();
        }
        else if (PS4_ControllerScript.Instance.ReturnDpadUp())
        {
            handler.PanelMoveUp();
        }
        else if (PS4_ControllerScript.Instance.ReturnDpadDown())
        {
            handler.PanelMoveDown();
        }
        else if (PS4_ControllerScript.Instance.ReturnLeft_AnalogLeft())
        {
            handler.PanelMoveLeft();
        }
        else if (PS4_ControllerScript.Instance.ReturnLeft_AnalogRight())
        {
            handler.PanelMoveRight();
        }
        else if (PS4_ControllerScript.Instance.ReturnLeft_AnalogUp())
        {
            handler.PanelMoveUp();
        }
        else if (PS4_ControllerScript.Instance.ReturnLeft_AnalogDown())
        {
            handler.PanelMoveDown();
        }

    }
}
