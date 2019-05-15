using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseUIScript : MonoBehaviour
{
    public Sprite WinImage = null;
    public Sprite LoseImage = null;

    GameEventsPrototypeScript Manager = null;
    Image myImage = null;

    public int renderimg = 0;

    public static WinLoseUIScript Instance = null;

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
                imgColor.a = 255;
                myImage.color = imgColor;
                myImage.sprite = LoseImage;
                break;
            case 0:
                imgColor.a = 0;
                myImage.color = imgColor;
                myImage.sprite = null;
                break;
            case 1:
                imgColor.a = 255;
                myImage.color = imgColor;
                myImage.sprite = WinImage;
                break;
        }




    }
}
