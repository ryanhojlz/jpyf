﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Chase_State : IState
{
    Entity_Unit m_user;

    public Unit_Chase_State(Entity_Unit _user)
    {
        m_user = _user;
    }

    public void Enter()
    {
        m_user.StartMoving();//Setting it to start moving
    }

    public void Execute()
    {
        if (m_user.InRangeCount() <= 0)//if there is nothing within range, goes to chase cart
        {
            m_user.ChangeState("chase_cart");
        }
        else
        {
            m_user.FindNearestInList();//find the nearest unit within chasing range
        }

        if (m_user.GetTarget())//If there is a target
        {
            if ((m_user.GetTarget().position - m_user.transform.position).magnitude < m_user.GetAttackRangeStat())//if is within attack range, goes to attack state
            {
                m_user.ChangeState("attack");
            }
        }

    }

    public void Exit()
    {
        m_user.StopMoving();//Setting it to start moving
    }
}