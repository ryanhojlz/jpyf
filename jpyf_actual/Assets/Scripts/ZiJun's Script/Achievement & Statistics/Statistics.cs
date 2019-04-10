using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    //private class StatisticsData
    //{
    //    public int number_win;
    //    public int number_lose;
    //}
    //public Text numberWin;
    //public Text numberLose;
    //int number_win;
    //int number_lose;

    public Text numberWin;
    public Text numberLose;

    public struct StatisticsData
    {
        public int number_win;
        public int number_lose;
    }

    public StatisticsData Data = new StatisticsData();

    // Use this for initialization
    void Start()
    {
        //loading value
        DontDestroyOnLoad(transform.gameObject);

        //StatisticsData statisticsData = new StatisticsData();
        //Data.number_win = 50;
        //Data.number_lose = 10;
        ////string json = JsonUtility.ToJson(statisticsData);

        //string json = JsonUtility.ToJson(Data);
        //File.WriteAllText(Application.dataPath + "saveFile.json", json);
        string json = File.ReadAllText(Application.dataPath + "saveFile.json");

        StatisticsData loadedStatisticsData = JsonUtility.FromJson<StatisticsData>(json);
        //Debug.Log(loadedStatisticsData.number_win);
        //Debug.Log(loadedStatisticsData.number_lose);

        Data = loadedStatisticsData;

        Debug.Log(Data.number_win);
    }

    // Update is called once per frame
    void Update()
    {
        //updating value
        numberWin.text = "" + Data.number_win;
        numberLose.text = "" + Data.number_lose;
    }

    public void SaveStats()
    {
        //Saving stats
    }

    public void LoadStats()
    {
        //Loading stats
    }

    public int GetWins()
    {
        return Data.number_win;
    }

    public int GetLoses()
    {
        return Data.number_lose;
    }

    public void incrementWin()
    {
        Data.number_win++;
    }

    public void incrementLose()
    {
        Data.number_lose++;
    }

    public void SetWin(int value)
    {
        Data.number_win = value;
    }

    public void SetLose(int value)
    {
        Data.number_lose = value;
    }
}
