using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Bar_Facing_Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Camera c in Camera.allCameras)
        {
            if(c.name == "spec_cam")
            this.transform.LookAt(transform.position + c.transform.rotation * Vector3.back, c.transform.rotation * Vector3.up);
        }
    }
}
