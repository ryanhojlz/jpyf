using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cart_Movement : MonoBehaviour
{
    public static Cart_Movement instance = null;

    [SerializeField]
    Vector3 waypoint;

    [SerializeField]
    int WaypointIndex;

    [SerializeField]
    float Moving_Range = 20;

    [SerializeField]
    GameObject TempPlayer;

    NavMeshAgent agent;

    Vector3 PrevForward = Vector3.zero;

    float currentAngularVelocity = 0f;

    //For checking purposes
    Vector3 PlayerPosition_Without_y;
    Vector3 CartPosition_Without_y;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();

        NewWaypointManager ListOfWaypoint = GameObject.Find("NewWaypointManager").GetComponent<NewWaypointManager>();

        if (ListOfWaypoint.GetSize() > 0)
        {
            WaypointIndex = ListOfWaypoint.GetNearestWaypoint(this.transform);//Assigning it a path
            agent.SetDestination(ListOfWaypoint.GetWaypointPosition(WaypointIndex, this.transform));//Finding the position and move to it
        }

        PrevForward = transform.forward;
    }
	// Update is called once per frame
	void Update ()
    {
        //agent.SetDestination(waypoint);
        //if (agent.remainingDistance <= agent.stoppingDistance)
        //{
        //    NextDestination();
        //}

        //if (CheckInMovingRange())
        //{
        //    CheckForRotation();
        //}
        
    }

    //Reason for having 2 is due to future rotation changes

    void NextDestination()//Going to the nextway point
    {
        Debug.Log("Assigning Next Waypoint");

        NewWaypointManager ListOfWaypoint = GameObject.Find("NewWaypointManager").GetComponent<NewWaypointManager>();

        int size = ListOfWaypoint.GetSize();

        if (size > 0)
        {
            if (++WaypointIndex >= size)
            {
                WaypointIndex = 0;
            }

            agent.SetDestination(ListOfWaypoint.GetWaypointPosition(WaypointIndex, this.transform));
        }
        else
        {
            Debug.Log("No waypoint found by using 'NextDestination Function' " + this.name);
        }
    }

    void PrevDestination()
    {
        Debug.Log("Assigning Prev Waypoint");

        NewWaypointManager ListOfWaypoint = GameObject.Find("WaypointManager").GetComponent<NewWaypointManager>();

        int size = ListOfWaypoint.GetSize();

        if (size > 0)
        {
            if (--WaypointIndex < 0)
            {
                WaypointIndex = size - 1;
            }

            agent.SetDestination(ListOfWaypoint.GetWaypointPosition(WaypointIndex, this.transform));
        }
        else
        {
            Debug.Log("No waypoint found by using 'NextDestination Function' " + this.name);
        }
    }

    bool CheckInMovingRange()
    {
        //After adding a player controller unit,
        PlayerPosition_Without_y = TempPlayer.transform.position;
        PlayerPosition_Without_y.y = 0f;
        CartPosition_Without_y = this.transform.position;
        CartPosition_Without_y.y = 0f;

        if ((PlayerPosition_Without_y - CartPosition_Without_y).magnitude < Moving_Range)
        {
            agent.isStopped = false;
            return true;
        }

        agent.isStopped = true;
        return false;
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

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Moving_Range);
        //Gizmos.DrawCube(transform.position, Moving_Range);
    }
}
