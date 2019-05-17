using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartSidePushingScript : MonoBehaviour
{
    // Player enter object
    bool playerEnter = true;
    GameObject cart = null;
    GameObject player = null;

    [Header("-1 for Left / +1 for Right")]
    public int AxisLR = 0;

    // Use this for initialization
	void Start ()
    {
        player = GameObject.Find("PS4_Player");
        cart = PayloadMovementScript.Instance.payloadObject.gameObject;
	}

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (playerEnter)
        {
            if (PS4_ControllerScript.Instance.ReturnSquarePress())
            {
                if (PS4_ControllerScript.Instance.ReturnSquarePress())
                {
                    
                }
            }
        }
        else
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            playerEnter = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            playerEnter = true;
    }
}
