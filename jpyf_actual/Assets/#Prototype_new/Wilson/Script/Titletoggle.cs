using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titletoggle : MonoBehaviour {


    GameObject buttonmanager = null;

    MainMenuButton buttonManager_script = null;

    bool isActive = true;

    public static Titletoggle Instance = null;

    // Use this for initialization
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);

        }
    }
    void Start ()
    {
        
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update ()
    {

        //Debug.Log("Its here 2");
        if (!buttonmanager)//If there is not button manager, then search for it
        {
            buttonmanager = GameObject.Find("ButtonManager");

            if (!buttonmanager)//if button manager not found stop the update
                return;
        }

        //Debug.Log("Its here 3");

        if (!buttonmanager.GetComponent<MainMenuButton>())//If it does not have the script return
        {
            Debug.Log("No have mainmenubutton lah dey");
            return;
        }
        else if (!buttonManager_script)
        {
            buttonManager_script = buttonmanager.GetComponent<MainMenuButton>();
        }

        //Debug.Log("Its here 4");

        if (!buttonManager_script)
            return;

        //Debug.Log("Its here 5");

        if (buttonManager_script.TitlescreenDisplay && !isActive)
        {
            //Debug.Log("Yea its here");
            buttonManager_script.TitlescreenDisplay = false;
        }

        if (!buttonManager_script.TitlescreenDisplay)
        {
            isActive = false;
        }
    }

    public void DestroyThisScript()
    {
        Destroy(this.gameObject);
    }
}
