using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Push_CartScript : MonoBehaviour
{
    // The actual Cart
    public Transform m_CartParent = null;
    // Player 2
    public Transform m_Player2 = null;
    // Player Object Controller
    public Object_ControlScript m_ObjControl = null;

    // Cart MoveDir 
    public Vector3 m_CartMoveDirection = Vector3.zero;

    // Cart Move Speed Multiplier
    public float m_CartSpeed = 0.01f;

    // Cart BuffSpeed
    public float m_CartBuffSpeed = 0;
    // Debuff Speed n Debuff Duration
    public float m_SpeedDebuff = 0;
    public float debuffDuration = 0;

    public float m_stunDuration = 0;

    Transform point;
    // Game Handler
    public Stats_ResourceScript m_handler = null;

    // Use this for initialization
    void Start()
    {
        // Parent finding
        m_CartParent = transform.parent.transform.parent;
        // Object Controller
        m_ObjControl = GameObject.Find("PS4_ObjectHandler").GetComponent<Object_ControlScript>();

        // Handler
        m_handler = GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>();

        point = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        // If player 2 in range
        //if (m_Player2)
        //{
        //    // Debug
        //    if (m_ObjControl.pushCart)
        //    {
        //        m_ObjControl.isPushingCart = true;
        //    }
        //    else
        //    {
        //        m_ObjControl.isPushingCart = false;
        //    }
        //}

        // Adjusting / Hardcode cart speed
        if (m_handler.m_CartHP > m_handler.m_CartHpCap * 0.7f)
        {
            m_CartSpeed = 0.003f;
        }
        else if (m_handler.m_CartHP > m_handler.m_CartHpCap * 0.5f)
        {
            m_CartSpeed = 0.002f;
        }
        else if (m_handler.m_CartHP > m_handler.m_CartHpCap * 0.3f)
        {
            m_CartSpeed = 0.0015f;
        }
        else if (m_handler.m_CartHP > 0)
        {
            m_CartSpeed = 0.001f;
        }
        else if (m_handler.m_CartHP <= 0)
        {
            m_CartSpeed = 0.0f;
        }




        //// Pushing Cart
        //if (m_ObjControl.isPushingCart)
        //{
        //    m_CartMoveDirection = m_ObjControl.movedir;
        //    m_CartMoveDirection.x = 0;
        //    m_CartMoveDirection.y = 0;
        //    m_CartParent.transform.position += (m_CartMoveDirection * m_CartSpeed);
        //    m_Player2.GetComponent<Rigidbody>().isKinematic = true;
        //}
        //else
        //{
        //    if (m_Player2)
        //        m_Player2.GetComponent<Rigidbody>().isKinematic = false;
        //}


        if (m_Player2)
        {
            if (m_ObjControl.checkCart)
            {
                if (m_ObjControl.isPushingCart)
                {
                    m_ObjControl.isPushingCart = false;
                }
                else if (!m_ObjControl.isPushingCart)
                {
                    m_ObjControl.isPushingCart = true;
                }
            }

            if (m_ObjControl.Gropper)
            {
                m_ObjControl.isPushingCart = false;
                m_Player2 = null;
            }



            if (debuffDuration < 0)
            {
                m_SpeedDebuff = 0;
            }
            else
            {
                debuffDuration -= 1 * Time.deltaTime;
                if (debuffDuration <= 0)
                {
                    debuffDuration = 0;
                }
            }

            if (m_stunDuration > 0)
            {
                
                m_stunDuration -= 1 * Time.deltaTime;
                return;
            }
            else
            {
                m_stunDuration = 0;
            }

            if (m_ObjControl.isPushingCart)
            {
                // If dead return;
                m_Player2.transform.position = point.position;
                m_CartMoveDirection = m_ObjControl.movedir;
                m_CartMoveDirection.x = 0;
                m_CartMoveDirection.y = 0;
                float cartspeed = (m_CartSpeed + m_CartBuffSpeed) - m_SpeedDebuff;
                if (cartspeed < 0)
                    cartspeed = 0;
                m_CartParent.transform.position += (m_CartMoveDirection * cartspeed);
            }

        }
    }


    public void SetStunned(float duration)
    {
        m_stunDuration = duration;
    }

    public void SetDebuffSpeed(float speed, float duration)
    {
        debuffDuration = duration;
        m_SpeedDebuff = speed;
    }

    public float GetDebuffSpeed()
    {
        return m_SpeedDebuff;
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player2")
        {
            //m_Player2.GetComponent<Rigidbody>().isKinematic = false;
            m_Player2.parent = null;
            m_Player2 = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (m_ObjControl.Gropper)
            return;
        

        if (other.gameObject.tag == "Player2")
        { 
            m_Player2 = other.transform;
            m_Player2.transform.parent = m_CartParent;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player2")
    //    {
    //        m_Player2 = other.transform;
    //        m_Player2.transform.parent = m_CartParent;
    //    }
    //}



}


