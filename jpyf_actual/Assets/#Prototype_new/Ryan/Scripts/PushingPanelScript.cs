using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingPanelScript : MonoBehaviour
{
    bool playerEnter = true;
    GameObject cart = null;
    GameObject player = null;
    Rigidbody cartRb = null;
    Rigidbody playerrb = null;
    Vector3 pushingforce = Vector3.zero;
    [Header("-1 for Left / +1 for Right")]
    public int AxisLR = 0;

    // Use this for initialization
    void Start ()
    {

        player = GameObject.Find("PS4_Player");
        cart = PayloadMovementScript.Instance.payloadObject.gameObject;
        playerrb = player.GetComponent<Rigidbody>();
        cartRb = cart.GetComponent<Rigidbody>();
        pushingforce.Set(20, 0, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    	
	}

    private void FixedUpdate()
    {
        
    }
}
