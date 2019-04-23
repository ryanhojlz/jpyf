﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DrumGameScript : MonoBehaviour
{
    
    // Drum stick Objects for Game
    public Transform m_LeftDrumstick = null;
    public Transform m_RightDrumStick = null;

    // UI Reference in game
    public Transform m_activityUI = null;

    // Beat Indicators
    public Transform m_beatIndicator;
    public Transform m_beatIndicator_left = null;
    public Transform m_beatIndicator_right = null;
    
    // Indicator Gameplay
    public Vector3 v_indicatorPos = Vector3.zero;
    bool b_translateRight = true;
    // Inside left indicator / right indicator booleans
    public bool b_insideLeft = false;
    public bool b_insideRight = false;



    // Boolean to check minigame
    public bool b_miniGamePlay = false;

    // Boolean way
    // Check for left stick hit, right stick hit, and hit in general
    public bool b_Left_StickHit = false;
    public bool b_Right_StickHit = false;
    public bool b_Hit_inGeneral = false;

    // Cart Reference
    public Push_CartScript m_pushCartRef = null;
    
    
    

    // Counter way
    //public int i_hitCounter = 0;




    // Game Activity Timer .. e.g refresh timer everytime an action is taken
    public float f_activity_timer = 0;
    
	// Use this for initialization
	void Start ()
    {
        // Initializing childs
        m_RightDrumStick = this.transform.parent.GetChild(1).GetChild(0);
        m_LeftDrumstick = this.transform.parent.GetChild(2).GetChild(0);
        // Getting references
        m_pushCartRef = GameObject.Find("PushingObjects").transform.GetChild(2).GetComponent<Push_CartScript>();
        // Pushing UI Activity
        m_activityUI = GameObject.Find("ActivityBar").transform;

        m_beatIndicator = GameObject.Find("BeatIndicator").transform;
        m_beatIndicator_left = GameObject.Find("BeatIndicatorLeft").transform;
        m_beatIndicator_right = GameObject.Find("BeatIndicatorRight").transform;

    }

    // Update is called once per frame
    void Update()
    {
        // Input shud come on top

        DebuggCodess();

        // If minigame in play
        if (b_miniGamePlay)
        {
            // Update ui
            m_activityUI.GetComponent<Image>().fillAmount = 1 * (f_activity_timer / 10.0f);
            v_indicatorPos = m_beatIndicator.transform.localPosition;

            // If it goes pass left right go back left
            if (b_translateRight)
            {
                if (m_beatIndicator.transform.position.x > m_beatIndicator_right.transform.position.x)
                {
                    b_translateRight = false;
                }
                v_indicatorPos.x += 0.5f * Time.deltaTime;
                m_beatIndicator.transform.localPosition = v_indicatorPos;
            }
            if (!b_translateRight) // if it goes pass left go back right
            {
                if (m_beatIndicator.transform.position.x < m_beatIndicator_left.transform.position.x)
                {
                    b_translateRight = true;
                }
                v_indicatorPos.x -= 0.5f * Time.deltaTime;
                m_beatIndicator.transform.localPosition = v_indicatorPos;
            }
        }
        else if (!b_miniGamePlay) // if not minigame in play
        {
            m_activityUI.parent.gameObject.SetActive(false);
            OnOffUI(false);
        }

        // Timer that determines whether the game is still going anot
        if (f_activity_timer > 0)
        {
            f_activity_timer -= 1 * Time.deltaTime;
        }
        if (f_activity_timer <= 0)
        {
            f_activity_timer = 0;
            b_miniGamePlay = false;
        }


        // If got hit in general
        if (b_Hit_inGeneral)
        {
            Debug.Log("Got hitto");
            b_miniGamePlay = true;
            f_activity_timer = 10;
            //m_activityUI.parent.gameObject.SetActive(true);
            //m_beatIndicator.gameObject.SetActive(false);
            //m_beatIndicator.gameObject.SetActive(false);
            //m_beatIndicator.gameObject.SetActive(false);
            OnOffUI(true);

            if (b_insideLeft)
            {
                //Debug.Log("SWITCH TO RIGHT");
                b_translateRight = true;
            }
            else if (b_insideRight)
            {
                //Debug.Log("SWITCH TO LEFT");
                b_translateRight = false;
            }
        }



        
        // iirc ontrigger updates first
        // So process logic above 
        // if im wrong then wrong lo
        b_Left_StickHit = false;
        b_Right_StickHit = false;
        b_Hit_inGeneral = false;
	}


    void DebuggCodess()
    {
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Running ");
            b_Hit_inGeneral = true;
        }
    }

    void OnOffUI(bool ui)
    {
        m_activityUI.parent.gameObject.SetActive(ui);
        m_beatIndicator.gameObject.SetActive(ui);
        m_beatIndicator_left.gameObject.SetActive(ui);
        m_beatIndicator_right.gameObject.SetActive(ui);
    }
    

    private void OnTriggerEnter(Collider other)
    {
        // If a stick enters
        //if (other.transform == m_LeftDrumstick || other.transform == m_RightDrumStick)
        //{
        //    // When i hit the counter goes up by 1
        //    ++i_hitCounter;
        //    Debug.Log("Got hitto");
        //    b_miniGamePlay = true;
        //    f_activity_timer = 10;
        //    m_activityUI.parent.gameObject.SetActive(true);
        //}

        // If right stick hit
        if (other.transform == m_LeftDrumstick)
        {
            b_Left_StickHit = true;
            b_Hit_inGeneral = true;
        }

        // if left stick hit
        if (other.transform == m_RightDrumStick)
        {
            b_Right_StickHit = false;
            b_Hit_inGeneral = true;
        }

        

    }


}