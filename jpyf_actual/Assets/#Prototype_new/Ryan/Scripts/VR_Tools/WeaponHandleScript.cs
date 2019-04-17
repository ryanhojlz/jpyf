using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandleScript : GrabbableObject
{

    //// Use this for initialization
    //void Start ()
    //   {

    //}

    //// Update is called once per frame
    //void Update ()
    //   {

    //}

    public override void OnGrab(MoveController currentController)
    {
        base.OnGrab(currentController);
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //this.transform.rotation = currentController.transform.rotation;
    }

    // Can prolly do coroutine on grab release but brute forcing it for now
    //private void Update()
    //{
        
    //}
}
