using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticDisplay : MonoBehaviour
{
    public Text numberWin;
    public Text numberLose;
    
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        numberWin.text = "" + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.number_win;
        numberLose.text = "" + GameObject.Find("StatsManager").GetComponent<Statistics>().Data.number_lose;
    }
}
