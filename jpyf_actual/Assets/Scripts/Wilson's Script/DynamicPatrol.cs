﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Code
{
    public class DynamicPatrol : MonoBehaviour
    {
        //Dictates whether the agent waits on each node.
        public bool patrolWaiting;

        //Total time taken to wait at each node
        public float totalWaitTime = 3f;

        //probability of switching direction
        public float switchProbability = 0.2f;

        //Private variables for base behaviour
        NavMeshAgent navMeshAgent;
        ConnectedWaypoint currentWaypoint;
        ConnectedWaypoint previousWaypoint;

        bool travelling;
        bool waiting;
        float waitTimer;
        int waypointsVisited;

        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = this.GetComponent<NavMeshAgent>();

            if (navMeshAgent == null) 
            {
                Debug.LogError("The nav mesh agent component is not attached to" + gameObject.name);
            }
            else
            {
                if(currentWaypoint == null)
                {
                    //Set at random, grab all the waypoints in the scene
                    GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

                    if (allWaypoints.Length > 0) 
                    {
                        while (currentWaypoint == null)
                        {
                            int random = UnityEngine.Random.Range(0, allWaypoints.Length);
                            ConnectedWaypoint startingWaypoint = allWaypoints[random].GetComponent<ConnectedWaypoint>();

                            //found waypoint
                            if (startingWaypoint != null) 
                            {
                                currentWaypoint = startingWaypoint;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("Failed to find any waypoints for use in the scene.");
                    }
                }
                SetDestination();
            }
        }

        // Update is called once per frame
        public void Update()
        {
            //Check if we are close to the destination
            if (travelling && navMeshAgent.remainingDistance <= 1.0f) 
            {
                travelling = false;
                waypointsVisited++;

                //if going to wait
                if (patrolWaiting)
                {
                    waiting = true;
                    waitTimer = 0f;
                }
                else
                {
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

                    SetDestination();
                }
            }
        }

        private void SetDestination()
        {
            if (waypointsVisited > 0) 
            {
                ConnectedWaypoint nextWaypoint = currentWaypoint.NextWaypoint(previousWaypoint);
                previousWaypoint = currentWaypoint;
                currentWaypoint = nextWaypoint;
            }

            Vector3 targetVector = currentWaypoint.transform.position;
            navMeshAgent.SetDestination(targetVector);
            travelling = true;
        }
    }
}
