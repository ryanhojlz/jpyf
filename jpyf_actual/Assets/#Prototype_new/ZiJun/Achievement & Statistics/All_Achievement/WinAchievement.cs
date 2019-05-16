using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinAchievement : Achievement
{
    public int numberofwins = 0;
    // Use this for initialization

	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(Achievement_name);
        //int TempIndex = PlayerPrefs.GetInt(Achievement_name);
        //Debug.Log(GameObject.Find("Stats").GetComponent<Statistics>().GetWins());
        if (!GameObject.Find("StatsManager"))
            return;
        if (!GameObject.Find("StatsManager").GetComponent<Statistics>())
            return;

        if (GameObject.Find("StatsManager").GetComponent<Statistics>().GetWins() >= numberofwins && !hasDone)//&& TempIndex != index)
        {
            hasDone = true;
            Debug.Log(Achievement_name + " : " + Achievement_descriptions);
            StartCoroutine(TriggerAchievement());
        }
	}
}
