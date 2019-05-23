using System.Collections;
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
#if UNITY_PS4
        // Please refer to zijuns achievement manager
        if (PS4_ControllerScript.Instance.ReturnCirclePress())
        {
            GameObject.Find("Sceneload").GetComponent<SceneLoad>().GoBackToMainMenu();   
        }

        if (PS4_ControllerScript.Instance.ReturnDpadLeft())
        {
            handler.PanelMoveRight();
        }
        if (PS4_ControllerScript.Instance.ReturnDpadRight())
        {
            handler.PanelMoveLeft();
        }
        //else if (PS4_ControllerScript.Instance.ReturnDpadUp())
        //{
        //    handler.PanelMoveUp();
        //}
        //else if (PS4_ControllerScript.Instance.ReturnDpadDown())
        //{
        //    handler.PanelMoveDown();
        //}
        //else if (PS4_ControllerScript.Instance.ReturnLeft_AnalogLeft())
        //{
        //    handler.PanelMoveLeft();
        //}
        //else if (PS4_ControllerScript.Instance.ReturnLeft_AnalogRight())
        //{
        //    handler.PanelMoveRight();
        //}
        //else if (PS4_ControllerScript.Instance.ReturnLeft_AnalogUp())
        //{
        //    handler.PanelMoveUp();
        //}
        //else if (PS4_ControllerScript.Instance.ReturnLeft_AnalogDown())
        //{
        //    handler.PanelMoveDown();
        //}



#endif
    }
}
