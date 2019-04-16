using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Projectile : MonoBehaviour
{
    //GameObject m_Owner = null;//The unit that shoots this projectile
    //GameObject m_Target = null;//The unit's target

    [SerializeField]
    float m_dmg = 0f; //Unit's Damage

    [SerializeField]
    float m_speed = 0f;

    [SerializeField]
    Vector3 m_targetPos = Vector3.zero; //Where to shoot to

    [SerializeField]
    Vector3 m_Direction = Vector3.zero;

    [SerializeField]
    float m_lifetime;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_Direction == Vector3.zero)//Somehow the Direction is 0, 0, 0;
        {
            Destroy(this.gameObject);
            return;
        }

        this.transform.position += m_Direction * m_speed * Time.deltaTime;
    }

    public void SetTarget(Vector3 _targetPos)
    {
        m_targetPos = _targetPos;
    }

    public void SetDamage(float _dmg) { m_dmg = _dmg; }

    public void SetSpeed(float _speed) { m_speed = _speed; }

    public void SetLifeTime(float _lifetime) { m_lifetime = _lifetime;  }

    public void SetDirection(Vector3 _targetPos, Vector3 _shooterPos)
    {
        m_Direction = (_targetPos - _shooterPos).normalized;
    }

    public Vector3 GetDirection()
    {
        return m_Direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Entity_Unit>())//Only if it is an Entity Unit
        {
            Debug.Log("Take Damage");
            other.GetComponent<Entity_Unit>().TakeDamage(m_dmg);
        }
    }
}
