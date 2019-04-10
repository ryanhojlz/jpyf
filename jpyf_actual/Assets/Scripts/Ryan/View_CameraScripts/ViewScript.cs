using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_PS4
using UnityEngine.PS4.VR;
using UnityEngine.PS4;
#endif

public class ViewScript : MonoBehaviour
{
    public Camera spectator_cam;
    public Camera player_cam;
    public bool debugbool1, debugbool2;
    //int numdisplay;
    // Use this for initialization

    private void Awake()
    {

        spectator_cam = GameObject.Find("_FollowCam").GetComponent<Camera>();
        player_cam = GameObject.Find("Camera_player").GetComponent<Camera>();
        UnityEngine.XR.XRSettings.eyeTextureResolutionScale = 0.9f;
    }

    void Start()
    {
       
        UnityEngine.XR.XRSettings.showDeviceView = false;
    }



    public void SetDeviceView(bool device_view)
    {
        UnityEngine.XR.XRSettings.showDeviceView = device_view;
    }
}
