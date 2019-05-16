using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialComplete : Achievement
{
    bool completed = false;

    void Update()
    {
        if (completed && !hasDone)
        {
            hasDone = true;
            Debug.Log(Achievement_name + " : " + Achievement_descriptions);
            StartCoroutine(TriggerAchievement());
        }

        Debug.Log(Achievement_name);

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
