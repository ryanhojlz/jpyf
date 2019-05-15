using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRCam_Ignore : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        UnityEngine.XR.XRSettings.showDeviceView = false;
        UnityEngine.XR.XRSettings.eyeTextureResolutionScale = 0; 
        XRSettings.enabled = true;

    }

    // Update is called once per frame
    void Update ()
    {
       
        //transform.rotation = Quaternion.identity;
	}
}
