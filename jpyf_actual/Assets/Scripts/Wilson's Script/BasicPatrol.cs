using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BasicPatrol : MonoBehaviour
{
    public bool patrolWaiting; //whether the agent waits on each node
    public float totalWaitTime = 3f; //the total time waiting at each node
    public float switchProbability = 0.2f; //probability of switching directions
    public List<Waypoint> patrolPoints;

    NavMeshAgent navMeshAgent;
    int currentPatrolIndex;
    bool travelling;
    bool waiting;
    bool patrolForward;
    float waitTimer;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent component is not attached to " + gameObject.name);
        }
        else
        {
            if (patrolPoints != null && patrolPoints.Count >= 2) //Need to have at least 2 patrol points
            {
                currentPatrolIndex = 0;
                SetDestination();
            }
            else
            {
                Debug.Log("Insufficient patrol points for basic patrolling behaviour.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //to check if close to destination
        if (travelling && navMeshAgent.remainingDistance <= 1.0f)
        {
            travelling = false;

            //if going to wait
            if (patrolWaiting)
            {
                waiting = true;
                waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }
        //if waiting 
        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= totalWaitTime)
            {
                waiting = false;

                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if(patrolPoints != null)
        {
            Vector3 targetVector = patrolPoints[currentPatrolIndex].transform.position;
            navMeshAgent.SetDestination(targetVector);
            travelling = true;
        }
    }

    //Select a new patrol point in the list and also have probability to move forward or backward
    private void ChangePatrolPoint()
    {
        if(UnityEngine.Random.Range(0f,1f)<=switchProbability)
        {
            patrolForward = !patrolForward;
        }

        if(patrolForward)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
        else
        {
            if (--currentPatrolIndex < 0) 
            {
                currentPatrolIndex = patrolPoints.Count - 1;
            }
        }
    }
}
