using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disablecanvas : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GameObject.Find("Canvas_P2").GetComponent<Canvas>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
