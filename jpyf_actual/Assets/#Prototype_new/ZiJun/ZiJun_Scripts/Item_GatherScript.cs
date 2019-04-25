using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Item_GatherScript : MonoBehaviour
{
    Image GatherUI = null;
	// Use this for initialization
	void Start ()
    {
        GatherUI = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Image>(); 	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
