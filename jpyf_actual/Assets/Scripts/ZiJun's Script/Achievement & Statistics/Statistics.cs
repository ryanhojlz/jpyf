using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    int number_win = 0;
    int number_lose = 0;

    // Use this for initialization
    void Start()
    {
        //loading value
    }

    // Update is called once per frame
    void Update()
    {
        //updating value
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
        return number_win;
    }

    public int GetLoses()
    {
        return number_lose;
    }

    public void incrementWin()
    {
        number_win++;
    }

    public void incrementLose()
    {
        number_lose++;
    }

    public void SetWin(int value)
    {
        number_win = value;
    }

    public void SetLose(int value)
    {
        number_lose = value;
    }
}
