using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Card_ZiJun : GrabbableObject
{
    public bool onfloor = false;
    public bool pickedup = false;
    float summonTimer = 2.0f;
    public GameObject summonedUnit;
    

    public override void OnGrab(MoveController currentController)
    {
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
            onfloor = true;
        }
    }
}
