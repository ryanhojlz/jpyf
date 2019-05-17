using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offset : MonoBehaviour {
    public static Offset Instance = null;
    GameObject offset = null;
	// Use this for initialization

	void Start ()
    {
       //offset = GameObject.Find("Position").gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //this.transform.position = offset.transform.position;

        //Debug.Log(offset.transform.eulerAngles);
#if UNITY_PS4
        if (PS4_ControllerScript.Instance.ReturnL1Press())
        {
            var rotate = this.transform.eulerAngles;
            rotate.y += 20 * Time.deltaTime;
            transform.eulerAngles = rotate;
        }
#endif

    }

}
