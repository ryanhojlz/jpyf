using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathClass : MonoBehaviour
{
    public List<WaypointClass> waypointList = new List<WaypointClass>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public WaypointClass GetWayPoint(int index)
    {
        if (waypointList.Count < 0)
            return null;

        //Debug.Log("ListCount : " + waypointList.Count);
        if (index < waypointList.Count)
        return waypointList[index];

        //If is the last one, return the last point
        return waypointList[waypointList.Count - 1];
        
    }

    public int GetSizeOfList()
    {
        return waypointList.Count;
    }
}
