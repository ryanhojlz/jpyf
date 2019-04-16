using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Dead_State : IState
{
    Entity_Unit m_user;
    float CountDownTImer = 5f;//Init the death timw (Animation delay before deletion)

    public Unit_Dead_State(Entity_Unit _user)
    {
        m_user = _user;
    }

    public void Enter()
    {
        CountDownTImer = 5f;
    }

    public void Execute()
    {
        CountDownTImer -= Time.deltaTime;

        if (CountDownTImer < 0f)
        {
            Debug.Log("Dead");
            m_user.Dead();
        }
    }

    public void Exit()
    {
       
    }
}
