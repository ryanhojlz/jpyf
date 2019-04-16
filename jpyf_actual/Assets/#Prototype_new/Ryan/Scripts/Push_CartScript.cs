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
    public float m_CartFriction;

    // Use this for initialization
    void Start()
    {
        // Parent finding
        m_CartParent = transform.parent.transform.parent;
        // Object Controller
        m_ObjControl = GameObject.Find("PS4_ObjectHandler").GetComponent<Object_ControlScript>();
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
        if (m_ObjControl.isPushingCart)
        {
            m_CartMoveDirection = m_ObjControl.movedir;
            m_CartMoveDirection.x = 0;
            m_CartParent.transform.position += m_CartMoveDirection * 0.01f;
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


