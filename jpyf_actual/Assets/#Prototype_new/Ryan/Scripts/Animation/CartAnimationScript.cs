using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartAnimationScript : MonoBehaviour
{
    // 1 highest 2 mid 3 lowest 0 destroyed
    int hpState = 3;
    bool b_Anim_IsWalking = false;
    bool b_Anim_IsIdle = false;
    Animator anim_component = null;

	// Use this for initialization
	void Start ()
    {
        anim_component = this.GetComponent<Animator>();
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
            hpState = 1;
        }
        else if (Stats_ResourceScript.Instance.m_CartHP > Stats_ResourceScript.Instance.m_CartHpCap * 0.4f)
        {
            hpState = 2;
        }
        else if (Stats_ResourceScript.Instance.m_CartHP > Stats_ResourceScript.Instance.m_CartHpCap * 0.25f)
        {
            hpState = 3;
        }
        else
        {
            hpState = 0;
        }

        anim_component.SetBool("Anim_IsIdle", b_Anim_IsIdle);
        anim_component.SetBool("Anim_IsWalking", b_Anim_IsWalking);

        // Updating animation
        if (b_Anim_IsIdle)
        {
            switch (hpState)
            {
                case 1:
                    anim_component.SetBool("Anim_IsIdle_High", true);
                    anim_component.SetBool("Anim_IsIdle_Mid", false);
                    anim_component.SetBool("Anim_IsIdle_Low", false);
                    break;
                case 2:
                    anim_component.SetBool("Anim_IsIdle_High", false);
                    anim_component.SetBool("Anim_IsIdle_Mid", true);
                    anim_component.SetBool("Anim_IsIdle_Low", false);
                    break;
                case 3:
                    anim_component.SetBool("Anim_IsIdle_High", false);
                    anim_component.SetBool("Anim_IsIdle_Mid", false);
                    anim_component.SetBool("Anim_IsIdle_Low", true);
                    break;
            }
            anim_component.SetBool("Anim_IsWalking_High", false);
            anim_component.SetBool("Anim_IsWalking_Mid", false);
            anim_component.SetBool("Anim_IsWalking_Low", false);

        }
        else if (b_Anim_IsWalking)
        {
            switch (hpState)
            {
                case 1:
                    anim_component.SetBool("Anim_IsWalking_High", true);
                    anim_component.SetBool("Anim_IsWalking_Mid", false);
                    anim_component.SetBool("Anim_IsWalking_Low", false);
                    break;
                case 2:
                    anim_component.SetBool("Anim_IsWalking_High", false);
                    anim_component.SetBool("Anim_IsWalking_Mid", true);
                    anim_component.SetBool("Anim_IsWalking_Low", false);
                    break;
                case 3:
                    anim_component.SetBool("Anim_IsWalking_High", false);
                    anim_component.SetBool("Anim_IsWalking_Mid", false);
                    anim_component.SetBool("Anim_IsWalking_Low", true);
                    break;
            }

            anim_component.SetBool("Anim_IsIdle_High", false);
            anim_component.SetBool("Anim_IsIdle_Mid", false);
            anim_component.SetBool("Anim_IsIdle_Low", false);
        }
    }


}
