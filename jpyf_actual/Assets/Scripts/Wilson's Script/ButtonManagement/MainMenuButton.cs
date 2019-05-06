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

    //public GameObject highlighted;

    public Canvas Titlescreencanvas;
    public Canvas Mainmenucanvas;

    GameObject titleScreen;
    GameObject text1;
    GameObject text2;
    GameObject text3;
    GameObject text4;

    Vector3 originalPos;
    Vector3 originalPos2;
    Vector3 originalPos3;
    Vector3 originalPos4;

    float optionOffset = 100;

    public bool TitlescreenDisplay = true;

    int indexX = 0;
    int indexY = 0;

    private bool loadScene = false;

    private string scene;
    //private Text loadingText;
    public Image loadImage;

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
        originalPos = buttons[0].transform.position;
        originalPos2 = buttons[1].transform.position;
        originalPos3 = buttons[2].transform.position;
        originalPos4 = buttons[3].transform.position;
        UnityEngine.XR.XRSettings.showDeviceView = false;
        loadImage.enabled = false;
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
        SelectedOptions();
        //highlighted.gameObject.transform.position = buttons[indexX/*, indexY*/].gameObject.transform.position;

        //highlighted.transform.position = new Vector3(buttons[indexX].transform.position.x + ((buttons[indexX].GetComponent<RectTransform>().rect.width * buttons[indexX].transform.lossyScale.x * 0.5f)) + (highlighted.GetComponent<RectTransform>().rect.width * highlighted.transform.lossyScale.x * 0.5f), buttons[indexX].transform.position.y, buttons[indexX].transform.position.z);
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

        if (originalPos != buttons[0].transform.position && buttons[indexX] != buttons[0])
        {
            buttons[0].transform.position = originalPos;
        }
        if (originalPos != buttons[1].transform.position && buttons[indexX] != buttons[1])
        {
            buttons[1].transform.position = originalPos2;
        }
        if (originalPos != buttons[2].transform.position && buttons[indexX] != buttons[2])
        {
            buttons[2].transform.position = originalPos3;
        }
        if (originalPos != buttons[3].transform.position && buttons[indexX] != buttons[3])
        {
            buttons[3].transform.position = originalPos4;
        }

        //Debug.Log(TitlescreenDisplay);
        Debug.Log(scene);
        // If the player has pressed the space bar and a new scene is not loading yet...
        if (scene != null && loadScene == false)
        {
            // ...set the loadScene boolean to true to prevent loading a new scene more than once...
            loadScene = true;

            // ...change the instruction text to read "Loading..."
            //loadingText.text = "Loading...";
            loadImage.enabled = true;
            // ...and start a coroutine that will load the desired scene.
            StartCoroutine(LoadNewScene());
        }

        // If the new scene has started loading...
        if (loadScene == true)
        {
            // ...then pulse the transparency of the loading text to let the player know that the computer is still working.
            //loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
            loadImage.color = new Color(loadImage.color.r, loadImage.color.g, loadImage.color.b, Mathf.PingPong(Time.time, 1));
        }
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

    public void SelectedOptions()
    {
        if (TitlescreenDisplay)
            return;

        Vector3 currButtonPos = buttons[indexX].transform.position;

        switch (buttons[indexX/*, indexY*/].gameObject.name)
        {
            case "coopvsai":
                {
                    if (buttons[indexX].transform.position.x < originalPos.x + optionOffset)
                        buttons[indexX].transform.position = new Vector3(currButtonPos.x += 500 * Time.deltaTime, currButtonPos.y, currButtonPos.z);
                }
                break;

            case "pvp":
                {
                    if (buttons[indexX].transform.position.x < originalPos2.x + optionOffset)
                        buttons[indexX].transform.position = new Vector3(currButtonPos.x += 500 * Time.deltaTime, currButtonPos.y, currButtonPos.z);
                }
                break;

            case "settings":
                {
                    if (buttons[indexX].transform.position.x < originalPos3.x + optionOffset)
                        buttons[indexX].transform.position = new Vector3(currButtonPos.x += 500 * Time.deltaTime, currButtonPos.y, currButtonPos.z);
                }
                break;

            case "credits":
                {
                    if (buttons[indexX].transform.position.x < originalPos4.x + optionOffset)
                        buttons[indexX].transform.position = new Vector3(currButtonPos.x += 500 * Time.deltaTime, currButtonPos.y, currButtonPos.z);
                }
                break;
        }

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
                    //SceneManager.LoadScene("Week_3Merge");
                    scene = "Week_3Merge";
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
                    //SceneManager.LoadScene("Achievement_Scene");
                    scene = "Achievement_Scene";
                }
                break;

            case "credits":
                {
                    Debug.Log("Loading game scene");
                    //SceneManager.LoadScene("PC_Build");
                    scene = "PC_Build";
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

    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene()
    {
        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }
    }
}
