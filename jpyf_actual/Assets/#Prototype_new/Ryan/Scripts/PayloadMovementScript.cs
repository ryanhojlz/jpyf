﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PayloadMovementScript : MonoBehaviour
{

    // Singleton
    public static PayloadMovementScript Instance = null;
    private Transform payloadObject = null;
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
		
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void FixedUpdate()
    {
        if (playerInsideCart)
        {
            if (!tutorial_bool)
                return;
            // Move Payload
            if (Stats_ResourceScript.Instance.m_CartHP >= 50)
            {
                // Assign velocity
                payloadObject.GetComponent<Rigidbody>().AddForce(payloadObject.transform.forward * cartSpeed * Time.deltaTime);

                // Assign velocity to clamp
                capZVelocity = payloadObject.GetComponent<Rigidbody>().velocity;
                capZVelocity.z = Mathf.Clamp(capZVelocity.z, -velocityCap, velocityCap);
                // Reassign Velocity
                payloadObject.GetComponent<Rigidbody>().velocity = capZVelocity;

                //payloadObject.transform.position += payloadObject.transform.forward * cartSpeed * Time.deltaTime;
            }
        }
        else if (!playerInsideCart)
        {
            payloadObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        // Debug Text
        GameObject.Find("Text4").GetComponent<Text>().text = "" + payloadObject.GetComponent<Rigidbody>().velocity;
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
