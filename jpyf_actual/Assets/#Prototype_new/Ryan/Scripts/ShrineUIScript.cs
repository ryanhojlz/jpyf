using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShrineUIScript : MonoBehaviour
{
    
    public Image gauge = null;
    // Acutal parent
    public Tile_EventScript _parent = null;
    
    // Use this for initialization
	void Start ()
    {
        _parent = transform.parent.parent.parent.GetComponent<Tile_EventScript>();
        gauge = transform.GetChild(0).transform.GetChild(1).GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_parent.b_eventStart)
        {
            transform.GetChild(0).gameObject.SetActive(true);        
            gauge.fillAmount = 1 * ((float)_parent.i_shrineHungerMeter / (float)_parent.shrineHungerCap);           
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
