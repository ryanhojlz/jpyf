using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FPS : MonoBehaviour
{
    float current = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        current = (int)(1f / Time.unscaledDeltaTime);
        this.GetComponent<Text>().text = " FPS?? " + current.ToString() ;
    }

  
}
