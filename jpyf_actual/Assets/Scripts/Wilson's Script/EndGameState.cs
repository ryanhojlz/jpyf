using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameState : MonoBehaviour
{
    //public Image winImage;
    //public Image loseImage;
    public Text winText;
    bool ifWin = true;
    //winlose conditions
    // Start is called before the first frame update
    void Start()
    {
        winText.text = "WINLOSE";
    }

    // Update is called once per frame
    void Update()
    {
        if (ifWin == true)
        {
            winText.text = "YOU WIN";
            //EndGame();
        }
        if (ifWin == false)
        {
            winText.text = "YOU LOSE";
            //EndGame();
        }
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

    public void EndGame(string enemytag)
    {
        //what to do when lose game?
        //what to trigger?
        //change scene?
        //this is for if win or lose screen uses same scene but different text
       // Debug.Log("Game over");
        //Debug.Log(enemytag + " wins");

        GameObject.Find("Stats").GetComponent<Statistics>().incrementWin();

        //SceneManager.LoadScene("EndGame");
    }

}
