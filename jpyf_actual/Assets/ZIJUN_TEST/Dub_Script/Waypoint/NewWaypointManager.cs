using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWaypointManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> ListOfWaypoint;
    
    public int GetNearestWaypoint(Transform GameObject)
    {
        float nearest = float.MaxValue;
        float DistanceCheck = 0f;
        int NearestIndex = -1;

        for (int i = 0; i < ListOfWaypoint.Count; ++i)
        {
            DistanceCheck = (ListOfWaypoint[i].GetComponent<Waypoint_Script>().GettingWaypointPosition(GameObject) - GameObject.transform.position).magnitude;
            if (DistanceCheck < nearest)
            {
                nearest = DistanceCheck;
                NearestIndex = i;
            }
        }

        if (NearestIndex == -1)
        {
            Debug.Log("No Waypoint found returning -1");
        }
        else
        {
            Debug.Log("Waypoint found : " + ListOfWaypoint[NearestIndex].name);
        }

        return NearestIndex;
    }

    public Vector3 GetWaypointPosition(int index, Transform GameObject)
    {
        if (index != -1)
        {
            Debug.Log("Waypoint found : " + ListOfWaypoint[index].name);
            return ListOfWaypoint[index].GetComponent<Waypoint_Script>().GettingWaypointPosition(GameObject);
        }

        Debug.Log("Invalid waypoint or not waypoint");

        return GameObject.position;
    }

    public int GetSize()
    {
        return ListOfWaypoint.Count;
    }
}
