using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MovingState : IState
{
    NavMeshAgent agent;
    float movespeed;

    public MovingState(NavMeshAgent _agent, float _movespeed)
    {
        //Debug.Log(_movespeed);
        this.agent = _agent;
        this.movespeed = _movespeed;
        this.agent.speed = movespeed;
    }

    public void Enter()
    {
        //Debug.Log(agent.tag);
        if(agent.isActiveAndEnabled && agent.isOnNavMesh)
          this.agent.isStopped = false;
    }

    public void Execute()
    {
        //Debug.Log(agent.velocity);

        Vector3 front = new Vector3(agent.velocity.x, agent.velocity.y, agent.velocity.z);
        front.Normalize();
        front += agent.transform.position;
 }

    public void Exit()
    {
        if (agent.isActiveAndEnabled && agent.isOnNavMesh)
            this.agent.isStopped = true;
    }
}
