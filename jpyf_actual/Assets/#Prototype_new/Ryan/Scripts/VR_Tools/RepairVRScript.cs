using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairVRScript : MonoBehaviour {

    public float repairPower = 10;
    public GameObject repairSpot = null;
    public GameObject lanternSpot = null;
    // Resource Handler
    public Stats_ResourceScript handler;
    // repair ticks
    int repairTick = 0;

	// Use this for initialization
	void Start ()
    {
        repairSpot = GameObject.Find("RepairSpot");
        handler = GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>();
        lanternSpot = GameObject.Find("LanternSpot");
    }

    // Update is called once per frame
    void Update()
    {
        // dis debug stuff
        if (Input.GetKeyDown(KeyCode.U))
        {
            handler.m_CartHP += 5;
        }

        if (repairTick >= 3)
        {
            //handler.m_Minerals -= 10;
            handler.m_CartHP += 2;
            //handler.Cart_TakeDmg(-5);
            if (handler.m_CartHP > handler.m_CartHpCap)
            {
                handler.m_CartHP = handler.m_CartHpCap;
            }
            repairTick = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == repairSpot)
        {
            if (handler.m_Minerals > 5)
            {
                ++repairTick;
                handler.m_CartHP += 3;
                --handler.m_Minerals;
                //handler.m_CartHP += 10;
            }
        }

        //if (other.gameObject == lanternSpot)
        //{
        //    handler.Lantern_TakeDmg(-2);
        //    --handler.m_Souls;
        //}


    }
}
