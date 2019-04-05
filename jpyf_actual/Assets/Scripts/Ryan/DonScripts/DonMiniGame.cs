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
    public GameObject controller_ui = null;
    // Don UI graphics
    public GameObject donTeam_ui = null;

    private void Awake()
    {

    }

    // Use this for initialization
    void Start ()
    {
        vr_player = GameObject.Find("Camera_player");
        ps4_player = GameObject.Find("Player_object");
        // Ensures to get the Spirit Unit
        // Should not need to change after start
        ps4_Unit = ps4_player.GetComponent<ControllerPlayer>().CurrentUnit;
        // Value Assigning
        time2don = time2donCap;

        donUI = GameObject.Find("DonUI").gameObject;
        //donTeam_ui = transform.Find("")
    }
	
	// Update is called once per frame
	void Update ()
    {
        SetMiniGameFromPlayer();
        UpdateGame();
    }


    // For readability or public for outside use
    public void SetMiniGame(bool boolean)
    {
        donStart = boolean;
    }



    // Set mini game via playr reference
    void SetMiniGameFromPlayer()
    {
        // When controller players is possesing
        if (ps4_Unit.GetComponent<NewPossesionScript>().isPossesing)
        {
            // If not in game start
            if (!donStart)
                SetMiniGame(true);
        }
        // When controller players is not possesing  
        if (!ps4_Unit.GetComponent<NewPossesionScript>().isPossesing)
        {
            SetMiniGame(false);
        }
    }

    void UpdateGame()
    {
        /// Update don ui
        UpdateDonUI();
        /// Winning & Losing condition
        Interaction();
        /// Rendererrrrrr
        SetChildRender(donStart);
        
    }

    // Interaction Win Lose Condition
    void Interaction()
    {
        // if interaction not in play return;
        if (!donStart)
            return;
        // Timer for losing 
        //time2don -= 1 * Time.deltaTime;


        if (Input.GetKey(KeyCode.U))
        {
            don_progress++;
        }


        // Win Condition
        if (don_progress >= don_progresscap)
        {
            don_progress = 0;
            // Fail Safe checking
            if (ps4_Unit.GetComponent<NewPossesionScript>())
            {

                ps4_Unit.GetComponent<NewPossesionScript>().EffectEnhances += 0.5f;
            }
            Debug.Log("Win");
        }

        // Losing condition
        //if (time2don <= 0)
        //{
        //    time2don = time2donCap;
        //    don_progress = 0;
        //    donStart = false;
        //}
        
    }

    // Public call function to don the object
    public void DonInteraction(int don)
    {
        if (donStart)
        {
            don_progress += don;
        }
    }

    

    // Set Render when on off
    void SetChildRender(bool render)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(render);
        }        
    }

    // UI update
    void UpdateDonUI()
    {
        donUI.GetComponent<Image>().fillAmount = 1 * (don_progress / don_progresscap);
    }

    
}
