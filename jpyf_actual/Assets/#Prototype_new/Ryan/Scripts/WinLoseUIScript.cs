using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseUIScript : MonoBehaviour
{
    bool callonce = false;
    public Sprite WinImage = null;
    public Sprite LoseImage = null;

    GameEventsPrototypeScript Manager = null;
    Image myImage = null;

    public int renderimg = 0;
    public static WinLoseUIScript Instance = null;

    public Text text1 = null;
    public Text text2 = null;
    public Text text3 = null;
    public Text text4 = null;
    public Text text5 = null;
    public Text text6 = null;

    bool win = false;
    bool lose = false;


    // Use this for initialization
    void Start ()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(this.gameObject);


        myImage = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        var imgColor = myImage.color;

        // Render win lose screen
        switch (renderimg)
        {
            case -1:
                {
                    imgColor.a = 255;
                    myImage.color = imgColor;
                    myImage.sprite = LoseImage;

                    if (!lose)
                    {
                        if (Statistics.Instance)
                            Statistics.Instance.incrementLose();
                        lose = true;
                    }
                }
                break;
            case 0:
                imgColor.a = 0;
                myImage.color = imgColor;
                myImage.sprite = null;
                break;
            case 1:
                {
                    imgColor.a = 255;
                    myImage.color = imgColor;
                    myImage.sprite = WinImage;
                    if (!win)
                    {
                        if(Statistics.Instance)
                            Statistics.Instance.incrementWin();
                        win = true;
                    }
                }
                break;
        }



        //Debug.Log("ADAWDAWDWAD " + GameEventsPrototypeScript.Instance.ReturnIsLose());

        if (GameEventsPrototypeScript.Instance.ReturnIsWin() || GameEventsPrototypeScript.Instance.ReturnIsLose())
        {
            if (!callonce)
            {
                Endgamestats.Instance.calDistTravelled();
                callonce = true;
            }
            text1.text = "" + Endgamestats.Instance.ReturnDistanceTravel();
            text2.text = "" + Endgamestats.Instance.ReturnLanterUpTime();
            text3.text = "" + Endgamestats.Instance.ReturnLightCollected();
            text4.text = "" + Endgamestats.Instance.ReturnWoodCollected();
            text5.text = "" + Endgamestats.Instance.ReturnPlayerHealingDone();
            text6.text = "" + Endgamestats.Instance.ReturnCartHealingDone();
        }
        else
        {
            text1.text = "";
            text2.text = "";
            text3.text = "";
            text4.text = "";
            text5.text = "";
            text6.text = "";
        }
    }
}
