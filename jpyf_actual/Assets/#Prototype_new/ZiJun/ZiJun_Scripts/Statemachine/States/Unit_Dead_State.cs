using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Dead_State : IState
{
    Entity_Unit m_user;
    float CountDownTImer = 5f;//Init the death timw (Animation delay before deletion)
    // For demo purpose
    //Vector3 rota_te = Vector3.zero;
    //float y_rota_te = 0;
    //float x_rota_te = 0;
    Vector3 deadpos = Vector3.zero;

    public Unit_Dead_State(Entity_Unit _user)
    {
        m_user = _user;
    }

    public void Enter()
    {
        CountDownTImer = 2f;
    }

    public void Execute()
    {
        CountDownTImer -= Time.deltaTime;
        deadpos = m_user.transform.localPosition;
        deadpos.y -= 1;
        m_user.transform.localPosition = deadpos;
        //y_rota_te += 100;
        //x_rota_te -= 0.1f;
        //rota_te.y = y_rota_te;
        //rota_te.x = x_rota_te;

        //m_user.transform.localEulerAngles = rota_te;
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
