using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    Transform Unit;
    public Animator AnimatorObj;

    bool Anim_IsIdle = true;
    bool Anim_IsDead = false;
    //bool Anim_IsDamaged = false;
    bool Anim_IsWalking = false;
    //bool Anim_IsAttacking = false;
    //bool Anim_IsJustSpawned = false;
    // Summary of animation states
    /// <summary>
    /// Anim_State_Spawn
    /// Anim_State_Idle
    /// Anim_State_Walking
    /// Anim_State_Attack
    /// Anim_State_Damaged
    /// Anim_State_Dead
    /// </summary>

    // Use this for initialization
    void Start()
    {
        // Assigning
        Unit = this.transform.parent;
        AnimatorObj = GetComponent<Animator>();

        // Setting Booleans to animation parameters in animator
        AnimatorObj.SetBool("Anim_IsIdle", Anim_IsIdle);
        //AnimatorObj.SetBool("Anim_IsDead", Anim_IsDead);
        AnimatorObj.SetBool("Anim_IsWalking", Anim_IsWalking);

    }

    // Update is called once per frame
    void Update()
    {

        DebugFunc();
        // Set bool update Everyframe
        // Setting Booleans to animation parameters in animator
        //AnimatorObj.SetBool("Anim_IsIdle", Anim_IsIdle);
        //AnimatorObj.SetBool("Anim_IsDead", Anim_IsDead);
        //AnimatorObj.SetBool("Anim_IsWalking", Anim_IsWalking);

        
    }

    public void DebugFunc()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetAnim(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetAnim(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            AnimatorObj.SetTrigger("Trigger_Anim_Attack");
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            AnimatorObj.SetTrigger("Trigger_Anim_IsDamaged");
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            AnimatorObj.SetTrigger("Trigger_Anim_Dead");
        }


    }

    /// <summary>
    /// Go inside the function to see what u return
    /// </summary>
    public bool ReturnAnim(int animID)
    {
        switch (animID)
        {
            case 0:
                return Anim_IsIdle;
            case 1:
                return Anim_IsWalking;
            default:
                break;
        }
        return false;
    }


    // 0 for Idle
    // 1 for Walking
    // 2 for Dead

    public void SetAnim(int id)
    {
        switch (id)
        {
            case 0:
                Anim_IsIdle = true;
                Anim_IsDead = false;
                Anim_IsWalking = false;
                break;
            case 1:
                Anim_IsWalking = true;
                Anim_IsIdle = false;
                Anim_IsDead = false;
                break;
        }
        AnimatorObj.SetBool("Anim_IsIdle", Anim_IsIdle);
        //AnimatorObj.SetBool("Anim_IsDead", Anim_IsDead);
        AnimatorObj.SetBool("Anim_IsWalking", Anim_IsWalking);
    }

    // Check whether can move after attacking
    public bool Move_FromAttack()
    {
        if (AnimatorObj.GetCurrentAnimatorStateInfo(0).IsName("Anim_State_Attack"))
        {
            if (AnimatorObj.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    // Check whether bullet can come out 
    public bool Sync_Projectile()
    {
        // 0.5;
        if (AnimatorObj.GetCurrentAnimatorStateInfo(0).IsName("Anim_State_Attack"))
        {
            if (AnimatorObj.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.01f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    // Check can move after getting hit
    public bool Move_FromDamage()
    {
        if (AnimatorObj.GetCurrentAnimatorStateInfo(0).IsName("Anim_State_Damaged"))
        {
            if (AnimatorObj.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }



    // 0 is to attack
    // 1 is to get damaged
    // 2 is to get dead

    public void SetAnimTrigger(int id)
    {
        switch (id)
        {
            case 0:
                AnimatorObj.SetTrigger("Trigger_Anim_Attack");
                break;
            case 1:
                AnimatorObj.SetTrigger("Trigger_Anim_IsDamaged");
                break;
            case 2:
                AnimatorObj.SetTrigger("Trigger_Anim_Dead");
                break;
        }
    }

}
