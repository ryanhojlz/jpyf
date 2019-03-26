using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeadState : IState
{
    BasicGameOBJ unit;

    //need to pass values for the dead state?
    public DeadState(BasicGameOBJ _unit)
    {
        unit = _unit;
    }

    public void Enter()
    {
        unit.SetIsActive(false);
    }

    public void Execute()
    {
        //Debug.Log("Destroying");
        unit.Die();
    }

    public void Exit()
    {
        
    }
}
