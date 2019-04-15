using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Push_CartScript : MonoBehaviour
{
    // The actual Cart
    public Transform m_CartParent = null;
    // Player 2
    public Transform player2 = null;
    // Player Object Controller
    public Object_ControlScript objControl = null;

    // Use this for initialization
    void Start()
    {
        // Parent finding
        m_CartParent = transform.parent.transform.parent;
        // Object Controller
        objControl = GameObject.Find("PS4_ObjectHandler").GetComponent<Object_ControlScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // If player 2 in range
        if (player2)
        {
            // Debug
            if (objControl.pushCart)
            {
                objControl.isPushingCart = true;
            }
            else
            {
                objControl.isPushingCart = false;
            }
        }
        if (objControl.isPushingCart)
        {
            m_CartParent.transform.position += objControl.movedir * 0.01f;
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
            player2.parent = null;
            player2 = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player2")
        {
            player2 = other.transform;
            player2.transform.parent = m_CartParent;
        }
    }


}


