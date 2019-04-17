using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Scripts : MonoBehaviour
{
    public int id = -1;
    
    // Use this for initialization
	void Start ()
    {
        switch (id)
        {
            case 1:
                transform.name = "pickup_spirit";
                this.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case 2:
                transform.name = "pickup_mineral";
                this.GetComponent<Renderer>().material.color = Color.red;
                break;
            default:
                transform.name = "???";
                break;
        }
	}
}
