using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Stun_State : IState
{
    Entity_Unit m_user;
    float CountDownTImer = 1f;//Init the death timw (Animation delay before deletion)
    float OriginalTime = 1f;

    public Unit_Stun_State(Entity_Unit _user)
    {
        m_user = _user;
    }

    public Unit_Stun_State(Entity_Unit _user, float stunTime)
    {
        m_user = _user;
        CountDownTImer = stunTime;
        OriginalTime = CountDownTImer;
    }

    public void Enter()
    {
        CountDownTImer = OriginalTime;

        
    }

    public void Execute()
    {
        CountDownTImer -= Time.deltaTime;

        if (CountDownTImer < 0f)
        {
            //Debug.Log("Finish_Stun");
            m_user.ChangeState("chase");//After finish stun, go back to previous state
        }
    }

    public void Exit()
    {
       
    }

    
}
