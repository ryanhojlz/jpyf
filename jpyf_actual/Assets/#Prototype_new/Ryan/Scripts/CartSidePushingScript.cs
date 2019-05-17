using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartSidePushingScript : MonoBehaviour
{
    // Player enter object
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
    private void FixedUpdate()
    {
        if (playerEnter)
        {
            if (PS4_ControllerScript.Instance.ReturnSquareDown())
            {
                // If square is pressed
                if (PS4_ControllerScript.Instance.ReturnLeft_AnalogUp())
                {
                    //// If analog up
                    //if (AxisLR == 1)
                    //{
                    //    // Move Right

                    //}
                    //else if (AxisLR == -1)
                    //{
                    //    // Move Left
                    //}
                    playerrb.velocity = cartRb.velocity = pushingforce;
                }
                else if (PS4_ControllerScript.Instance.ReturnLeft_AnalogDown())
                {
                    playerrb.velocity = cartRb.velocity = -pushingforce;
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
