using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : Attack_Unit
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Attack()
    {

    }

    public override void Unit_Self_Update()
    {
        if (minionWithinRange.Count > 0)
        {
            this.stateMachine.ChangeState(new AttackState(this, minionWithinRange, Enemy_Tag));
        }
    }
}
