using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialComplete : Achievement
{
    bool completed = false;
    // Update is called once per frame
    void Update()
    {
        if (completed && !hasDone)//&& TempIndex != index)
        {
            hasDone = true;
            Debug.Log(Achievement_name + " : " + Achievement_descriptions);
            StartCoroutine(TriggerAchievement());
        }

        Debug.Log(Achievement_name);
        //int TempIndex = PlayerPrefs.GetInt(Achievement_name);
        //Debug.Log(GameObject.Find("Stats").GetComponent<Statistics>().GetWins());
        if (!GameObject.Find("StatsManager"))
            return;
        if (!GameObject.Find("StatsManager").GetComponent<Statistics>())
            return;

        if ((GameObject.Find("StatsManager").GetComponent<Statistics>().GetCompletedTutorial() && !hasDone))//&& TempIndex != index)
        {
            hasDone = true;
            Debug.Log(Achievement_name + " : " + Achievement_descriptions);
            StartCoroutine(TriggerAchievement());
        }
    }
}
