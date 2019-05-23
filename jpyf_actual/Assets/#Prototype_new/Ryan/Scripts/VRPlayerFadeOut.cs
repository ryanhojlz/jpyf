using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerFadeOut : MonoBehaviour
{
    Material thismat = null;
	// Use this for initialization
	void Start ()
    {
        thismat = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update ()
    {
        var mat = thismat.color;
        mat.a = GameEventsPrototypeScript.Instance.ReturnAlpha();
        thismat.color = mat;
	}
}
