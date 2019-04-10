using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinAchievement : Achievement
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //int TempIndex = PlayerPrefs.GetInt(Achievement_name);
        if (GameObject.Find("Stats").GetComponent<Statistics>().GetWins() >= 1 && !hasDone)//&& TempIndex != index)
        {
            hasDone = true;
            Debug.Log(Achievement_name + " : " + Achievement_descriptions);
            StartCoroutine(TriggerAchievement());
        }
	}
}
