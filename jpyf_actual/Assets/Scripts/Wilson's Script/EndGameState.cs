using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameState
{
    //winlose conditions
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void WinGame()
    //{
    //    //what to do when win game?
    //    //what to trigger?
    //    //change scene?
    //    SceneManager.LoadScene("test");
    //}

    //public void LoseGame()
    //{
    //    //what to do when lose game?
    //    //what to trigger?
    //    //change scene?
    //    SceneManager.LoadScene("test");
    //}

    public void EndGame()
    {
        //what to do when lose game?
        //what to trigger?
        //change scene?
        //this is for if win or lose screen uses same scene but different text
        Debug.Log("Game over");
        SceneManager.LoadScene("EndGame");
    }

}
