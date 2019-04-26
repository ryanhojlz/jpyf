using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritPickupScript : GrabbableObject
{
    public Stats_ResourceScript handler = null;
    public Transform lanternSpot = null;
    public bool grabbedOnce = false;
    

    private void Start()
    {
        handler = GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>();
        lanternSpot = GameObject.Find("LanternSpot").transform;

    }

    public override void OnGrab(MoveController currentController)
    {
        base.OnGrab(currentController);
        grabbedOnce = true;
        this.transform.parent = null;
        this.GetComponent<Rigidbody>().isKinematic = false;
    }

    public override void OnGrabReleased(MoveController currentController)
    {
        base.OnGrabReleased(currentController);
    }

    private void OnTriggerEnter(Collider other)
    {
        // When this object collides with lantern object 
        if (other.transform == lanternSpot)
        {
            handler.Lantern_TakeDmg(-25);
            //handler.ConsumeSouls(10);
            Destroy(this.gameObject);
        }
    }

    

    
}
