using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayerCameraScript : MonoBehaviour
{
    public GameObject player = null;
    public float cameraOffset = 10.0f;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("NewTestControllableUnit");
	}
	
	// Update is called once per frame
	void Update ()
    {
        var pos = player.transform.position;
        pos.y += cameraOffset;
        transform.position = pos;
    }
}
