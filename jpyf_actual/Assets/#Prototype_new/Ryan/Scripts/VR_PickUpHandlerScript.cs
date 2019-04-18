using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_PickUpHandlerScript : MonoBehaviour
{
    public Stats_ResourceScript handler = null;
    
    // Use this for initialization
	void Start ()
    {
        handler = GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Pickup_Scripts>())
        {
            handler.ProcessPickUp(other.GetComponent<Pickup_Scripts>());
            Destroy(other.gameObject);
        }
    }
}
