using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticDisplay : MonoBehaviour
{
    public Text numberWin;
    public Text numberLose;
    public Text totalPlayTime;
    public Text dateTime;
    public Text enemiesKilled;
    public Text itemsCollected;
    public int Hour;
    public int Min;

    // Use this for initialization
    private void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        numberWin.text = "" + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.number_win;
        numberLose.text = "" + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.number_lose;
        dateTime.text = "" + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.dateTime;
        totalPlayTime.text = "" + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.Hour + " hr : " + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.Min + " min";
        enemiesKilled.text = "" + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.number_of_enemies_killed;
        itemsCollected.text = "" + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.number_of_times_items_gathered;
    }
}
