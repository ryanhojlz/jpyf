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
	void Update ()
    {

#if UNITY_PS4
        //if (PS4_ControllerScript.Instance.ReturnAnalogLDown())
        //{
        //    var pos = this.transform.position;
        //    pos.y += 1 * Time.deltaTime;
        //    this.transform.position = pos;
        //}
#endif
    }
}
