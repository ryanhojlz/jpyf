using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    GameObject[,] buttons = new GameObject[2, 2];

    public GameObject x0y0;
    public GameObject x0y1;
    public GameObject x1y0;
    public GameObject x1y1;

    public GameObject highlighted;

    int indexX = 0;
    int indexY = 0;

    // Use this for initialization
    void Start()
    {
        //buttons = new Transform[1][1];
        buttons[0, 0] = x0y0;
        buttons[0, 1] = x0y1;
        buttons[1, 0] = x1y0;
        buttons[1, 1] = x1y1;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(buttons[indexX, indexY].gameObject.name);

        highlighted.gameObject.transform.position = buttons[indexX, indexY].gameObject.transform.position;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            EnterSelected();
        }

    }

    void MoveUp()
    {
        if (indexY > 0)
        {
            indexY = 0;
        }
        else
        {
            indexY += 1;
        }
    }

    void MoveDown()
    {
        if (indexY <= 0)
        {
            indexY = 1;
        }
        else
        {
            indexY -= 1;
        }
    }

    void MoveLeft()
    {
        if (indexX <= 0)
        {
            indexX = 0;
        }
        else
        {
            indexX -= 1;
        }
    }

    void MoveRight()
    {
        if (indexX > 0)
        {
            indexX = 0;
        }
        else
        {
            indexX += 1;
        }
    }

    void EnterSelected()
    {
        switch (buttons[indexX, indexY].gameObject.name)
        {
            case "coopvsai":
                {
                    Debug.Log("Loading game scene");
                    SceneManager.LoadScene("PC_Build_Wilson");
                }
                break;

            case "pvp":
                {

                }
                break;

            case "settings":
                {

                }
                break;

            case "credits":
                {

                }
                break;

        }

    }
}
