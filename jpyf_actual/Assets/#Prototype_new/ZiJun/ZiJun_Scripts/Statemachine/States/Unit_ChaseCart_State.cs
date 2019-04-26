using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_ChaseCart_State : IState
{

    Entity_Unit m_user;

    public Unit_ChaseCart_State(Entity_Unit _user)
    {
        m_user = _user;
    }

    public void Enter()
    {
        m_user.StartMoving();//Setting it to start moving
    }

    public void Execute()
    {
        if (m_user.InRangeCount() > 0)//if there is nothing within range, goes to chase cart
        {
            m_user.ChangeState("chase");
        }
        //else if (!m_user.GetTarget())
        //{
        //    m_user.FindPayload();//If there is no target && no units in list, Chase cart
        //}

    }

    public void Exit()
    {
        m_user.StopMoving();//Setting it to start moving
    }
}
