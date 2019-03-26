using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCast : MonoBehaviour
{

    RaycastHit raycastHit;
    Ray ray;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Physics.Raycast(this.transform.position, this.transform.forward, out raycastHit, 100);
        if (raycastHit.transform == null)
            return;
        if (raycastHit.transform.name == "demo_ground")
        {
            raycastHit.transform.GetComponent<Renderer>().material.color = Color.black;
        }
    }
}
