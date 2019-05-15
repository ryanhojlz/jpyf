using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PayloadMovementScript : MonoBehaviour
{

    // Singleton
    public static PayloadMovementScript Instance = null;
    public Transform payloadObject = null;
    public Rigidbody payloadRb = null;

    private Transform player2 = null;


    

    // Trigger bool
    public bool playerInsideCart = false;
    // Variable use to modify instead of newing a vector cos vr
    private Vector3 capZVelocity = Vector3.zero;
    // Variable use in to cap velocity
    public float cartSpeed = 20;
    public float velocityCap = 10;

    // Tutorial Boolen
    public bool tutorial_bool = false;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else if (Instance)
            Destroy(this.gameObject);

        payloadObject = transform.parent;
        player2 = GameObject.Find("PS4_Player").transform;
    }
    // Use this for initialization
    void Start ()
    {
        payloadRb = payloadObject.GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void FixedUpdate()
    {
       // GameObject.Find("Text5").GetComponent<Text>().text = "" + Push_CartScript.Instance.m_stunDuration;


        if (GameEventsPrototypeScript.Instance.b_bigExplain)
        {
            return;
        }

        if (Push_CartScript.Instance.m_stunDuration > 0)
        {
            //payloadObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //payloadRb.velocity = Vector3.zero;
        }
        else
        { 
            if (playerInsideCart)
            {
                if (!tutorial_bool)
                    return;

                if (!GameEventsPrototypeScript.Instance.TileEvent_Start)
                {
                    // Move Payload
                    if (Stats_ResourceScript.Instance.m_CartHP >= 25)
                    {
                        // Assign velocity
                        //payloadObject.GetComponent<Rigidbody>().AddForce(payloadObject.transform.forward * cartSpeed * Time.deltaTime);
                        //// Assign velocity to clamp
                        //capZVelocity = payloadObject.GetComponent<Rigidbody>().velocity;
                        //capZVelocity.z = Mathf.Clamp(capZVelocity.z, -velocityCap, velocityCap);
                        //// Reassign Velocity
                        //payloadObject.GetComponent<Rigidbody>().velocity = capZVelocity;

                        //payloadObject.transform.position += payloadObject.transform.forward * cartSpeed * Time.deltaTime;

                        // new code
                        // Assign Velocity
                        //payloadRb.AddForce(payloadObject.transform.forward * cartSpeed * Time.deltaTime);
                        //capZVelocity = payloadRb.velocity;
                        //capZVelocity.z = Mathf.Clamp(capZVelocity.z, -velocityCap, velocityCap);
                        // Reassign Velocity
                        //payloadRb.velocity = capZVelocity;
                        var pos = payloadObject.transform.position;
                        pos.z += 1 * Time.deltaTime;
                        payloadObject.transform.position = pos;
                    }
                }
                else
                {
                    //payloadObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    payloadRb.velocity = Vector3.zero;
                }
            }
            else if (!playerInsideCart)
            {
                //payloadObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                payloadRb.velocity = Vector3.zero;
            }

        }

        // Debug Text
        //GameObject.Find("Text4").GetComponent<Text>().text = "" + payloadObject.GetComponent<Rigidbody>().velocity;
        // Dangerous
        playerInsideCart = false;

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform == player2.transform)
        {
            if (GameEventsPrototypeScript.Instance.Tutorial == 2)
            {
                tutorial_bool = true;
            }
            playerInsideCart = true;
            // Cart moves
        }

    }
}
