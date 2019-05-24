using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    GameEventsPrototypeScript handler = null;
    public GameObject pauseMenu = null;
	// Use this for initialization
	void Start ()
    {
        handler = GameEventsPrototypeScript.Instance;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (pauseMenu)
        {
            if (handler.ReturnPause())
            {
                pauseMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);
            }

#if UNITY_PS4
            if (PS4_ControllerScript.Instance)
            {



                if (PS4_ControllerScript.Instance.ReturnOptions())
                {
                    GameEventsPrototypeScript.Instance.PauseFunc();
                }

                if (handler.ReturnPause())
                {
                    if (PS4_ControllerScript.Instance.ReturnCrossPress())
                    {
                        GameEventsPrototypeScript.Instance.PauseFunc();
                        Time.timeScale = 1;
                    }
                    else if (PS4_ControllerScript.Instance.ReturnCirclePress())
                    {
                        Time.timeScale = 1;
                        GameEventsPrototypeScript.Instance.PauseFunc();
                        GameObject.Find("Sceneload").GetComponent<SceneLoad>().GoBackToMainMenu();
                    }
                }

            }

#endif
        }
	}
}
