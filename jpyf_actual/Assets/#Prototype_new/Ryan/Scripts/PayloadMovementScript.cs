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
    public bool isRotating = false;

    //what am i doing
    public bool cannotMove = false;

    // bool movesideways
    public bool moveSideways = false;
    public bool moveRight = false;

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
        if (Endgamestats.Instance)
        {
            Endgamestats.Instance.SetStartPos(payloadObject.transform.position);
        }

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
                else if (isRotating)
                    return;
                else if (cannotMove)
                    return;

                
                if (!GameEventsPrototypeScript.Instance.TileEvent_Start)
                {
                    // Move Payload
                    if (Stats_ResourceScript.Instance.m_CartHP >= 25)
                    {
                        if (!moveSideways)
                        {
                            var pos = payloadObject.transform.position;
                            pos += payloadObject.transform.forward * Time.deltaTime;
                            payloadObject.transform.position = pos;

                        }                        
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
