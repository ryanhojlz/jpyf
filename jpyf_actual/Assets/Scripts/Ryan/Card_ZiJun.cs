using System.Collections;
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
        if (playerReference.GetComponent<PlayerScript>().Mana == 0)
            return;
        if (playerReference.GetComponent<PlayerScript>().Mana < this.ManaCost)
            return;

        base.OnGrab(currentController);
        playerReference.GetComponent<PlayerScript>().Mana -= 1;
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
        if (collision.gameObject.name == "enivronment_test 1")
        {
            //playerReference.GetComponent<PlayerScript>().Mana -= this.ManaCost;
            onfloor = true;
        }
    }
}
