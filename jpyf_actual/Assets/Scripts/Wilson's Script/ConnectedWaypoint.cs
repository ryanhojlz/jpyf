using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code
{
    public class ConnectedWaypoint : Waypoint
    {
        public float connectivityRadius = 50f;
        public List<ConnectedWaypoint> connections;

        // Start is called before the first frame update
        void Start()
        {
            //Grab all the waypoint objects in the scene
            GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

            //Create a list of waypoints to refer to later
            connections = new List<ConnectedWaypoint>();

            //check if they are a connected waypoint
            for (int i = 0; i < allWaypoints.Length; i++) 
            {
                ConnectedWaypoint nextWaypoint = allWaypoints[i].GetComponent<ConnectedWaypoint>();

                //if found a waypoint
                if(nextWaypoint != null)
                {
                    if (Vector3.Distance(this.transform.position, nextWaypoint.transform.position) <= connectivityRadius && nextWaypoint != this) 
                    {
                        connections.Add(nextWaypoint);
                    }
                }
            }
        }

        public override void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, DrawRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, connectivityRadius);
        }

        public ConnectedWaypoint NextWaypoint(ConnectedWaypoint previousWaypoint)
        {
            if(connections.Count == 0)
            {
                //No waypoints
                Debug.LogError("Insufficient waypoint count.");
                return null;
            }
            else if(connections.Count == 1&&connections.Contains(previousWaypoint))
            {
                //Only one waypoint and if it's the previous one then use that
                return previousWaypoint;
            }
            else //otherwise, find a random one that isn't the previous one
            {
                ConnectedWaypoint nextWaypoint;
                int nextIndex = 0;

                do
                {
                    nextIndex = UnityEngine.Random.Range(0, connections.Count);
                    nextWaypoint = connections[nextIndex];
                }
                while (nextWaypoint == previousWaypoint);

                return nextWaypoint;
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
