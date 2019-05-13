using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShrineScript : MonoBehaviour
{

    Tile_EventScript tile_event = null;
    
	// Use this for initialization
	void Start ()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        tile_event = transform.parent.GetComponent<Tile_EventScript>();

        //foreach (Transform obj in transform)
        //{
        //    if (obj.GetComponent<Collider>())
        //        Physics.IgnoreCollision(this.GetComponent<Collider>(), obj.GetComponent<Collider>());
        //}
	}

    // Update is called once per frame
    void Update()
    {
        // If event start 
        if (!tile_event.b_eventStart)
        {
            transform.GetChild(3).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Pickup_Scripts>())
        {
            if (other.GetComponent<Pickup_Scripts>().id == 5)
            {
                tile_event.UpShrineHunger(1);
                Destroy(other.gameObject);
                //Debug.Log("Run this wdawdawdawdawd");
            }
        }
    }

    
}
