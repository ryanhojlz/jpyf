using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_TakeDamage_State : IState
{
    Entity_Unit m_user;
    float animationDuration = 0f;//Animation duration;
    float previousTime = 0f;

    public Unit_TakeDamage_State(Entity_Unit _user)
    {
        m_user = _user;
        animationDuration = _user.GetTakeDamageTimer();
    }

    public void Enter()
    {
        if (m_user.GetComponent<AnimationScript>())
        {
            m_user.GetComponent<AnimationScript>().SetAnimTrigger(1);
        }

        //previousTime = Time.time;
    }

    public void Execute()
    {
        //if (previousTime == 0f)
        //{
        //    previousTime = Time.time;
        //}

        //if (previousTime + animationDuration < Time.time)
        //{
        //    m_user.ReturnPreviousState();
        //}


        if (m_user.GetComponent<AnimationScript>().AnimatorObj.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            m_user.ChangeState("chase");
        }
    }

    public void Exit()
    {

    }
}
