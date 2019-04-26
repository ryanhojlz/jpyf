using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mini_Game : MonoBehaviour
{
    public Image imgTimer;
    public Image img;
    public GameObject Manager;

    bool isActiveQTE = false;
    bool QTE_Completed = true;

    float timeLimit = 5f;

    float currentTime = 0f;
    float maxTimeLimit = 0f;

    float sourceAmout = 100f;

    float sourceCurrentAmount = 0f;
    float sourceMaxAmount = 0f;

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

        if (!imgTimer || !img)
        {
            Debug.Log("some image cannot be found");
            return;
        }

        currentTime -= Time.deltaTime;
        imgTimer.fillAmount = currentTime / maxTimeLimit;
        img.fillAmount = sourceCurrentAmount / sourceMaxAmount;

        if (imgTimer.fillAmount <= 0)
        {
            isActiveQTE = false;
        }
        else if (img.fillAmount >= 1)
        {
            isActiveQTE = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            sourceCurrentAmount += 5f;
        }
        
	}

    public void QTEstart()
    {
        isActiveQTE = true;
        currentTime = timeLimit;
        maxTimeLimit = timeLimit;

        sourceCurrentAmount = 0f;
        sourceMaxAmount = sourceAmout;
    }
}
