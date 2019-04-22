using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Projectile : MonoBehaviour
{
    //GameObject m_Owner = null;//The unit that shoots this projectile
    //GameObject m_Target = null;//The unit's target

    [SerializeField]
    protected float m_dmg = 0f; //Unit's Damage

    [SerializeField]
    float m_speed = 0f;

    [SerializeField]
    Vector3 m_targetPos = Vector3.zero; //Where to shoot to

    [SerializeField]
    Vector3 m_Direction = Vector3.zero;

    [SerializeField]
    float m_lifetime = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_lifetime -= Time.deltaTime;
        if (m_lifetime < 0)
        {
            Destroy(this.gameObject);
            return;
        }

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

    public float GetDamage() {return m_dmg; }

    public void SetSpeed(float _speed) { m_speed = _speed; }

    public void SetLifeTime(float _lifetime) { m_lifetime = _lifetime + (_lifetime * 0.5f);  }//Added 1 to give it abit further range

    public void SetDirection(Vector3 _targetPos, Vector3 _shooterPos)
    {
        m_Direction = (_targetPos - _shooterPos).normalized;
    }

    public void SetProjectileTag(string _tag)
    {
        this.tag = _tag;
    }

    public Vector3 GetDirection()
    {
        return m_Direction;
    }

    public void OnTriggerEnter(Collider other)
    {
       
        if (other.GetComponent<Entity_Unit>() && this.tag != other.tag)//Only if it is an Entity Unit
        {
            Debug.Log("Take Damage");
            other.GetComponent<Entity_Unit>().TakeDamage(m_dmg);
            Destroy(this.gameObject);
        }
       
        if (other.tag == "Payload")
        {
            HitCart(other);
            Destroy(this.gameObject);
        }

        if (other.tag == "Player2")
        {
            HitPlayer(other);
            Destroy(this.gameObject);
        }
    }

    public virtual void HitCart(Collider other)
    {
        GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>().Cart_TakeDmg((int)m_dmg);
    }

    public virtual void HitPlayer(Collider other)
    {
        other.GetComponent<PS4_PlayerHitboxScript>().TakeDamage((int)m_dmg);
    }
}
