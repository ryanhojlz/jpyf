using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_PS4
using UnityEngine.PS4;
using UnityEngine.XR;
#endif

public class MainMenuButton : MonoBehaviour
{
    GameObject[] buttons = new GameObject[4];

    public GameObject coopvsai;
    public GameObject pvp;
    public GameObject settings;
    public GameObject credits;

    public GameObject highlighted;

    public Canvas Titlescreencanvas;
    public Canvas Mainmenucanvas;

    GameObject titleScreen;
    GameObject text1;
    GameObject text2;
    GameObject text3;
    GameObject text4;

    public bool TitlescreenDisplay = true;

    int indexX = 0;
    int indexY = 0;

    // Use this for initialization
    void Start()
    {
        //buttons = new Transform[1][1];
        buttons[0] = coopvsai;
        buttons[1] = pvp;
        buttons[2] = settings;
        buttons[3] = credits;
        titleScreen = GameObject.Find("Titlescreen");
        text1 = GameObject.Find("Text1");
        text2 = GameObject.Find("Text2");
        text3 = GameObject.Find("Text3");
        text4 = GameObject.Find("Text4");
        UnityEngine.XR.XRSettings.showDeviceView = false;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(buttons[indexX/*, indexY*/].gameObject.name);

        text1.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);
        text4.SetActive(false);
        MessageDisplay();
        highlighted.gameObject.transform.position = buttons[indexX/*, indexY*/].gameObject.transform.position;

        highlighted.transform.position = new Vector3(buttons[indexX].transform.position.x + ((buttons[indexX].GetComponent<RectTransform>().rect.width * buttons[indexX].transform.lossyScale.x * 0.5f)) + (highlighted.GetComponent<RectTransform>().rect.width * highlighted.transform.lossyScale.x * 0.5f), buttons[indexX].transform.position.y, buttons[indexX].transform.position.z);
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

        if (Input.GetKeyUp(KeyCode.Space))
        {
            //titleScreen.SetActive(false);
            TitlescreenDisplay = false;
        }
        if (TitlescreenDisplay == true)
        {
            Titlescreencanvas.enabled = true;
            Mainmenucanvas.enabled = false;
        }
        if (TitlescreenDisplay == false)
        {
            Mainmenucanvas.enabled = true;
            Titlescreencanvas.enabled = false;
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                EnterSelected();
            }
        }

        //Debug.Log(TitlescreenDisplay);
    }

    public void MoveUp()
    {
        if (indexX > 0)
            indexX--;
    }

    public void MoveDown()
    {
        if (indexX < 3)
            indexX++;
    }

    public void MoveLeft()
    {
        if (indexX > 0)
            indexX--;
    }

    public void MoveRight()
    {
        if (indexX < 3)
            indexX++;
    }

    public void EnterSelected()
    {
        if (TitlescreenDisplay)
            return;
        switch (buttons[indexX/*, indexY*/].gameObject.name)
        {
            case "coopvsai":
                {
                    Debug.Log("Loading game scene");
                    SceneManager.LoadScene("Week_3Merge");
                    //SceneManager.LoadScene("PC_Build_Wilson");

                }
                break;

            case "pvp":
                {

                }
                break;

            case "settings":
                {
                    Debug.Log("Loading game scene");
                    SceneManager.LoadScene("Achievement_Scene");
                }
                break;

            case "credits":
                {
                    Debug.Log("Loading game scene");
                    SceneManager.LoadScene("PC_Build");
                }
                break;

        }

    }


    public void MessageDisplay()
    {
        if (TitlescreenDisplay)
            return;
        switch (buttons[indexX/*, indexY*/].gameObject.name)
        {
            case "coopvsai":
                {
                    text1.SetActive(true);

                }
                break;

            case "pvp":
                {
                    text2.SetActive(true);
                }
                break;

            case "settings":
                {
                    text3.SetActive(true);
                }
                break;

            case "credits":
                {
                    text4.SetActive(true);
                }
                break;
        }
    }
}
