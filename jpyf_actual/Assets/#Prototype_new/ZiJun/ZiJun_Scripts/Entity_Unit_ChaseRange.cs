using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Unit_ChaseRange : MonoBehaviour
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
            Range = Unit.GetComponent<Entity_Unit>().GetChaseRangeStat();
            this.GetComponent<SphereCollider>().radius = Range;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Player2" && other.tag != "Payload") //if is enemy unit, ignore each other
            return;

        if (Unit.GetComponent<Entity_Unit>())
        {
            Unit.GetComponent<Entity_Unit>().AddToUnitsInRange(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Unit.GetComponent<Entity_Unit>())
        {
            Unit.GetComponent<Entity_Unit>().RemoveFromUnitsInRange(other.gameObject);
        }
    }
}
