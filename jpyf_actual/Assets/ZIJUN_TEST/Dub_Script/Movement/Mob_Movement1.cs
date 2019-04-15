using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mob_Movement : MonoBehaviour
{
    [SerializeField]
    GameObject TargetPosition;

    NavMeshAgent agent;

    Vector3 PrevForward = Vector3.zero;

    float currentAngularVelocity = 0f;

    //For checking purposes
    Vector3 PlayerPosition_Without_y;
    Vector3 CartPosition_Without_y;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

        //WaypointManager ListOfWaypoint = GameObject.Find("WaypointManager").GetComponent<WaypointManager>();

        //if (ListOfWaypoint.GetSize() > 0)
        //{
        //    WaypointIndex = ListOfWaypoint.GetNearestWaypoint(this.transform);//Assigning it a path
        //    agent.SetDestination(ListOfWaypoint.GetWaypointPosition(WaypointIndex, this.transform));//Finding the position and move to it
        //}

        //PrevForward = transform.forward;
    }
	// Update is called once per frame
	void Update ()
    {
        CheckForRotation();
        agent.SetDestination(TargetPosition.transform.position);
    }

    bool CheckForRotation()
    {
        currentAngularVelocity = Vector3.Angle(transform.forward, PrevForward) / Time.deltaTime;
        PrevForward = transform.forward;

        if (currentAngularVelocity > 3f)
        {
            agent.isStopped = true;//Stops it from moving
            return true;
        }
        else
        {
            agent.isStopped = false;//Making it move again
        }

        return false;
    }

}
