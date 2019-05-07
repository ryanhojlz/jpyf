using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Hord_Nurakabe : MonoBehaviour
{
    public int m_damage;
    public float m_moveSpeed;
    public Vector3 dir = Vector3.zero;

    Stats_ResourceScript handler = null;
    // Use this for initialization
    void Start()
    {
        handler = Stats_ResourceScript.Instance;
        dir.Set(0, 0, -1);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.transform.forward * m_moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Payload")
        {
            AttackCart();
        }
        else if (other.tag == "Player2")
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        //Debug.Log("Player_Got_Hit");
        handler.Player2_TakeDmg(m_damage);
    }

    void AttackCart()
    {
        //Debug.Log("Cart_Got_Hit");
        handler.Cart_TakeDmg(m_damage);
    }
}
