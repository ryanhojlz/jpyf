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
#if UNITY_PS4
        if (Object_ControlScript.Instance.Gropper)
        {
            return;
            playerEnter = false;
            Object_ControlScript.Instance.isPushingCart = false;

        }



        if (playerEnter)
        {
            if (PS4_ControllerScript.Instance.ReturnSquareDown())
            {
                Object_ControlScript.Instance.isPushingCart = true;
                PayloadMovementScript.Instance.cannotMove = true;
                playerrb.isKinematic = true;
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
                    //playerrb.velocity = cartRb.velocity = pushingforce;
                    player.transform.position += (Vector3.right * 2) * Time.deltaTime;
                    cartRb.transform.position += (Vector3.right * 2) * Time.deltaTime;
                }
                else if (PS4_ControllerScript.Instance.ReturnLeft_AnalogDown())
                {
                    player.transform.position -= (Vector3.right * 2) * Time.deltaTime;
                    cartRb.transform.position -= (Vector3.right * 2) * Time.deltaTime;

                    //playerrb.velocity = cartRb.velocity = -pushingforce;
                }

            }
            else
            {
                playerrb.isKinematic = false;
                Object_ControlScript.Instance.isPushingCart = false;
                PayloadMovementScript.Instance.cannotMove = false;
            }
        }
        else
        {
            playerrb.isKinematic = false;
            Object_ControlScript.Instance.isPushingCart = false;
            PayloadMovementScript.Instance.cannotMove = false;
        }
#endif
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            playerEnter = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {           
            Object_ControlScript.Instance.isPushingCart = false;
            playerEnter = false;
        }
    }
}
