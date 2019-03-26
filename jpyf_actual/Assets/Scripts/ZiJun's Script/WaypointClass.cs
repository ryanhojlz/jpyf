using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointClass : MonoBehaviour
{
    //// Start is called before the first frame update
   
    private Vector3 position;
    private Vector3 RayCastPosition;

    void Start()
    {
        //AddWaypoint();
        //Destroy(this.GetComponent<MeshFilter>());

        Ray CastToGround = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(CastToGround, out hit))
        {
            this.position = transform.position;
            this.RayCastPosition = hit.point;

            //Debug.Log(position);
        }
    }

    void Update()
    {
        Ray CastToGround = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(CastToGround, out hit))
        {
            this.position = transform.position;
            this.RayCastPosition = hit.point;

            //Debug.Log(position);
        }
    }

    public Vector3 GetPos()
    {
        return position;
    }

    public void SetPos(Vector3 pos)
    {
        position = pos;
    }

    public Vector3 GetRayCast()
    {
        return RayCastPosition;
    }

    public void SetRayCast(Vector3 ray)
    {
        RayCastPosition = ray;
    }

    private void AddWaypoint()
    {
        Ray CastToGround = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(CastToGround, out hit))
        {
            //Setting the positions
            this.RayCastPosition = hit.point;
            this.position = transform.position;
            GameObject tempWaypointmanager;// = GameObject.FindGameObjectWithTag("WaypointManager");

            //Getting the variables
            if (this.tag == "Enemy_Waypoint")
            {
                tempWaypointmanager = GameObject.FindGameObjectWithTag("Enemy_WaypointManager");
                //Debug.Log("Added Enemy waypoint");
            }
            else
            {
                tempWaypointmanager = GameObject.FindGameObjectWithTag("WaypointManager");
                //Debug.Log("Added Ally waypoint");
            }

            if (tempWaypointmanager != null)
            {
                var accessingVar = tempWaypointmanager.GetComponent<WaypointManager>();

                //Adding into the list
                accessingVar.AddWaypoint(this);
            }
        }
    }

    //Commented out to test waypoints

    //private void OnDestroy()
    //{
    //    //Getting the variables
    //    var tempWaypointmanager = GameObject.FindGameObjectWithTag("WaypointManager");

    //    if (this.tag == "Enemy_Waypoint")
    //    {
    //        tempWaypointmanager = GameObject.FindGameObjectWithTag("Enemy_WaypointManager");
    //    }

    //    if (tempWaypointmanager != null)
    //    {
    //        var accessingVar = tempWaypointmanager.GetComponent<WaypointManager>();

    //        //Remove from list
    //        if (this != null && accessingVar != null)
    //        {
    //            accessingVar.RemoveWaypoint(this);
    //        }
    //    }
    //}
}
