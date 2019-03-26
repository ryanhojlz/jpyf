using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

//base class for enemies
public class Minion : BasicGameOBJ
{
    public enum MINIONTYPE
    {
        MELEE,
        RANGE,
        FLYING
    }

    public MINIONTYPE MinionType;
    public List<MINIONTYPE>targetableType;

    void Start()
    {
        OBJ_TYPE = OBJType.MINION;
        ChangeToMoveState();
    }

    protected void ChangeToMoveState()
    {
        this.stateMachine.ChangeState(new MovingState(this.GetComponent<NavMeshAgent>(), moveSpeedValue));//state machine
    }
 
    protected override void ClassUpdate()
    {

        Unit_Self_Update();//Update for indivisual units (unique to each type of unit) 
      
        if (target == null && isActive)
        {
            //Debug.Log(name);
            ChangeToMoveState();
        }

        //if (healthValue <= 0)
        //{
        //    target = null;

        //    this.stateMachine.ChangeState(new DeadState(this));//state machine
        //}
    }

    public virtual void Unit_Self_Update()
    {

    }
}
