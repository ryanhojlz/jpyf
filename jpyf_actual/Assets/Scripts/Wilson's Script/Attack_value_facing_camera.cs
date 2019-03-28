using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_value_facing_camera : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Camera c in Camera.allCameras)
        {
            if (c.name == "spec_cam")
                this.transform.LookAt(transform.position + c.transform.rotation * Vector3.back, c.transform.rotation * Vector3.up);
        }
    }
}
