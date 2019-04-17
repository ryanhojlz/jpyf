using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairVRScript : MonoBehaviour {

    public float repairPower = 10;
    public GameObject repairSpot = null;
    // Resource Handler
    public Stats_ResourceScript handler;
    // repair ticks
    int repairTick = 0;

	// Use this for initialization
	void Start ()
    {
        repairSpot = GameObject.Find("RepairSpot");
        handler = GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // dis debug stuff
        if (Input.GetKeyDown(KeyCode.U))
        {
            handler.m_CartHP += 10;
        }

        if (repairTick >= 3)
        {
            //handler.m_Minerals -= 10;
            handler.m_CartHP += 10;
            repairTick = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == repairSpot)
        {
            ++repairTick;
            handler.m_Minerals -= 5;
            //handler.m_CartHP += 10;
        }


    }
}
