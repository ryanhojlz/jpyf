﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour
{
    public GameObject attackSlider;
    public GameObject hitArea;
    public GameObject arrow;
    public GameObject SuccessImage;
    public GameObject FailureImage;
    Vector3 arrowPosition;
    Vector3 hitAreaPosition;
    float travelDistance;

    //Used Variables
    Vector3 attackSliderPos;
    float ScaleX_attackSlider;
    float StartX;
    float EndX;

    Vector3 hitAreaPos;
    float ScaleX_hitArea;
    float StartX_hitArea;
    float EndX_hitArea;

    Vector3 arrowPos;
    float ScaleX_arrow;
    float StartX_arrow;
    float EndX_arrow;



    float ArrowSpeed = 200f;

    int QTEsuccessCounter;

    bool Started = true;
    bool SuccessTrue = false;
    bool FailureTrue = false;
    float timer;

    // Start is called before the first frame update
    void Awake()
    {
        //Zi Jun Test
        attackSliderPos = attackSlider.GetComponent<RectTransform>().localPosition;
        ScaleX_attackSlider = attackSlider.transform.localScale.x * 0.5f;

        ScaleX_hitArea = hitArea.transform.localScale.x * 0.5f;
        ScaleX_arrow = arrow.transform.localScale.x * 0.5f;

        StartX = attackSliderPos.x - ScaleX_attackSlider;
        EndX = attackSliderPos.x + ScaleX_attackSlider;

        arrowPos = arrow.transform.localPosition;
    }

    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SuccessImage.SetActive(false);
        FailureImage.SetActive(false);
        //Arrow scale & HitArea need to be in update. Might change during runtime
        ScaleX_hitArea = hitArea.transform.localScale.x * 0.5f;
        ScaleX_arrow = arrow.transform.localScale.x * 0.5f;

        //Getting position of Arrow & hitArea
        arrowPos = arrow.transform.localPosition;
        hitAreaPos = hitArea.transform.localPosition;

        //Setting the start and end position if Arrow n HitArea
        StartX_arrow = arrowPos.x - ScaleX_arrow;
        EndX_arrow = arrowPos.x + ScaleX_arrow;

        StartX_hitArea = hitAreaPos.x - ScaleX_hitArea;
        EndX_hitArea = hitAreaPos.x + ScaleX_hitArea;

        if (CheckWithin() && Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Success Hit");
            //Render the texture here
            //SuccessImage.SetActive(true);
            SuccessTrue = true;
            FullReset();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Fail Hit");
            //Render the texture here
            //FailureImage.SetActive(true);
            FailureTrue = true;
        }

        if (Input.GetKeyDown(KeyCode.R) || Started)
        {
            FullReset();
        }

        if (arrow.GetComponent<RectTransform>().localPosition.x + ScaleX_arrow > EndX)
        {
            //this.gameObject.SetActive(false);
            FullReset();
        }
        else
        {
            arrow.GetComponent<RectTransform>().localPosition = new Vector3(arrow.GetComponent<RectTransform>().localPosition.x + ArrowSpeed * Time.deltaTime, arrow.GetComponent<RectTransform>().localPosition.y, arrow.GetComponent<RectTransform>().localPosition.z);
        }
        if (SuccessTrue)
        {
            SuccessImage.SetActive(true);
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                Debug.Log("success enter");
                SuccessTrue = false;
                SuccessImage.SetActive(false);
                timer = 0;
            }
        }
        if (FailureTrue)
        {
            FailureImage.SetActive(true);
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                Debug.Log("failure enter");
                FailureTrue = false;
                FailureImage.SetActive(false);
                timer = 0;
            }
        }
    }

    void RandSpawnhitArea()
    {
        //float DistanceBTW = Mathf.Sqrt((StartX - EndX) * (StartX - EndX));//This will be the length(use this if things starts getting weird)
        float DistanceBTW = attackSlider.transform.localScale.x;//This will be the length

        hitArea.GetComponent<RectTransform>().localPosition = new Vector3(StartX + Random.Range(ScaleX_hitArea, (DistanceBTW - ScaleX_hitArea)), attackSliderPos.y, attackSliderPos.z);
    }

    void ResetArrow()
    {
        arrow.GetComponent<RectTransform>().localPosition = new Vector3(StartX + arrow.transform.localScale.x * 0.5f, arrow.GetComponent<RectTransform>().localPosition.y, arrow.GetComponent<RectTransform>().localPosition.z);
    }

    bool CheckWithin()
    {
        if ((StartX_arrow > StartX_hitArea && StartX_arrow < EndX_hitArea)
            || (EndX_arrow > StartX_hitArea && EndX_arrow < EndX_hitArea))
        {
            return true;
        }
        return false;
    }

    void SetStart(bool start)
    {
        Started = start;
    }

    bool GetStart()
    {
        return Started;
    }

    void FullReset()
    {
        RandSpawnhitArea();
        ResetArrow();
        Started = false;
    }

    void QTEsuccess()
    {

    }

    void QTEfailure()
    {

    }
}
