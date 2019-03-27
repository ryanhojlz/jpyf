﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Card_ZiJun : GrabbableObject
{
    public bool onfloor = false;
    public bool pickedup = false;
    public bool lookedAt = false;
    
    public int ManaCost = 0;
    float summonTimer = 2.0f;
    public GameObject summonedUnit;
    private Shader Mat;

    private void Awake()
    {
        Mat = GetComponent<Renderer>().material.shader;
    }

    public override void OnGrab(MoveController currentController)
    {
        GameObject playerReference = GameObject.Find("Player");
        //if (playerReference.GetComponent<PlayerScript>().Mana == 0)
        //    return;
        //if (playerReference.GetComponent<PlayerScript>().Mana < this.ManaCost)
        //    return;

        base.OnGrab(currentController);
        pickedup = true;
    }

    public override void OnGrabReleased(MoveController currentController)
    {
        base.OnGrabReleased(currentController);
        
    }

    // Update is called once per frame
    void Update()
    {
        float[] debugArray = new float[4];
        debugArray = transform.GetComponent<Renderer>().material.GetFloatArray("_OutlineColor");
        Debug.Log("ArrayColor  " + debugArray);
        if (pickedup)
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        //else
        //{
        //    this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //    this.gameObject.GetComponent<Rigidbody>().useGravity = false;
        //}

        if (onfloor)
        {
            summonTimer -= 1.0f * Time.deltaTime;

            var tempcolor = this.GetComponent<MeshRenderer>().material.color;
            tempcolor.r = 255;
            tempcolor.g = 0;
            tempcolor.b = 255;
            this.GetComponent<MeshRenderer>().material.color = tempcolor;
        }
        if (summonTimer < 0)
        {
            GameObject go = Instantiate(summonedUnit) as GameObject;
            //Vector3 spawnPoint = new Vector3(250, 7, -55);
            Vector3 spawnPoint = this.transform.position;

            if (this.transform.position.x < -3)
            {
                // Left
                spawnPoint.x = GameObject.Find("SpawnReference3").transform.position.x;
                spawnPoint.z = GameObject.Find("SpawnReference3").transform.position.z;

            }
            else if (this.transform.position.x < 3)
            {
                // Middle
                spawnPoint.x = GameObject.Find("SpawnReference2").transform.position.x;
                spawnPoint.z = GameObject.Find("SpawnReference2").transform.position.z;
            }
            else
            {
                // Right
                spawnPoint.x = GameObject.Find("SpawnReference1").transform.position.x;
                spawnPoint.z = GameObject.Find("SpawnReference1").transform.position.z;

            }

            go.GetComponent<NavMeshAgent>().Warp(spawnPoint);
            Destroy(gameObject);
        }
        
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.name == "demo_ground")
        //{
        //    onfloor = true;
        //}
        if (collision.gameObject.name == "enivronment_test_1")
        {
            if (this.transform.position.y > 0)
            {
                GameObject.Find("armthing").GetComponent<CardScript>().ReturnToHand(this.gameObject);
                pickedup = false;
            }
            else
            {
                //playerReference.GetComponent<PlayerScript>().Mana -= this.ManaCost;
                onfloor = true;
                GameObject playerReference = GameObject.Find("Player");
                playerReference.GetComponent<PlayerScript>().Mana -= 1;
            }
        }
    }
}