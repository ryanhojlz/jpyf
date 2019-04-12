using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_PS4
using UnityEngine.PS4;
using UnityEngine.XR;
using System;
#endif
public class PCDEBUGG : MonoBehaviour
{
    public GameObject[] ally_waypoints;
    public GameObject[] enemy_waypoints;
    public GameObject[] units;
    bool renderdis = false;
    
    // Use this for initialization
	void Start ()
    {
        ally_waypoints = GameObject.FindGameObjectsWithTag("Ally_Waypoint");
        enemy_waypoints = GameObject.FindGameObjectsWithTag("Enemy_Waypoint");
        UpdateRender(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
#if UNITY_PS4
        if (Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), "Joystick" + 1 + "Button9", true)))
        {
            if (renderdis)
                renderdis = false;
            else if (!renderdis)
                renderdis = true;

            UpdateRender(renderdis);
        }
#endif
    }

    void UpdateRender(bool render)
    {
        for (int i = 0; i < ally_waypoints.Length; ++i)
        {
            ally_waypoints[i].GetComponent<Renderer>().enabled = render;
        }
        for (int i = 0; i < enemy_waypoints.Length; ++i)
        {
            enemy_waypoints[i].GetComponent<Renderer>().enabled = render;
        }
        //units = GameObject.FindGameObjectsWithTag("Ally_Unit");
        //for (int i = 0; i < units.Length; ++i)
        //{
        //    if (units[i].GetComponent<Minion>())
        //        units[i].GetComponent<MeshRenderer>().enabled = render;
        //}
    }
}
