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

    public Canvas Titlescreencanvas;
    public Canvas Mainmenucanvas;
    public Canvas Loadingcanvas;
    public Canvas Statscanvas;

    GameObject titleScreen;
    GameObject text1;
    GameObject text2;
    GameObject text3;
    GameObject text4;

    Vector3 originalPos;
    Vector3 originalPos2;
    Vector3 originalPos3;
    Vector3 originalPos4;

    float optionOffset = Screen.width * 0.1f;

    float ScreenWidth = Screen.width;
    float ScreenHeight = Screen.height;

    public bool TitlescreenDisplay = true;

    int indexX = 0;
    int indexY = 0;

    private bool loadScene = false;

    private string scene;
    //private Text loadingText;
    public Image loadImage;

    float speed = 500f;
    float valueToStop = 0.001f;

    // Use this for initialization
    void Start()
    {
        buttons[0] = coopvsai;
        buttons[1] = pvp;
        buttons[2] = settings;
        buttons[3] = credits;
        titleScreen = GameObject.Find("Titlescreen");
        text1 = GameObject.Find("Text1");
        text2 = GameObject.Find("Text2");
        text3 = GameObject.Find("Text3");
        text4 = GameObject.Find("Text4");
        originalPos = buttons[0].GetComponent<RectTransform>().anchoredPosition;
        originalPos2 = buttons[1].GetComponent<RectTransform>().anchoredPosition;
        originalPos3 = buttons[2].GetComponent<RectTransform>().anchoredPosition;
        originalPos4 = buttons[3].GetComponent<RectTransform>().anchoredPosition;
        UnityEngine.XR.XRSettings.showDeviceView = false;
        loadImage.enabled = false;
        Loadingcanvas.enabled = false;
        Statscanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(buttons[indexX/*, indexY*/].gameObject.name);

        optionOffset = Screen.width * 0.1f;
        text1.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);
        text4.SetActive(false);
        MessageDisplay();
        SelectedOptions();

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
        if (!Statscanvas.enabled)
        {
            if (TitlescreenDisplay == false)
            {
                Mainmenucanvas.enabled = true;
                Titlescreencanvas.enabled = false;
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    EnterSelected();
                }
            }
        }

        if (Statscanvas.enabled && Input.GetKeyDown(KeyCode.Escape))
        {
            Statscanvas.enabled = false;
            Mainmenucanvas.enabled = true;
        }
        if (buttons[indexX] != buttons[0])
        {
            buttons[0].GetComponent<RectTransform>().anchoredPosition = originalPos;
        }
        if (buttons[indexX] != buttons[1])
        {
            buttons[1].GetComponent<RectTransform>().anchoredPosition = originalPos2;
        }
        if (buttons[indexX] != buttons[2])
        {
            buttons[2].GetComponent<RectTransform>().anchoredPosition = originalPos3;
        }
        if (buttons[indexX] != buttons[3])
        {
            buttons[3].GetComponent<RectTransform>().anchoredPosition = originalPos4;
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
            Mainmenucanvas.enabled = false;
            Loadingcanvas.enabled = true;
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
        //float value = 0.001f;


        switch (buttons[indexX/*, indexY*/].gameObject.name)
        {
            case "start":
                {
                    ////if (buttons[indexX].transform.position.x < originalPos.x + optionOffset)
                    ////    buttons[indexX].transform.position = new Vector3(currButtonPos.x += 500 * Time.deltaTime, currButtonPos.y, currButtonPos.z);
                    //Vector2 temp = buttons[indexX].GetComponent<RectTransform>().anchoredPosition;
                    //Debug.Log(temp);
                    //if (buttons[indexX].GetComponent<RectTransform>().position.x 
                    //    - buttons[indexX].GetComponent<RectTransform>().rect.width
                    //    * buttons[indexX].GetComponent<RectTransform>().localScale.x 
                    //    < Screen.width * 0.25)
                    //{
                    //    temp.x += speed * Time.deltaTime;
                    //    buttons[indexX].GetComponent<RectTransform>().anchoredPosition = temp;
                    //}

                    Vector2 temp = buttons[indexX].GetComponent<RectTransform>().anchoredPosition;
                    if (temp.x - buttons[indexX].GetComponent<RectTransform>().rect.width * 0.5f < originalPos.x + Screen.width * valueToStop)
                    {
                        temp.x += speed * Time.deltaTime;
                        buttons[indexX].GetComponent<RectTransform>().anchoredPosition = temp;
                    }
                    //else
                    //{
                    //    temp.x = originalPos.x + Screen.width * valueToStop + buttons[indexX].GetComponent<RectTransform>().rect.width * 0.5f;
                    //    buttons[indexX].GetComponent<RectTransform>().anchoredPosition = temp;
                    //}
                }
                break;

            case "tutorial":
                {
                    ////if (buttons[indexX].transform.position.x < originalPos2.x + optionOffset)
                    ////    buttons[indexX].transform.position = new Vector3(currButtonPos.x += 500 * Time.deltaTime, currButtonPos.y, currButtonPos.z);
                    //Vector2 temp = buttons[indexX].GetComponent<RectTransform>().anchoredPosition;
                    //if (buttons[indexX].GetComponent<RectTransform>().position.x 
                    //    - buttons[indexX].GetComponent<RectTransform>().rect.width
                    //    * buttons[indexX].GetComponent<RectTransform>().localScale.x 
                    //    < Screen.width * 0.25)
                    //{
                    //    temp.x += speed * Time.deltaTime;
                    //    buttons[indexX].GetComponent<RectTransform>().anchoredPosition = temp;
                    //}
                    Vector2 temp = buttons[indexX].GetComponent<RectTransform>().anchoredPosition;
                    if (temp.x - buttons[indexX].GetComponent<RectTransform>().rect.width * 0.5f < originalPos.x + Screen.width * valueToStop)
                    {
                        temp.x += speed * Time.deltaTime;
                        buttons[indexX].GetComponent<RectTransform>().anchoredPosition = temp;
                    }
                    //else
                    //{
                    //    temp.x = originalPos.x + Screen.width * valueToStop + buttons[indexX].GetComponent<RectTransform>().rect.width * 0.5f;
                    //    buttons[indexX].GetComponent<RectTransform>().anchoredPosition = temp;
                    //}
                }
                break;

            case "quit":
                {
                    ////if (buttons[indexX].transform.position.x < originalPos3.x + optionOffset)
                    ////    buttons[indexX].transform.position = new Vector3(currButtonPos.x += 500 * Time.deltaTime, currButtonPos.y, currButtonPos.z);
                    //Vector2 temp = buttons[indexX].GetComponent<RectTransform>().anchoredPosition;
                    //if (buttons[indexX].GetComponent<RectTransform>().position.x 
                    //    - buttons[indexX].GetComponent<RectTransform>().rect.width
                    //    * buttons[indexX].GetComponent<RectTransform>().localScale.x 
                    //    < Screen.width * 0.25)
                    //{
                    //    temp.x += speed * Time.deltaTime;
                    //    buttons[indexX].GetComponent<RectTransform>().anchoredPosition = temp;
                    //}
                    Vector2 temp = buttons[indexX].GetComponent<RectTransform>().anchoredPosition;
                    if (temp.x - buttons[indexX].GetComponent<RectTransform>().rect.width * 0.5f < originalPos.x + Screen.width * valueToStop)
                    {
                        temp.x += speed * Time.deltaTime;
                        buttons[indexX].GetComponent<RectTransform>().anchoredPosition = temp;
                    }
                    //else
                    //{
                    //    temp.x = originalPos.x + Screen.width * valueToStop + buttons[indexX].GetComponent<RectTransform>().rect.width * 0.5f;
                    //    buttons[indexX].GetComponent<RectTransform>().anchoredPosition = temp;
                    //}
                }
                break;

            case "credits":
                {
                    ////if (buttons[indexX].transform.position.x < originalPos4.x + optionOffset)
                    ////    buttons[indexX].transform.position = new Vector3(currButtonPos.x += 500 * Time.deltaTime, currButtonPos.y, currButtonPos.z);


                    //Vector2 temp = buttons[indexX].GetComponent<RectTransform>().anchoredPosition;
                    //if (buttons[indexX].GetComponent<RectTransform>().position.x
                    //    - buttons[indexX].GetComponent<RectTransform>().rect.width
                    //    * buttons[indexX].GetComponent<RectTransform>().localScale.x
                    //    < Screen.width * 0.25)
                    //{
                    //    temp.x += speed * Time.deltaTime;
                    //    buttons[indexX].GetComponent<RectTransform>().anchoredPosition = temp;
                    //}
                    Vector2 temp = buttons[indexX].GetComponent<RectTransform>().anchoredPosition;
                    if (temp.x - buttons[indexX].GetComponent<RectTransform>().rect.width * 0.5f < originalPos.x + Screen.width * valueToStop)
                    {
                        temp.x += speed * Time.deltaTime;
                        buttons[indexX].GetComponent<RectTransform>().anchoredPosition = temp;
                    }
                    //else
                    //{
                    //    temp.x = originalPos.x + Screen.width * valueToStop + buttons[indexX].GetComponent<RectTransform>().rect.width * 0.5f;
                    //    buttons[indexX].GetComponent<RectTransform>().anchoredPosition = temp;
                    //}

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
            case "start":
                {
                    Debug.Log("Loading game scene");
                    //SceneManager.LoadScene("Week_3Merge");
                    scene = "CurrentScene";
                    //SceneManager.LoadScene("PC_Build_Wilson");

                }
                break;

            case "tutorial":
                {
                    scene = "Ryan_Prototype";
                }
                break;

            case "quit":
                {
                    Debug.Log("Loading game scene");
                    //SceneManager.LoadScene("Achievement_Scene");
                    //scene = "Achievement_Scene";
                    Application.Quit();
                }
                break;

            case "credits":
                {
                    Debug.Log("Loading game scene");
                    //SceneManager.LoadScene("PC_Build");
                    //scene = "PC_Build";
                    Statscanvas.enabled = true;
                    Mainmenucanvas.enabled = false;
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
            case "start":
                {
                    text1.SetActive(true);
                }
                break;

            case "tutorial":
                {
                    text2.SetActive(true);
                }
                break;

            case "quit":
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
        //float loadTime = 0f;
        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        //yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            //loadTime += Time.deltaTime;
            //Debug.Log("Time Taken to load : " + loadTime);
            yield return null;
            //Debug.Log("Time Taken to load : 2 . " + loadTime);
        }
    }
}
