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

    //public Text numberWin;
    //public Text numberLose;
    //public Text dateTime;
    public struct StatisticsData
    {
        public int number_win;
        public int number_lose;
        public float playTime;

        public int Hour;
        public int Min;
        public float Sec;
    }

    public StatisticsData Data = new StatisticsData();

    // Use this for initialization
    void Start()
    {
        //loading value
        DontDestroyOnLoad(transform.gameObject);
        Data.playTime = 0f;

        Data.Hour = 0;
        Data.Min = 0;
        Data.Sec = 0f;
    //StatisticsData statisticsData = new StatisticsData();
    //Data.number_win = 50;
    //Data.number_lose = 10;
    ////string json = JsonUtility.ToJson(statisticsData);

    //string json = JsonUtility.ToJson(Data);
    //File.WriteAllText(Application.dataPath + "saveFile.json", json);
    //string json = File.ReadAllText(Application.dataPath + "/saveFile.json");

    //StatisticsData loadedStatisticsData = JsonUtility.FromJson<StatisticsData>(json);
    //Debug.Log(loadedStatisticsData.number_win);
    //Debug.Log(loadedStatisticsData.number_lose);

    LoadStats();

        Debug.Log(Data.number_win);
    }

    // Update is called once per frame
    void Update()
    {
        //updating value
        //numberWin.text = "" + Data.number_win;
        //numberLose.text = "" + Data.number_lose;
        //dateTime.text = System.DateTime.Now.ToString();
        //Debug.Log(dateTime.text);
        Data.Sec += Time.deltaTime;

        if (Data.Sec > 59)
        {
            Data.Min += 1;
            Data.Sec = 0;
        }
        if (Data.Min > 59)
        {
            Data.Hour += 1;
            Data.Min = 0;
        }
    }

    public void SaveStats()
    {
        //Saving stats
        string json = JsonUtility.ToJson(Data);
        File.WriteAllText(Application.dataPath + "/saveFile.json", json);
    }

    public void LoadStats()
    {
        //Loading stats
        string json = File.ReadAllText(Application.dataPath + "/saveFile.json");
        StatisticsData loadedStatisticsData = JsonUtility.FromJson<StatisticsData>(json);
        Data = loadedStatisticsData;
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

    public void ResetSave()
    {
        Data.number_win = 0;
        Data.number_lose = 0;
        SaveStats();
    }

    private void OnDestroy()
    {
        SaveStats();
        ResetSave();
    }
}
