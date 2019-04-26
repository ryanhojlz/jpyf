using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_AFK_State : IState
{
    Entity_Unit m_user;

    public Unit_AFK_State(Entity_Unit _user)
    {
        m_user = _user;
    }

    public void Enter()
    {

    }

    public void Execute()
    {
        m_user.FindNearestInList();

        if (m_user.GetTarget() || m_user.GetinstantChasePlayer())//If target found go chase it
        {
            m_user.ChangeState("chase");
        }
    }

    public void Exit()
    {

    }

}
