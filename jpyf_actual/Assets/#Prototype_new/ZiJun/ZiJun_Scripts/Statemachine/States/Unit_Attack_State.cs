using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Attack_State : IState
{
    Entity_Unit m_user;

    public Unit_Attack_State(Entity_Unit _user)
    {
        m_user = _user;
    }

    public void Enter()
    {
        //When entering, find a unit with that is nearest to unit
        //m_user.FindNearestInList();
    }

    public void Execute()
    {
        //if (!m_user.GetTarget() || (m_user.GetTarget().position - m_user.transform.position).magnitude > m_user.GetAttackRangeStat())//If there is not a target or target is not within attack range
        //    m_user.ChangeState("chase");

        if (!m_user.GetTarget() || !m_user.GetInAttackRange())//If there is not a target or target is not within attack range
            m_user.ChangeState("chase");

        m_user.Attack();//Attack if enemy is in range
        
    }

    public void Exit()
    {
        
    }
}
