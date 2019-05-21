using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS4_MenuStuff : MonoBehaviour
{
    public MainMenuButton menu = null;
    bool input = false;

	// Use this for initialization
	void Start ()
    {
	    	
	}
	
	// Update is called once per frame
	void Update ()
    {
#if UNITY_PS4
        if (PS4_ControllerScript.Instance)
        {
            if (!input)
            {
                if (PS4_ControllerScript.Instance.ReturnDpadRight())
                {
                    menu.MoveRight();
                    input = true;
                }
                else if (PS4_ControllerScript.Instance.ReturnDpadLeft())
                {
                    menu.MoveLeft();
                    input = true;

                }
                else if (PS4_ControllerScript.Instance.ReturnDpadUp())
                {
                    menu.MoveUp();
                    input = true;

                }
                else if (PS4_ControllerScript.Instance.ReturnDpadDown())
                {
                    menu.MoveDown();
                    input = true;
                }
            }
            else
            {
                if (PS4_ControllerScript.Instance.ReturnInputZero())
                {
                    input = false;
                }
            }


            if (PS4_ControllerScript.Instance.ReturnCrossPress())
            {

                menu.EnterSelected();

                if (!menu.TitlescreenDisplay)
                    menu.TitlescreenDisplay = true;
                else if (menu.TitlescreenDisplay)
                    menu.TitlescreenDisplay = false;

            }

        }
#endif
    }

}
