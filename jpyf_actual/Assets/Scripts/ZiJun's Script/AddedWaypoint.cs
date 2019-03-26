using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddedWaypoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Ray CastToGround = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(CastToGround, out hit))
        {
            //Getting the variables
            var tempWaypointmanager = GameObject.FindGameObjectWithTag("WaypointManager");
            var accessingVar = tempWaypointmanager.GetComponent<WaypointManager>();

            //Creating a new waypoint 
            WaypointClass newWaypoint = new WaypointClass();

            //Setting the position
            newWaypoint.SetRayCast(hit.point);
            newWaypoint.SetPos(transform.position);

            //Adding into the list
            accessingVar.AddWaypoint(newWaypoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
