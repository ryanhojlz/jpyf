using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticDisplay : MonoBehaviour
{
    public Text numberWin;
    public Text numberLose;
    public Text totalPlayTime;
    public int Hour;
    public int Min;

    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        numberWin.text = "" + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.number_win;
        numberLose.text = "" + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.number_lose;

        totalPlayTime.text = "" + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.Hour + " hr : " + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.Min + " min";
    }
}
