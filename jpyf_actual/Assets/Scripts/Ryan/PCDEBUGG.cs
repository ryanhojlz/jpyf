using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCDEBUGG : MonoBehaviour
{
    public GameObject wadoiawoid;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Dont use unity things please fk la");
            GameObject go = Instantiate(wadoiawoid) as GameObject;
            go.gameObject.GetComponent<Transform>().position = new Vector3(250, 7, -55);

        }
	}
}
