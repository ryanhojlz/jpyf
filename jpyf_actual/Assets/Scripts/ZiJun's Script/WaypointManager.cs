using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{

    public List<WaypointClass> waypointList = new List<WaypointClass>();
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddWaypoint(WaypointClass waypoint)
    {
        waypointList.Add(waypoint);
    }

    public Vector3 GetNearestWaypoint(Vector3 player_position)
    {
        float nearest = float.MaxValue;
        Vector3 nearestpoint;

        Ray CastToGround = new Ray(player_position, Vector3.down);
        RaycastHit hit;
        Vector3 PlayerPos;
        nearestpoint = player_position;//Initing the pos for nearest point to be it's own point

        if (Physics.Raycast(CastToGround, out hit))
        {
            //Playerpos is raycasted to floor value
            PlayerPos = hit.point;
            
            for (int i = 0; i < waypointList.Count; ++i)
            {
                Vector3 waypointPos = waypointList[i].GetRayCast();
                float dist = (PlayerPos - waypointPos).magnitude;

                if (dist < nearest)
                {
                    nearest = dist;
                    nearestpoint = waypointPos;
                }
            }

        }

       
        return nearestpoint;

    }

    public List<WaypointClass> GetWaypointList()
    {
        return waypointList;
    }

    public void RemoveWaypoint(WaypointClass waypoint)
    {
        for (int i = 0; i < waypointList.Count; ++i)
        {
            if(waypointList[i] == waypoint)
            {
                waypointList.Remove(waypointList[i]);
            }
        }
    }
}
