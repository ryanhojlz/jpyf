using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Card_ZiJun : GrabbableObject
{
    // Theres alot of old code because the old way of summoning didnt really feel great

    public bool onfloor = false;
    public bool pickedup = false;
    public bool lookedAt = false;
    public bool isthrown = false;

    public bool summon = false;
    
    public int ManaCost = 0;
    public float summonTimer = 2.0f;
    public GameObject summonedUnit = null;
    private Shader Mat = null;

    private void Awake()
    {
        Mat = GetComponent<Renderer>().material.shader;
    }

    public override void OnGrab(MoveController currentController)
    {
        GameObject playerReference = GameObject.Find("Player");
        // if current mana is lesser than this mana
        if (playerReference.GetComponent<PlayerScript>().Mana < this.ManaCost)
            return;

        base.OnGrab(currentController);
        pickedup = true;
    }

    public override void OnGrabReleased(MoveController currentController)
    {
        base.OnGrabReleased(currentController);
        if (pickedup)
            isthrown = true;
    }

    // Update is called once per frame
    void Update()
    {
        //float[] debugArray = new float[4];
        //debugArray = transform.GetComponent<Renderer>().material.GetFloatArray("_OutlineColor");
        //Debug.Log("ArrayColor  " + debugArray);

        // Deattach from yugioh hand thing
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


        //if (onfloor)
        //{
        //    summonTimer -= 1.0f * Time.deltaTime;

        //    var tempcolor = this.GetComponent<MeshRenderer>().material.color;
        //    tempcolor.r = 255;
        //    tempcolor.g = 0;
        //    tempcolor.b = 255;
        //    this.GetComponent<MeshRenderer>().material.color = tempcolor;
        //}
        //if (isthrown)
        //{
        //    if (transform.position.z > -17)
        //    {
        //        summonTimer = 0;
        //    }
        //}
        //if (summonTimer <= 0)
        //{
        //    GameObject go = Instantiate(summonedUnit) as GameObject;
        //    //Vector3 spawnPoint = new Vector3(250, 7, -55);
        //    Vector3 spawnPoint = this.transform.position;

        //    if (this.transform.position.x < -3)
        //    {
        //        // Left
        //        spawnPoint.x = GameObject.Find("SpawnReference3").transform.position.x;
        //        spawnPoint.z = GameObject.Find("SpawnReference3").transform.position.z;

        //    }
        //    else if (this.transform.position.x < 3)
        //    {
        //        // Middle
        //        spawnPoint.x = GameObject.Find("SpawnReference2").transform.position.x;
        //        spawnPoint.z = GameObject.Find("SpawnReference2").transform.position.z;
        //    }
        //    else
        //    {
        //        // Right
        //        spawnPoint.x = GameObject.Find("SpawnReference1").transform.position.x;
        //        spawnPoint.z = GameObject.Find("SpawnReference1").transform.position.z;

        //    }

        //    go.GetComponent<NavMeshAgent>().Warp(spawnPoint);
        //    Destroy(gameObject);
        //}

        if (isthrown)
        {

            if (transform.position.z > -17)
            {
                var pos = transform.position;
                pos.y = -4.5f;
                transform.position = pos;
                summon = true;
            }
            
        }
        

        if (summon)
        {
            //Vector3 spawnPoint = new Vector3(250, 7, -55);
            Vector3 spawnPoint = Vector3.zero;
            spawnPoint.y = 1;
           

            if (this.transform.position.x < -0.8f)
            {
                // Left
                spawnPoint.x = GameObject.Find("SpawnReference3").transform.position.x;
                spawnPoint.z = GameObject.Find("SpawnReference3").transform.position.z;

            }
            else if (this.transform.position.x < 0.8)
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

            Ray CastToGround = new Ray(spawnPoint, Vector3.down);
            RaycastHit hit;
            Physics.Raycast(CastToGround, out hit);

            GameObject go = Instantiate(summonedUnit,hit.point, summonedUnit.transform.rotation) as GameObject;
            go.GetComponent<TestUnit_Control>().waypointIndex = 0;

            //go.GetComponent<NavMeshAgent>().Warp(spawnPoint);
            Destroy(gameObject);
        }


    }
    
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.name == "demo_ground")
        //{
        //    onfloor = true;
        //}
        //if (collision.gameObject.name == "enivronment_test_1")
        //{
        //    // Return to hand 
        //    if (this.transform.position.y > 0)
        //    {
        //        GameObject.Find("armthing").GetComponent<CardScript>().ReturnToHand(this.gameObject);
        //        pickedup = false;
        //    }
        //    else
        //    {
        //        //playerReference.GetComponent<PlayerScript>().Mana -= this.ManaCost;
        //        onfloor = true;
        //        GameObject playerReference = GameObject.Find("Player");
        //        playerReference.GetComponent<PlayerScript>().Mana -= 1;
        //    }
        //}

        if (collision.gameObject.name == "enviroment_test1")
        {
            if (isthrown)
            {
                if (this.transform.position.y > 0)
                {
                    GameObject.Find("armthing").GetComponent<CardScript>().ReturnToHand(this.gameObject);
                    pickedup = false;
                    this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    this.gameObject.GetComponent<Rigidbody>().useGravity = false;
                }
            }
        }
    }


}
