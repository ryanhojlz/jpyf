﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{
    Vector3 angle = Vector3.zero;
    float speed_of_rotation = 180f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        angle = this.transform.localEulerAngles;
        angle.y += Time.deltaTime * speed_of_rotation;
        //angle.x += Time.deltaTime * speed_of_rotation;
        //angle.z += Time.deltaTime * speed_of_rotation * 0.5f;
        this.transform.localEulerAngles = angle;
        //this.transform.localEulerAngles.y += Time.deltaTime * 60f;
    }
}