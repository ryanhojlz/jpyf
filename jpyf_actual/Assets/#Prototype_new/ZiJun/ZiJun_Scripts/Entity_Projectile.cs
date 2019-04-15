using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Projectile : MonoBehaviour
{
    //GameObject m_Owner = null;//The unit that shoots this projectile
    //GameObject m_Target = null;//The unit's target

    float m_dmg = 0f; //Unit's Damage
    float m_speed = 0f;
    Vector3 m_targetPos = Vector3.zero; //Where to shoot to
    Vector3 Direction = Vector3.zero;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTarget(Vector3 _targetPos)
    {
        m_targetPos = _targetPos;
    }

    public void SetDamage(float _dmg)
    {
        m_dmg = _dmg;
    }

    public Vector3 GetDirection(Vector3 _targetPos)
    {
        return (_targetPos - this.transform.position).normalized;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Entity_Unit>())//Only if it is an Entity Unit
        {
            other.GetComponent<Entity_Unit>().TakeDamage(m_dmg);
        }
    }
}
