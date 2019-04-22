﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Stun_State : IState
{
    Entity_Unit m_user;
    float CountDownTImer = 10f;//Init the death timw (Animation delay before deletion)

    public Unit_Stun_State(Entity_Unit _user)
    {
        m_user = _user;
    }

    public void Enter()
    {
        CountDownTImer = 10f;
    }

    public void Execute()
    {
        CountDownTImer -= Time.deltaTime;

        if (CountDownTImer < 0f)
        {
            Debug.Log("Finish_Stun");
            m_user.ReturnPreviousState();//After finish stun, go back to previous state
        }
    }

    public void Exit()
    {
       
    }
}
