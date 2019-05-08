using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorCartScript : MonoBehaviour
{
    // pay load reference
    public Transform m_payLoad = null;
    // anchor point
    public Vector3 m_anchorPoint;
    //
    bool b_startAnchoring = false;
    //
    Object_ControlScript handler_ctrl = null;
    
	// Use this for initialization
	void Start ()
    {
        m_payLoad = GameObject.Find("PayLoad").transform;
        m_anchorPoint = transform.position;
        m_anchorPoint.y = 0;
        m_anchorPoint.y = 0;
        handler_ctrl = GameObject.Find("PS4_ObjectHandler").GetComponent<Object_ControlScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (b_startAnchoring)
        {
            transform.parent.GetComponent<Tile_EventScript>().Start_Event();
            handler_ctrl.isPushingCart = false;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_payLoad.gameObject)
        {
            Debug.Log("Hi");
            b_startAnchoring = true;
            m_payLoad.transform.position = m_anchorPoint;
        }
    }
}
