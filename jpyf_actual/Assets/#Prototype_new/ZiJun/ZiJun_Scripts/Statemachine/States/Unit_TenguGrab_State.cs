using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_TenguGrab_State : IState
{
    Entity_Unit m_user;

    public Unit_TenguGrab_State(Entity_Unit _user)
    {
        m_user = _user;
    }

    public void Enter()
    {
        //Debug.Log("HIIII");
    }

    public void Execute()
    {
        //if(m_user.transform.childCount <= 0)
        //{
        //    m_user.ReturnPreviousState();//After finish stun, go back to previous state
        //}
        //Debug.Log("HIIII");
        
    }

    public void Exit()
    {
       
    }
}
