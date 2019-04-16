using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waypoint_Script : MonoBehaviour
{
    [SerializeField]
    GameObject m_waypoint;

    [SerializeField]
    Vector3 m_Waypoint_Pos;

    RaycastHit m_hit;
    Ray m_OnTerrain = new Ray();

    //Can remove on build (For debugging purposes only)
    Vector3 m_Prev_pos;

    //end Can Remove on build

    void Start()
    {
        m_waypoint = this.gameObject;//Assign this to waypoint

        SettingWaypointPosition();

        if (Debug.isDebugBuild)
            m_Prev_pos = m_waypoint.transform.position;//Setting the previous position
    }

    void Update()
    {
        if (Debug.isDebugBuild)//only if it is debug (Can delete this after finalizing)
        {
            if ((this.transform.position - m_Prev_pos).magnitude > 0f)
            {
                SettingWaypointPosition();//Changing waypoint if it is moved
            }

            m_Prev_pos = m_waypoint.transform.position;
        }
    }

    void SettingWaypointPosition()
    {
        //Debug.Log("Updating Waypoint position of " + this.name);//If the waypoint is newly assigned, this line will prompt 

        m_OnTerrain.origin = transform.position;
        m_OnTerrain.direction = Vector3.down;

        if (!m_waypoint)
        {
            Debug.Log("There is no GameObject on this : " + this.name);
        }
        else
        {
            Physics.Raycast(m_OnTerrain, out m_hit);
        }

        m_Waypoint_Pos = m_hit.point;
    }

    public Vector3 GettingWaypointPosition(Transform unitRequesting)//To offset the waypoint's correctly
    {
        Vector3 offset_Position = Vector3.zero;
        
        offset_Position.y = unitRequesting.lossyScale.y;

        return m_Waypoint_Pos + offset_Position;
    }
}
