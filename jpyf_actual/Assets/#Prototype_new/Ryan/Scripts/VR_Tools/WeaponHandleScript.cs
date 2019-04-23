using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandleScript : GrabbableObject
{
    public bool b_ToolReleased = false;
    Vector3 localResetPos = Vector3.zero;
    Vector3 originalLocalAngles = Vector3.zero;
    public float timerToReset;
    Vector3 ocd = new Vector3(0, 0, 90);
    // Use this for initialization
    void Start()
    {
        localResetPos = this.transform.localPosition;
        originalLocalAngles = this.transform.eulerAngles;
    }

    private void Update()
    {
        if (b_ToolReleased)
        {
            timerToReset += 1 * Time.deltaTime;
        }
        if (timerToReset >= 3.5f)
        {
            timerToReset = 0;
            this.transform.localPosition = localResetPos;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            this.transform.eulerAngles= originalLocalAngles;
            b_ToolReleased = false;
        }
    }

    public override void OnGrab(MoveController currentController)
    {
        base.OnGrab(currentController);
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    public override void OnGrabReleased(MoveController currentController)
    {
        base.OnGrabReleased(currentController);
        b_ToolReleased = true;
        timerToReset = 0;
    }


    // Can prolly do coroutine on grab release but brute forcing it for now
    //private void Update()
    //{

    //}
}
