using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UI_Healthbar : MonoBehaviour
{
    // Player
    GameObject player = null;
    //Unit
    GameObject unit = null;
    
    // Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player_object");
	}
	
	// Update is called once per frame
	void Update ()
    {
        unit = player.GetComponent<ControllerPlayer>().CurrentUnit;
        // if unit is a minion
        if (!unit.GetComponent<NewPossesionScript>())
        {
            if (!this.GetComponent<Image>().enabled)
                this.GetComponent<Image>().enabled = true;
            this.GetComponent<Image>().fillAmount = 1 * (unit.GetComponent<BasicGameOBJ>().healthValue / unit.GetComponent<BasicGameOBJ>().startHealthvalue);
        }
        else
        {
            this.GetComponent<Image>().enabled = false;
        }
        
	}
}
