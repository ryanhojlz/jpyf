using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumUpdateIndicatorsScript : MonoBehaviour
{
    public bool rightside = false;
    public DrumGameScript drumHandler = null;
    public Transform beatIndicator = null;
    // Use this for initialization
    void Start()
    {
        beatIndicator = GameObject.Find("BeatIndicator").transform;
        drumHandler = GameObject.Find("RepairSpot").GetComponent<DrumGameScript>();
    }

    //// Update is called once per frame
    //void Update ()
    //   {

    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == beatIndicator)
        {
            
            if (rightside)
            {
                drumHandler.b_insideRight = true;
            }
            else
            {
                drumHandler.b_insideLeft = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == beatIndicator)
        {
            
            if (rightside)
            {
                drumHandler.b_insideRight = false;
            }
            else
            {
                drumHandler.b_insideLeft = false;
            }
        }
    }
}
