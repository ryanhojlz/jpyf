using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour
{
    [SerializeField]
    Transform m_targetPos;

    NavMeshAgent m_agent;

    Vector3 m_PrevForward = Vector3.zero;

    float m_currentAngularVelocity = 0f;

    //For checking purposes
    Vector3 m_PlayerPosition_Without_y;
    Vector3 m_CartPosition_Without_y;

    void Start()
    {
        m_agent = this.GetComponent<NavMeshAgent>();
        FindPayload();
    }
    // Update is called once per frame
    void Update()
    {
        //CheckForRotation(); //Don't think this will work well with unit

        //Constantly changing target position
        if (m_targetPos)
        {
            m_agent.SetDestination(m_targetPos.position);//Have to constantly changing he unit movement
        }
        else
        {
            m_agent.SetDestination(this.transform.position);//If no target is found, go to itself position
        }
    }

    void SetTarget(Transform _targetPos)
    {
        m_targetPos = _targetPos;
    }

    bool CheckForRotation()
    {
        m_currentAngularVelocity = Vector3.Angle(transform.forward, m_PrevForward) / Time.deltaTime;
        m_PrevForward = transform.forward;

        if (m_currentAngularVelocity > 3f)
        {
            m_agent.isStopped = true;//Stops it from moving
            return true;
        }
        else
        {
            m_agent.isStopped = false;//Making it move again
        }

        return false;
    }

    void FindPayload()
    {
        m_targetPos = GameObject.Find("PayLoad").transform;
    }

}
