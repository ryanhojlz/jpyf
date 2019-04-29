﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    //float dayTimeTimer;
    //float nightTimeTimer;
    //bool isChangingDayNight;
    [SerializeField]
    float sunRotation;

    bool isDaytime;

    public static DayNightCycle Instance = null;

    // Start is called before the first frame update
    void Start()
    {
  
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        sunRotation = transform.eulerAngles.z;

        //dayTimeTimer += Time.deltaTime;
        transform.RotateAround(Vector3.zero, Vector3.right, 0); // to rotate by the side instead of front and back

        //if (isChangingDayNight == true)
        RotatingSun();

        if ((sunRotation < 90 && sunRotation > 0) || (sunRotation > 270 && sunRotation < 360))
        {
            isDaytime = true;
            Debug.Log("Daytime " + isDaytime);
        }

        if (sunRotation > 90 && sunRotation < 270)
        {
            isDaytime = false;
            Debug.Log("Night " + isDaytime);
        }
    }

    void RotatingSun()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, 15.0f * Time.deltaTime);
        transform.LookAt(Vector3.zero);
    }
}
