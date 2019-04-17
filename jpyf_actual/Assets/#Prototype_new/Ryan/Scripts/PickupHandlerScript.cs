using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandlerScript : MonoBehaviour
{
    // This handler is used to prevent picking up multiple objects
    public Object_ControlScript objHandler = null;
    public Transform nearest_pickup_object = null;
    public Transform currentObject = null;
    Vector3 offset = new Vector3(0, 1.5f, 0);
    Vector3 throwDirection = Vector3.zero;


	// Use this for initialization
	void Start ()
    {
        objHandler = GameObject.Find("PS4_ObjectHandler").GetComponent<Object_ControlScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // If picking up
		if (objHandler.pickup)
        {
            // Nearest Pickup object
            if (!nearest_pickup_object)
                return;
            if (currentObject)
            {
                // Drop the current object
                currentObject.localPosition = Vector3.zero;
                currentObject.parent = null;
                currentObject.GetComponent<Rigidbody>().isKinematic = false;

            }
            // Nearest object gone // ReAssign
            currentObject = nearest_pickup_object;
            nearest_pickup_object = null;
            // Parent cos picking up**
            currentObject.parent = this.transform;
            currentObject.transform.localPosition = offset;
            currentObject.GetComponent<Rigidbody>().isKinematic = true;
            currentObject.localEulerAngles = Vector3.zero;

        }

        if (objHandler.throw_item)
        {
            if (currentObject)
            {
                // Drop the current object
                //currentObject.localPosition = Vector3.zero;
                currentObject.parent = null;
                currentObject.GetComponent<Rigidbody>().isKinematic = false;
                throwDirection = this.transform.forward;
                throwDirection.y = 15;
                throwDirection.z *= 35;
                currentObject.GetComponent<Rigidbody>().AddForce(throwDirection * 1000);
            }
        }
        // Yabai code
        nearest_pickup_object = null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Pickup_Scripts>())
        {
            nearest_pickup_object = other.transform;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    // If its the only object i can pickup
    //    if (other.transform == nearest_pickup_object.transform)
    //    {
    //        nearest_pickup_object = null;
    //    }
    //}
}
