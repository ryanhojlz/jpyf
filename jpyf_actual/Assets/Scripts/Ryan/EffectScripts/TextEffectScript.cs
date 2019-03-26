using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEffectScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Using color as lifetime // using alpha channel
        var _alpha = this.gameObject.GetComponent<TextMesh>().color;
        var _pos = this.gameObject.transform.position;
        if (_alpha.a <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _alpha.a -= 3 * Time.deltaTime;
            _pos.y += 3 * Time.deltaTime;
            this.gameObject.GetComponent<TextMesh>().color = _alpha;
            this.gameObject.transform.position = _pos;
        }
    }
}
