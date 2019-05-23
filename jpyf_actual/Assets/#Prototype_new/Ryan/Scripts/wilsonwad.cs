using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class wilsonwad : MonoBehaviour
{
    // hi this ur script
    // Use this for initialization

    Image img = null;
    void Start ()
    {
        img = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log("Gawdawdwad" + GetComponent<Image>().color.a);

        //var y_really_y = this.GetComponent<Image>().color;
        //y_really_y.a = GameEventsPrototypeScript.Instance.ReturnAlpha();
        //GetComponent<Image>().color = y_really_y;


        var y_really_y = img.color;
        y_really_y.a = GameEventsPrototypeScript.Instance.ReturnAlpha();
        img.color = y_really_y;

    }
}
