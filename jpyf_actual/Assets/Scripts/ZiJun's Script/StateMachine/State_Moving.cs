using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class State_Moving : IState
{
    //public void Enter()
    //{

    //}

    //public void Execute()
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void Exit()
    //{

    //}
    private NavMeshAgent agent;
    int index = new int();

    public State_Moving(NavMeshAgent _agent, int _index)
    {
        agent = _agent;
        index = _index;
    }

    public void Enter()
    {
        
    }

    public void Execute()
    {
        //index = 10;
        //Debug.Log("Excuting");
        //agent.SetDestination(tempPathManager.GetComponent<PathManager>().GetNextWaypoint(pathIndex, waypointIndex));
    }

    public void Exit()
    {
        
    }
}
