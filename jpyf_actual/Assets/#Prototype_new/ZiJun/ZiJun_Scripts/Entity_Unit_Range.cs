using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Unit_Range : MonoBehaviour
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
            Range = Unit.GetComponent<Entity_Unit>().GetRangeStat();
            this.GetComponent<SphereCollider>().radius = Range;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Unit.GetComponent<Entity_Unit>().AddToUnitsInRange(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        Unit.GetComponent<Entity_Unit>().RemoveFromUnitsInRange(other.gameObject);
    }
}
