using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Attack_State : IState
{
    Entity_Unit m_user;
    float previousTime = 0f;

    public Unit_Attack_State(Entity_Unit _user)
    {
        m_user = _user;
    }

    public void Enter()
    {
        //When entering, find a unit with that is nearest to unit
        //m_user.FindNearestInList();
        //Debug.Log("Entering : " + m_user.GetTarget());
    }

    public void Execute()
    {
        //if (!m_user.GetTarget() || (m_user.GetTarget().position - m_user.transform.position).magnitude > m_user.GetAttackRangeStat())//If there is not a target or target is not within attack range
        //    m_user.ChangeState("chase");
        if (Time.time > previousTime + 1f)
        {
            previousTime = Time.time;
            m_user.FindNearestInList();
        }

        if ((!m_user.GetTarget() || !m_user.GetInAttackRange()) && !m_user.GetStillAttacking())
        {//If there is not a target or target is not within attack range
            m_user.ChangeState("chase");
            return;
        }

        //Debug.Log(m_user.GetTarget());

        if (!m_user.GetStillAttacking())
        m_user.Attack();//Attack if enemy is in range
        
    }

    public void Exit()
    {
        //Debug.Log("Exiting : " + m_user.GetTarget());
    }
}
