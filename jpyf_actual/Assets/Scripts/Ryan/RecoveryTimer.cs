using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecoveryTimer : MonoBehaviour
{
    public GameObject player = null;
    //GameObject spirit = null;


	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player_object").GetComponent<ControllerPlayer>().CurrentUnit;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (player.GetComponent<NewPossesionScript>().isRecovering)
        {
            GetComponent<Text>().text = "Recovery: " + (int)player.GetComponent<NewPossesionScript>().recoveryTimer;
        }
        else
        {
            GetComponent<Text>().text = "CanPossess";
        }
	}
}
