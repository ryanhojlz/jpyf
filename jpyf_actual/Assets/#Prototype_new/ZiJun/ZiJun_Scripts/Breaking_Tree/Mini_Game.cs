using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mini_Game : MonoBehaviour
{
    public Image imgTimer;
    public Image img;
    public GameObject Manager;

    Object_Breaking objBreak = null;

    int counter = 1;
    //section
    int section = 3;

    bool isActiveQTE = false;
    bool QTE_Completed = true;

    float timeLimit = 5f;

    float currentTime = 0f;
    float maxTimeLimit = 0f;

    float sourceAmout = 100f;

    float sourceCurrentAmount = 0f;
    float sourceMaxAmount = 0f;

    float powerHit = 0f;

    // Use this for initialization
    void Start()
    {
        //QTEstart();
        if (!imgTimer)
        {
            Debug.Log("You have forgotten to put an image in inspector for : " + this.name);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if (!isActiveQTE)
        {
            Manager.SetActive(false);// = false;
            return;
        }
        else
        {
            Manager.SetActive(true);
        }

        if (objBreak)
        {
            if (!imgTimer || !img)
            {
                Debug.Log("some image cannot be found");
                return;
            }

            currentTime -= Time.deltaTime;
            imgTimer.fillAmount = currentTime / maxTimeLimit;
            img.fillAmount = sourceCurrentAmount / sourceMaxAmount;

            if (imgTimer.fillAmount <= 0 || img.fillAmount >= 1)
            {
                counter = (int)(section * img.fillAmount);
                isActiveQTE = false;
                objBreak.SetComplete(counter);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                BreakObject();
            }
        }
	}

    public void QTEstart(Object_Breaking other)
    {
        if (other)
        {
            objBreak = other;
            isActiveQTE = true;
            currentTime = other.GetTimeLimit();
            maxTimeLimit = other.GetTimeLimit();

            sourceCurrentAmount = 0f;
            sourceMaxAmount = other.GetMaxSpamPoint();

            powerHit = other.GetPowerPerHit();
        }
        
    }

    public void BreakObject()
    {
        sourceCurrentAmount += powerHit;
    }
}
