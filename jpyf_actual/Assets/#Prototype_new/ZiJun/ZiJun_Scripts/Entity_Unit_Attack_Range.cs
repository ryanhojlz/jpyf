using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Unit_Attack_Range : MonoBehaviour
{

    [SerializeField]
    GameObject Unit;

    Entity_Unit m_entity_unit = null;

    // Use this for initialization
    void Start()
    {
        if (Debug.isDebugBuild && !Unit)//Debug purposes
        {
            Debug.Log("No Unit is being place at the inspector");
            return;
        }

        m_entity_unit = Unit.GetComponent<Entity_Unit>();

        //if (Unit.GetComponent<Entity_Unit>())
        //{

        //    Range = Unit.GetComponent<Entity_Unit>().GetAttackRangeStat();
        //    this.GetComponent<SphereCollider>().radius = Range;
        //}

        if (m_entity_unit)
        {
            this.GetComponent<SphereCollider>().radius = m_entity_unit.GetAttackRangeStat();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_entity_unit)
        {
            if (m_entity_unit.GetTarget() == null)
            {
                //Debug.Log("Target's name : " + Unit.GetComponent<Entity_Unit>().GetTarget().name);
                m_entity_unit.SetInAttackRange(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (m_entity_unit)
        {
            //if (other.gameObject.name == "PayLoad")
            //{
            //    Debug.Log("Collided's name : " + other.name);
            //}
            //Debug.Log("Collided's name : " + other.name);
            if (m_entity_unit.GetTarget() == other.transform)
            {
                m_entity_unit.SetInAttackRange(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if(other.name == "PayLoad")
            //Debug.Log("Why u go out? : " + other.name);

        if (m_entity_unit)
        {
            //Debug.Log("Collided_exit name : " + other.name);
            if (m_entity_unit.GetTarget() == other.transform)
            {
                m_entity_unit.SetInAttackRange(false);
            }
        }
    }
}
