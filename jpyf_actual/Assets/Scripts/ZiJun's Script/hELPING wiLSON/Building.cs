using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : BasicGameOBJ
{
    void Start()
    {
        OBJ_TYPE = OBJType.BUILDING;
    }

    protected override void ClassUpdate()
    {
        Unit_Self_Update();//Update for indivisual units (unique to each type of unit) 

        if (target == null && isActive)
        {
            Debug.Log(name);
        }

    }

    public virtual void Unit_Self_Update()
    {

    }
}
