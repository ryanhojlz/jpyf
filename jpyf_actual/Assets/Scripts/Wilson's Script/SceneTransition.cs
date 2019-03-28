using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    //For the main menu buttons if there are any
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Titlescreen":
                {
                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                        SceneManager.LoadScene("MainMenu");
                    }
                    break;
                }
        }
    }

    //public void ToMainMenu()
    //{
    //    SceneManager.LoadScene("MainMenu");
    //}

    //public void ToGame()
    //{
    //    SceneManager.LoadScene("test");
    //}

    //public void ToEndGame()
    //{
    //    SceneManager.LoadScene(" ");
    //}
}
