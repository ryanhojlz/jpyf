using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinAchievement : Achievement
{
    public int numberofwins = 0;

	void Update ()
    {
        if (!GameObject.Find("StatsManager"))
            return;
        if (!GameObject.Find("StatsManager").GetComponent<Statistics>())
            return;

        if (GameObject.Find("StatsManager").GetComponent<Statistics>().GetWins() >= numberofwins && !hasDone)//&& TempIndex != index)
        {
            hasDone = true;
            //Debug.Log(Achievement_name + " : " + Achievement_descriptions);
            StartCoroutine(TriggerAchievement());
        }
	}
}
