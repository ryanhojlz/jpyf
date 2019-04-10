using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Bar_Facing_Player : MonoBehaviour
{
    GameObject cameraPointer = null;
    // Start is called before the first frame update
    void Start()
    {
        cameraPointer = GameObject.Find("_FollowCam");
    }

    // Update is called once per frame
    void Update()
    {
        //foreach (Camera c in Camera.allCameras)
        //{
        //    if(c.name == "spec_cam")
        //    this.transform.LookAt(transform.position + c.transform.rotation * Vector3.back, c.transform.rotation * Vector3.up);
        //}
        this.transform.LookAt(transform.position + cameraPointer.transform.rotation * Vector3.back, cameraPointer.transform.rotation * Vector3.up);
    }
}
