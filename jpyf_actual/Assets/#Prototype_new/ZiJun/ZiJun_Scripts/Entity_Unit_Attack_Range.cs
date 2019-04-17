using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Unit_Attack_Range : MonoBehaviour
{

    [SerializeField]
    GameObject Unit;

    float Range;

    // Use this for initialization
    void Start()
    {
        if (Debug.isDebugBuild && !Unit)//Debug purposes
        {
            Debug.Log("No Unit is being place at the inspector");
            return;
        }

        if (Unit.GetComponent<Entity_Unit>())
        {
            Range = Unit.GetComponent<Entity_Unit>().GetAttackRangeStat();
            this.GetComponent<SphereCollider>().radius = Range;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Unit.GetComponent<Entity_Unit>())
        {
            if (Unit.GetComponent<Entity_Unit>().GetTarget() == null)
            {
                Debug.Log("Target's name : " + Unit.GetComponent<Entity_Unit>().GetTarget().name);
                Unit.GetComponent<Entity_Unit>().SetInAttackRange(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Unit.GetComponent<Entity_Unit>())
        {
            Debug.Log("Collided's name : " + other.name);
            if (Unit.GetComponent<Entity_Unit>().GetTarget() == other.transform)
            {
                Unit.GetComponent<Entity_Unit>().SetInAttackRange(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Unit.GetComponent<Entity_Unit>())
        {
            Debug.Log("Collided_exit name : " + other.name);
            if (Unit.GetComponent<Entity_Unit>().GetTarget() == other.transform)
            {
                Unit.GetComponent<Entity_Unit>().SetInAttackRange(false);
            }
        }
    }
}
