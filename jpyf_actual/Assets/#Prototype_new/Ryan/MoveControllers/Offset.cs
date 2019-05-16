using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offset : MonoBehaviour {

    GameObject offset = null;
	// Use this for initialization
	void Start ()
    {
        offset = GameObject.Find("PosReference").gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = offset.transform.position; 
	}
}
