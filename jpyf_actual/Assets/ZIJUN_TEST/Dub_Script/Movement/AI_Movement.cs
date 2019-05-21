using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour
{
    [SerializeField]
    GameObject Self;

    [SerializeField]
    Transform m_targetPos;

    Vector3 m_moveToPosition = Vector3.zero;

    NavMeshAgent m_agent;

    Vector3 m_PrevForward = Vector3.zero;

    float m_currentAngularVelocity = 0f;

    //For checking purposes
    Vector3 m_PlayerPosition_Without_y;
    Vector3 m_CartPosition_Without_y;

    Vector3 tempThis = new Vector3();
    Vector3 tempTarget = new Vector3();

    private void Awake()
    {
        //transform.parent = GameObject.Find("DebugParent").transform;
    }

    void Start()
    {
        if (!Self)
            Self = transform.GetChild(0).gameObject;
       m_agent = this.GetComponent<NavMeshAgent>();
       //FindPayload();
    }
    // Update is called once per frame
    void Update()
    {
        //CheckForRotation(); //Don't think this will work well with unit
        if (Self.GetComponent<Entity_Unit>())
        {
            if (Self.GetComponent<Entity_Unit>().GetTarget())
            {
                //Debug.Log(Self.GetComponent<Entity_Unit>().GetTarget().name);
                //m_targetPos = Self.GetComponent<Entity_Unit>().GetTarget();//Finding Self own target
            }
            m_targetPos = Self.GetComponent<Entity_Unit>().GetTarget();//Finding Self own target
        }

        ////// Experimental code
        //if (m_agent.pathPending)
        //{
        //    return;
        //}

        //Constantly changing target position
        if (m_targetPos)
        {
            m_agent.SetDestination(m_targetPos.position);//Have to constantly changing he unit movement
        }
        else//If the target is null, go to a set position
        {
            m_agent.SetDestination(m_moveToPosition);
        }

        if (m_agent.isStopped && m_targetPos)
        {
            if (Self.GetComponent<Entity_Unit>())
                if (!Self.GetComponent<Entity_Unit>().GetStillAttacking())
                {
                    tempThis.x = transform.position.x;
                    tempThis.z = transform.position.z;

                    tempTarget.x = m_targetPos.position.x;
                    tempTarget.z = m_targetPos.position.z;

                    var _direction = (tempTarget - tempThis).normalized;
                    var _lookRotation = Quaternion.LookRotation(_direction);

                    //Debug.Log(Mathf.Atan2(_lookRotation.y, _lookRotation.x));
                    if(!Self.GetComponent<Entity_Unit>().isStun())
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, _lookRotation, 10);
                }
        }
        //else
        //{
        //    Debug.Log("No Target found");
        //    FindPayload();
        //    m_agent.SetDestination(m_targetPos.position);//If no target is found, go to itself position
        //}
    }

    void SetTarget(Transform _targetPos)
    {
        m_targetPos = _targetPos;
    }

    public void SetTargetPosition(Vector3 _pos)
    {
        m_moveToPosition = _pos;
    }

    //bool CheckForRotation()
    //{
    //    m_currentAngularVelocity = Vector3.Angle(transform.forward, m_PrevForward) / Time.deltaTime;
    //    m_PrevForward = transform.forward;

    //    if (m_currentAngularVelocity > 3f)
    //    {
    //        m_agent.isStopped = true;//Stops it from moving
    //        return true;
    //    }
    //    else
    //    {
    //        m_agent.isStopped = false;//Making it move again
    //    }

    //    return false;
    //}

    //void FindPayload()//Use this function if controller player is not found & target the payload / Init the target position
    //{
    //    m_targetPos = GameObject.Find("PayLoad").transform;
    //}

    public void StopMoving()
    {
        //Debug.Log("Stop");
        if(m_agent && m_agent.isActiveAndEnabled)
            m_agent.isStopped = true;
    }

    public void StartMoving()
    {
        //Debug.Log("Move");
        if (m_agent && m_agent.isActiveAndEnabled)
            m_agent.isStopped = false;
    }

    public void ChangeNavAgentPosition(Vector3 position)
    {
        this.transform.position = position;
    }

}
