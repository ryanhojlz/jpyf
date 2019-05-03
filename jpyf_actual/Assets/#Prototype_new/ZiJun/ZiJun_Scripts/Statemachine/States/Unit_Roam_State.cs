using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Roam_State : IState
{

    Entity_Unit m_user;
    Vector3 ForUse = Vector3.zero;

    float changeRoamTimer = 3f;//Set how long change 1 time 
    float previousTimer = 0f;
    public Unit_Roam_State(Entity_Unit _user)
    {
        m_user = _user;
    }

    public void Enter()
    {
        m_user.StartMoving();
    }

    public void Execute()
    {
        if (m_user.GetisIdle())
        {
            m_user.ChangeState("afk");
        }

        m_user.FindNearestInList();

        if (m_user.GetTarget())//If target found go chase it
        {
            m_user.ChangeState("chase");
        }
        else//No target, roam instead
        {
            
            if (previousTimer + changeRoamTimer < Time.time)
            {
                previousTimer = Time.time;

                float randX = 0f;
                float randZ = 0f;

                

                randX = Random.Range(-m_user.GetChaseRangeStat(), m_user.GetChaseRangeStat());
                randZ = Random.Range(-m_user.GetChaseRangeStat(), m_user.GetChaseRangeStat());

                ForUse.x = m_user.transform.position.x + randX;
                ForUse.z = m_user.transform.position.z + randZ;

                m_user.MoveToTargetedPosition(ForUse);
            }
        }
    }

    public void Exit()
    {
        m_user.StopMoving();
    }
}
