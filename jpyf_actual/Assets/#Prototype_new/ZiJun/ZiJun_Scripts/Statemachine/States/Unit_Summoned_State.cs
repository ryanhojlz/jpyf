using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Summoned_State : IState
{
    Entity_Unit m_user;
    float animationDuration = 0f;//Animation duration;
    float previousTime = 0f;

    public Unit_Summoned_State(Entity_Unit _user)
    {
        m_user = _user;
        animationDuration = _user.GetSummoningTimer();
    }

    public void Enter()
    {
        previousTime = Time.time;
    }

    public void Execute()
    {
        if (previousTime == 0f)
        {
            previousTime = Time.time;
        }

        m_user.StopMoving();

        //Debug.Log("Animation Timer : " + animationDuration);
        if (previousTime + animationDuration < Time.time)
        {
            //Debug.Log("Got Come here");
            m_user.ChangeState("chase");
        }
    }

    public void Exit()
    {

    }
}
