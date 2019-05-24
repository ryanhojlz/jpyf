using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAnimScript : MonoBehaviour
{
    // The model of the object
    Transform Unit = null;
    // The actual object
    Transform PS4_Object = null;
    public Animator AnimatorObj;

    public static PlayerAnimScript instance_player = null;

    // Toggling on and of stuff
    // Carrying Object
    //bool IsCarrying = false;
    //bool IsWalking = false;
    // Idle Animation
    bool Anim_IsIdle = false;
    bool Anim_IsIdleCarry = false;

    // Walking Animation
    bool Anim_IsWalking = false;
    bool Anim_IsWalkingCarry = false;

    public bool Anim_IsPushing = false;

    // If got caught
    bool Anim_IsCaught = false;

    // Bool is dead
    bool Anim_IsDeadOnce = false;

    private void Awake()
    {
        if (!instance_player)
            instance_player = this;
        else
            Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        // Setting animation parameters
        AnimatorObj = GetComponent<Animator>();
        PS4_Object = Object_ControlScript.Instance.CurrentObj.transform;
        Unit = this.transform;
	}
	
	
    // Update is called once per frame
	void Update ()
    {
        //DebugFunc();
        UpdateMovementAnim();
        UpdateGettingCaught();
        UpdateDeathAnim();

        if (Object_ControlScript.Instance.jump)
        {
            AnimatorObj.SetTrigger("Anim_IsJumping");
        }
        else if (Object_ControlScript.Instance.dashAtk)
        {
            AnimatorObj.SetTrigger("Anim_IsDashing");
        }

        // Always setting this
        AnimatorObj.SetBool("Anim_IsIdle", Anim_IsIdle);
        AnimatorObj.SetBool("Anim_IsIdleCarry", Anim_IsIdleCarry);
        AnimatorObj.SetBool("Anim_IsWalking", Anim_IsWalking);
        AnimatorObj.SetBool("Anim_IsWalkingCarry", Anim_IsWalkingCarry);
        AnimatorObj.SetBool("Anim_IsNotDead", Anim_IsDeadOnce);
        AnimatorObj.SetBool("Anim_IsCaught", Anim_IsCaught);
        AnimatorObj.SetBool("Anim_IsPushing", Anim_IsPushing);

    }


    bool ReturnIsCarryingObj()
    {
        bool gotObject = false;

        if (PickupHandlerScript.Instance.currentObject)
            gotObject = true;
        else
            gotObject = false;

        return gotObject;
    }


    void UpdateMovementAnim()
    {
        //GameObject.Find("Text5").GetComponent<Text>().text = "" + PS4_Object.GetComponent<Rigidbody>().velocity;

        if (Object_ControlScript.Instance.movedir != Vector3.zero)
        {
            // Boolean 
            Anim_IsIdle = false;
            Anim_IsIdleCarry = false;

            if (ReturnIsCarryingObj())
            {
                Anim_IsWalkingCarry = true;
                Anim_IsWalking = false;
            }
            else
            {
                Anim_IsWalkingCarry = false;
                Anim_IsWalking = true;
            }
        }
        else
        {
            // Boolean
            Anim_IsWalking = false;
            Anim_IsWalkingCarry = false;

            // No velocity = not walking
            if (ReturnIsCarryingObj())
            {
                Anim_IsIdleCarry = true;
                Anim_IsIdle = false;
            }
            else
            {
                Anim_IsIdleCarry = false;
                Anim_IsIdle = true;
            }

        }



        // Check Walking       
        //if (PS4_Object.GetComponent<Rigidbody>().velocity != Vector3.zero)
        //{
        //    // Boolean 
        //    Anim_IsIdle = false;
        //    Anim_IsIdleCarry = false;

        //    if (ReturnIsCarryingObj())
        //    {
        //        Anim_IsWalkingCarry = true;
        //        Anim_IsWalking = false;
        //    }
        //    else
        //    {
        //        Anim_IsWalkingCarry = false;
        //        Anim_IsWalking = true;
        //    }
        //}
        //else if (PS4_Object.GetComponent<Rigidbody>().velocity == Vector3.zero)
        //{
        //    // Boolean
        //    Anim_IsWalking = false;
        //    Anim_IsWalkingCarry = false;

        //    // No velocity = not walking
        //    if (ReturnIsCarryingObj())
        //    {
        //        Anim_IsIdleCarry = true;
        //        Anim_IsIdle = false;
        //    }
        //    else
        //    {
        //        Anim_IsIdleCarry = false;
        //        Anim_IsIdle = true;
        //    }

        //}



    }

    void UpdateGettingCaught()
    {
        if (Object_ControlScript.Instance.Gropper)
        {
            Anim_IsCaught = true;
        }
        else
        {
            Anim_IsCaught = false;
        }
    }

    void UpdateDeathAnim()
    {
        if (Stats_ResourceScript.Instance.m_P2_hp <= 0)
        {
            if (!Anim_IsDeadOnce)
            {
                AnimatorObj.SetTrigger("Anim_IsDead");
                Anim_IsDeadOnce = true;
            }

        }
        else if (Stats_ResourceScript.Instance.m_P2_hp >= 100)
        {
            Anim_IsDeadOnce = false;
        }
    }

    // Debug Func
    void DebugFunc()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Anim_IsWalking = false;
            Anim_IsWalkingCarry = false;
            Anim_IsIdleCarry = false;
            Anim_IsIdle = true;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Anim_IsWalking = false;
            Anim_IsWalkingCarry = false;
            Anim_IsIdleCarry = true;
            Anim_IsIdle = false;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Anim_IsIdle = false;
            Anim_IsIdleCarry = false;
            Anim_IsWalkingCarry = false;
            Anim_IsWalking = true;
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            Anim_IsIdle = false;
            Anim_IsIdleCarry = false;
            Anim_IsWalkingCarry = true;
            Anim_IsWalking = false;
        }
    }

    public void SetTriggerThrow()
    {
        AnimatorObj.SetTrigger("Anim_ThrowObject");

    }
}
