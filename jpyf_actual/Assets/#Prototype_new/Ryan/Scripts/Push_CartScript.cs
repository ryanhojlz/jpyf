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
    }

    // Update is called once per frame
    void Update()
    {
        // If player 2 in range
        if (m_Player2)
        {
            // Debug
            if (m_ObjControl.pushCart)
            {
                m_ObjControl.isPushingCart = true;
            }
            else
            {
                m_ObjControl.isPushingCart = false;
            }
        }

        if (m_handler.m_CartHP > m_handler.m_CartHpCap * 0.7f)
        {
            m_CartSpeed = 0.01f;
        }
        else if (m_handler.m_CartHP > m_handler.m_CartHpCap * 0.4f)
        {
            m_CartSpeed = 0.005f;
        }
        else if (m_handler.m_CartHP > 0)
        {
            m_CartSpeed = 0.002f;
        }
        else if (m_handler.m_CartHP <=  0)
        {
            m_CartSpeed = 0.0f;
        }




        // Pushing Cart
        if (m_ObjControl.isPushingCart)
        {
            m_CartMoveDirection = m_ObjControl.movedir;
            m_CartMoveDirection.x = 0;
            m_CartMoveDirection.y = 0;
            m_CartParent.transform.position += (m_CartMoveDirection * m_CartSpeed);
        }
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag != "Player2")
    //        return;
    //    player2 = other.transform;
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player2")
        {
            m_Player2.parent = null;
            m_Player2 = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player2")
        {
            m_Player2 = other.transform;
            m_Player2.transform.parent = m_CartParent;
        }
    }


}


