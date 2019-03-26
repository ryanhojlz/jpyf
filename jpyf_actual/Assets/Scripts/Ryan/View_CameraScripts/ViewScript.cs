using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.PS4.VR;
using UnityEngine.PS4;

public class ViewScript : MonoBehaviour
{
    public Camera spectator_cam;
    public Camera player_cam;
    public bool debugbool1, debugbool2;
    //int numdisplay;
    // Use this for initialization
    
    private void Awake()
    {
        {
            //spectator_cam = GameObject.Find("spec_cam").GetComponent<Camera>();
            //player_cam = GameObject.Find("Camera_player").GetComponent<Camera>();


            //UnityEngine.PS4.RenderSettings.
            //UnityEngine.XR.XRSettings.enabled = false;

            spectator_cam = GameObject.Find("spec_cam").GetComponent<Camera>();
            player_cam = GameObject.Find("Camera_player").GetComponent<Camera>();
            //UnityEngine.XR.XRSettings.eyeTextureResolutionScale = 1.1f;

            //float width = Screen.width;
            //float height = Screen.height;

            //Display.displays[1].Activate((int)1920 ,(int)1080,120);

            //player_cam.depth = -1;
            //spectator_cam.depth = 0;
            //Display.displays[1].Activate();
            //player_cam.targetDisplay = 1;
            //spectator_cam.targetDisplay = 0;


            //    camera_1.enabled = true;
            //    UnityEngine.XR.XRSettings.showDeviceView = false;


        }
    }

    void Start()
    {
        {
            //numdisplay = UnityEngine.Display.displays.Length;

            //    camera_1.enabled = false;
            //    //camera_1.gameObject.SetActive(false);
            //var xrSettings = UnityEngine.XR.XRSettings.showDeviceView;
            //xrSettings = false;
            //UnityEngine.XR.XRSettings.showDeviceView = xrSettings;
            //
            //float width = Screen.width;
            //float height = Screen.height;
            //Display.displays[1].Activate((int)width,(int)height,120);
            //UnityEngine.XR.XRSettings.eyeTextureResolutionScale = 0.1f;

        }
        UnityEngine.XR.XRSettings.showDeviceView = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Debug.Log("TEEEEEEEEEEEEEEEEEEEEEEEEEEEEEST");
        //Debug.Log("Num displays " + numdisplay);
    }

    private void FixedUpdate()
    {
        //for (int i = 0; i < 8; ++i)
        //{
        //    string fla = "indexu of " + i + " ____ is it active   ";
        //    Debug.Log(fla + UnityEngine.Display.displays[i].active);
        //}
        //Debug.Log("Supported Devices are            " + UnityEngine.XR.XRSettings.supportedDevices);
        //Debug.Log("Is this active   " + Display.displays[1].active);
    }

    public void SetDeviceView(bool device_view)
    {
        UnityEngine.XR.XRSettings.showDeviceView = device_view;
    }
}
