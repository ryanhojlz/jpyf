using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DonMiniGame : MonoBehaviour
{
    // If true playing game if not not playing game
    public bool donStart = false;

    // Obj references
    public GameObject vr_player = null;
    public GameObject ps4_player = null;
    public GameObject ps4_Unit = null;
    public GameObject donUI = null;

    // Time for interaction
    public float time2don = 0;
    public float time2donCap = 8;


    // Interaction meter / interactions
    public float don_progress = 0;
    public float don_progresscap = 30;

    // Don UI graphics
    public GameObject controller_ui;

    private void Awake()
    {

    }

    // Use this for initialization
    void Start ()
    {
        vr_player = GameObject.Find("Camera_player");
        ps4_player = GameObject.Find("Player_object");
        ps4_Unit = ps4_player.GetComponent<ControllerPlayer>().CurrentUnit;
        time2don = time2donCap;
        donUI = transform.Find("DonUI").gameObject;

    }
	
	// Update is called once per frame
	void Update ()
    {
        SetMiniGameFromPlayer();
        UpdateGame();
    }


    // For readability and public for outside use
    public void SetMiniGame(bool boolean)
    {
        donStart = boolean;
        // Render the objects 
        SetChildRender(donStart);
    }



    // Set mini game via playr reference
    void SetMiniGameFromPlayer()
    {
        if (ps4_Unit.GetComponent<NewPossesionScript>().isPossesing)
        {
            if (!donStart)
                SetMiniGame(true);
        }
        else if (!ps4_Unit.GetComponent<NewPossesionScript>().isPossesing)
        {
            SetMiniGame(false);
        }
    }

    void UpdateGame()
    {
        // If interaction start
        if (donStart)
        {
            // Condition to win
            InteractionSuccess();
            // Condition to lose
            InteractionToFail();
        }

        // Update don ui
        UpdateDonUI();
        
            
    }

    // Public call function to don the object
    public void DonInteraction()
    {
        if (donStart)
            don_progress += 1;
    }

    // Sucess call
    void InteractionSuccess()
    {
        if (don_progress > don_progresscap)
        {
            time2don = time2donCap;
            don_progress = 0;
            SetMiniGame(false);
        }
    }


    // Non sucess call
    void InteractionToFail()
    {
        // Time for interaction
        time2don -= 1 * Time.deltaTime;
        if (time2don <= 0)
        {
            // time for interaction cap // reset value
            time2don = time2donCap;
            don_progress = 0;
            SetMiniGame(false);
        }
    }


    void SetChildRender(bool render)
    {
        // If render false
        if (render == false)
        {
            if (transform.Find("DonHandle").transform.gameObject.activeSelf == true)
            {
                transform.Find("DonHandle").GetComponent<DonHandle>().ResetPos();
                transform.Find("DonHandle_1").GetComponent<DonHandle>().ResetPos();
                donUI.GetComponent<Image>().fillAmount = 0;
            }

        }
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(render);
        }
    }

    void UpdateDonUI()
    {
        donUI.GetComponent<Image>().fillAmount = 1 * (don_progress / don_progresscap);
    }

    
}
