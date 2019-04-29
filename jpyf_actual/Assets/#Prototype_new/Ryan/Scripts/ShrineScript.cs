using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineScript : MonoBehaviour
{

    Tile_EventScript tile_event = null;
	// Use this for initialization
	void Start ()
    {
        tile_event = transform.parent.GetComponent<Tile_EventScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Pickup_Scripts>())
        {
            if (other.GetComponent<Pickup_Scripts>().id == 1)
            {
                tile_event.UpShrineHunger(1);
                Destroy(other.gameObject);
                //Debug.Log("Run this wdawdawdawdawd");
            }
        }
    }
}
