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
    bool ifGameEnd = false;
    public GameObject ally_wall = null;
    public GameObject enemy_wall = null;
    //winlose conditions
    // Start is called before the first frame update
    void Start()
    {
        winText.text = "WINLOSE";
    }

    // Update is called once per frame
    void Update()
    {
        if (!ifGameEnd)
        {
            // If ally hp 0 lose
            if (ally_wall.GetComponent<BasicGameOBJ>().healthValue <= 0)
            {
                ifGameEnd = true;
                ifWin = false;
                // Change Text
                //winlosetext.GetComponent<TextMesh>().text = "PLAYER LOSE";
                winText.GetComponent<Text>().text = "YOU LOSE";
            }
            // If enemy hp 0 win
            else if (enemy_wall.GetComponent<BasicGameOBJ>().healthValue <= 0)
            {
                ifGameEnd = true;
                ifWin = true;
                // Change Text
                //winlosetext.GetComponent<TextMesh>().text = "PLAYER WIN";
                winText.GetComponent<Text>().text = "YOU WIN";
            }
            // If game ended render
            winText.enabled = false;
        }
        else
        {
            // If game ended render
            winText.enabled = true;
        }
        //if (ifWin == true)
        //{
        //    winText.text = "YOU WIN";
        //    //EndGame();
        //}
        //if (ifWin == false)
        //{
        //    winText.text = "YOU LOSE";
        //    //EndGame();
        //}
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

        GameObject.Find("StatsManager").GetComponent<Statistics>().incrementWin();

        //SceneManager.LoadScene("EndGame");
    }

}
