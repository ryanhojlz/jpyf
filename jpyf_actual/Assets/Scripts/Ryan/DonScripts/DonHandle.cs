using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonHandle : GrabbableObject
{
    Vector3 originalPos;
    Quaternion originalRotation;

    private void Start()
    {
        originalPos = this.transform.position;
        originalRotation = this.transform.rotation;
    }

    public void ResetPos()
    {
        this.transform.position = originalPos;
        this.transform.rotation = originalRotation;
        
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    public override void OnGrab(MoveController currentController)
    {
        base.OnGrab(currentController);
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
    

}
