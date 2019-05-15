using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartAnimationScript : MonoBehaviour
{
    // 3 highest 2 mid 1 lowest 0 destroyed
    int hpState = 3;
    bool b_Anim_IsWalking = false;
    bool b_Anim_IsIdle = false;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Update walking or idle boolean
        if (PayloadMovementScript.Instance.playerInsideCart)
        {
            // Move no idlerino
            b_Anim_IsIdle = false;
            b_Anim_IsWalking = true;
        }
        else
        { 
            b_Anim_IsIdle = true;
            b_Anim_IsWalking = false;
        }

        // Update model base on health
        if (Stats_ResourceScript.Instance.m_CartHP > Stats_ResourceScript.Instance.m_CartHpCap * 0.7f)
        {
            hpState = 3;
        }
        else if (Stats_ResourceScript.Instance.m_CartHP > Stats_ResourceScript.Instance.m_CartHpCap * 0.4f)
        {
            hpState = 2;
        }
        else if (Stats_ResourceScript.Instance.m_CartHP > Stats_ResourceScript.Instance.m_CartHpCap * 0.25f)
        {
            hpState = 1;
        }
        else
        {
            hpState = 0;
        }


        // Updating animation
        if (b_Anim_IsIdle)
        {
            switch (hpState)
            {
                // Later add brb eat
            }
        }
        else if (b_Anim_IsWalking)
        {
            switch (hpState)
            {

            }
        }
    }


}
