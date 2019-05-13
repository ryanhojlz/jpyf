using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS4_PlayerHitboxScript : MonoBehaviour
{
    Stats_ResourceScript handler = null;
    Object_ControlScript objHandler = null;
	// Use this for initialization
	void Start ()
    {
        handler = GameObject.Find("Stats_ResourceHandler").GetComponent< Stats_ResourceScript>();
	}

    //// Update is called once per frame
    void Update()
    {
        Debug.Log("wdawdwd " + Object_ControlScript.Instance.isGrounded);
    }

    public void TakeDamage( int damage)
    {
        handler.m_P2_hp -= damage;
        if (handler.m_P2_hp < 0)
        {
            handler.m_P2_hp = 0;
        }
        else if (handler.m_P2_hp > handler.m_P2_hpCap)
        {
            handler.m_P2_hp = handler.m_P2_hpCap;
        }
    }

    
    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.GetComponent<Entity_Projectile>())
    //    {
    //        //TakeDamage((int)other.GetComponent<Entity_Projectile>().GetDamage());
    //        Destroy(other.gameObject);
    //    }
    //}

    public void OnCollisionEnter(Collision collision)
    {
        if (!Object_ControlScript.Instance.isGrounded)
        {
            if (collision.gameObject.tag == "floor")
            {
                Object_ControlScript.Instance.isGrounded = true;
            }
        }

       

    }

   

}
