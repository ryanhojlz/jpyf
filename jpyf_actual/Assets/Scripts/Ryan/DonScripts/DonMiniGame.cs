using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonMiniGame : MonoBehaviour
{
    // If true playing game if not not playing game
    public bool donStart = false;

    // Obj references
    public GameObject vr_player = null;
    public GameObject ps4_player = null;
    public GameObject ps4_Unit = null;

    // Time for interaction
    public float time2don = 0;
    public float time2donCap = 3;


    // Interaction meter / interactions
    public float don_progress = 0;
    public float don_progresscap = 20;

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
    }

    // Update player spirit
    void UpdatePlayerSpirit()
    {
        
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
            if (donStart)
                SetMiniGame(false);
        }
    }

    void UpdateGame()
    {
        // If interaction start
        if (donStart)
        {
            InteractionSuccess();
            InteractionToFail();
        }
        SetChildRender(donStart);

       
            
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
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(render);
        }
    }
}
