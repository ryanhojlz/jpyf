using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherPushingCartScript : MonoBehaviour
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
    void Start()
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
#if UNITY_PS4
        if (playerEnter)
        {
            if (PayloadMovementScript.Instance.moveSideways)
            {
                if (PS4_ControllerScript.Instance.ReturnSquareDown())
                {
                    if (PayloadMovementScript.Instance.moveRight)
                    {
                        player.transform.position += (Vector3.right * 2) * Time.deltaTime;
                        cartRb.transform.position += (Vector3.right * 2) * Time.deltaTime;
                    }
                    else
                    {
                        player.transform.position -= (Vector3.right * 2) * Time.deltaTime;
                        cartRb.transform.position -= (Vector3.right * 2) * Time.deltaTime;
                    }
                }
            }
        }
#endif

#if UNITY_EDITOR_WIN
        if (playerEnter)
        {
            if (PayloadMovementScript.Instance.moveSideways)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    if (PayloadMovementScript.Instance.moveRight)
                    {
                        player.transform.position += (Vector3.right * 2) * Time.deltaTime;
                        cartRb.transform.position += (Vector3.right * 2) * Time.deltaTime;
                    }
                    else
                    {
                        player.transform.position -= (Vector3.right * 2) * Time.deltaTime;
                        cartRb.transform.position -= (Vector3.right * 2) * Time.deltaTime;
                    }
                }
            }
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
