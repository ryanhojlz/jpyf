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
    }
}
