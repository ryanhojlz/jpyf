using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public List<PathClass> PathList = new List<PathClass>();

    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < PathList.Count; ++i)
        //{
        //    for (int j = 0; j < PathList[i].GetComponent<PathClass>().waypointList.Count; ++j)
        //    {
        //        Debug.Log(i + " - " + j + " : " + PathList[i].GetComponent<PathClass>().waypointList[j].GetRayCast());
        //    }
        //}
        //Debug.Log("High");
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < PathList.Count; ++i)
        //{
        //    for (int j = 0; j < PathList[i].GetComponent<PathClass>().waypointList.Count; ++j)
        //    {
        //        Debug.Log(i + " - " + j + " : " + PathList[i].GetComponent<PathClass>().waypointList[j].GetRayCast());
        //    }
        //}

        //Debug.Log("High");
    }

    public int AssignPath(Vector3 playerpos, string UnitTag)
    {
        float nearest = float.MaxValue;
        int NearestPath = -1;//Negative 1 == no path

        //RayCasting the player to the ground
        Ray CastToGround = new Ray(playerpos, Vector3.down);
        RaycastHit hit;
        Physics.Raycast(CastToGround, out hit);
        Vector3 PlayerPos = hit.point;

        string findTargetPath = "NoTag";
        if (UnitTag == "Enemy_Unit")
        {
            findTargetPath = "Enemy_Path";
        }
        else if (UnitTag == "Ally_Unit")
        {
            findTargetPath = "Ally_Path";
        }
       
        for (int i = 0; i < PathList.Count; ++i)
        {
            //If the size of the waypoint is not less then 0
            if (PathList[i].GetSizeOfList() > 0 && PathList[i].tag == findTargetPath)
            {
                
                float temp = (PlayerPos - PathList[i].GetWayPoint(0).GetRayCast()).magnitude;
                if (temp < nearest)
                {
                    NearestPath = i;
                    nearest = temp;
                }
            }
        }
        return NearestPath;
    }

    public Vector3 GetNextWaypoint(int PlayerPath_Index,int PlayerWaypoint_Index)
    {
        // Debug.Log("PlayerWaypoint_Index = " + PlayerWaypoint_Index);
        //Gets the waypoint
        return PathList[PlayerPath_Index].GetWayPoint(PlayerWaypoint_Index).GetRayCast();
    }

    public bool ReachDestination(int PlayerPath_Index, int PlayerWaypoint_Index, float DistanceStop, Vector3 playerpos)
    {
        Ray CastToGround = new Ray(playerpos, Vector3.down);
        RaycastHit hit;
        Physics.Raycast(CastToGround, out hit);
        Vector3 PlayerPos = hit.point;
        float DistBt = (PlayerPos - PathList[PlayerPath_Index].GetWayPoint(PlayerWaypoint_Index).GetRayCast()).magnitude;

        if (DistBt < DistanceStop)
        {
            return true;
        }
       
        return false;
    }

    public void TestValue(int PlayerPath_Index, int PlayerWaypoint_Index, Vector3 playerpos)
    {
        Ray CastToGround = new Ray(playerpos, Vector3.down);
        RaycastHit hit;
        Physics.Raycast(CastToGround, out hit);
        Vector3 PlayerPos = hit.point;
        float DistBt = (PlayerPos - PathList[PlayerPath_Index].GetWayPoint(PlayerWaypoint_Index).GetRayCast()).magnitude;
        Debug.Log("DistBt = " + DistBt);
    }

    public int GetPathWaypointCount(int PlayerPath_Index)
    {
        return PathList[PlayerPath_Index].GetSizeOfList();
    }
}
