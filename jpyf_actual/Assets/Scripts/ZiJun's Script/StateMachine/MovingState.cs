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



        //new Vector3(agent.velocity.x, agent.velocity.y, agent.velocity.z) + agent.transform.position).normalized()
        //if(!(agent.velocity.magnitude <= 0))
        //agent.transform.LookAt(front);

        //agent.gameObject.transform.Rotate(0, Mathf.Atan2(agent.velocity.x, agent.velocity.z), 0);

        //agent.gameObject.transform.rotation = Quaternion.RotateTowards(agent.gameObject.transform.rotation, Quaternion.LookRotation(front), 360).y;
    }

    public void Exit()
    {
        if (agent.isActiveAndEnabled && agent.isOnNavMesh)
            this.agent.isStopped = true;
    }
}
