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
        }
	}
}
