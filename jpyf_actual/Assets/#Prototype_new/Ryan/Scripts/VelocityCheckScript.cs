using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VelocityCheckScript : MonoBehaviour
{
    // Velocity Check for move controller
    public Vector3 oldPos = Vector3.zero;
    public Vector3 newPos = Vector3.zero;
    private float currVelocity = 0;
    Transform debugUI;
    Transform debugUI2;

    // Debug
    public Vector3 originalPos = Vector3.zero;
    
    //Shake Counter
    int shakeCounter = 0;
    float threshHold = 0.05f;
    enum VelocityCheck
    {
        NEUTRAL,
        UP,
        DOWN
    };


    VelocityCheck ControllerState = VelocityCheck.NEUTRAL;
    
    // Use this for initialization
    void Start()
    {
        oldPos = Vector3.zero;
        newPos = Vector3.zero;
        debugUI = GameObject.Find("Text3").transform;
        debugUI2 = GameObject.Find("Text4").transform;

        originalPos = this.transform.position;
        ControllerState = VelocityCheck.NEUTRAL;
    }

    // Vigrous Shaking Thresh hold distance shud be around 0.016 ~ 0.02;

    // Light Shaking Thresh hold distance shud be around 0.01 ish

    // Update is called once per frame
    void Update()
    {
        // Velocity Check calculation
        newPos = this.transform.position;
        // Use magnitude here bcos can get negative value;
        currVelocity = newPos.y - oldPos.y;

        //if (currVelocity < -0.05f)
        //{
        //    Debug.Log("Dmitri you have forsaken me down");
        //}
        //else if (currVelocity > 0.05f)
        //{
        //    Debug.Log("Dmitri you have forsaken me up");
        //}

        // Velocity Check
        if (ControllerState == VelocityCheck.NEUTRAL)
        {
            if (currVelocity > threshHold) // Upwards
            {
                //Debug.Log("Dmitri neutral to up");
                ++shakeCounter;
                ControllerState = VelocityCheck.UP;
            }
            else if (currVelocity < -threshHold) // Downwards
            {
                //Debug.Log("Dmitri neutral to down");
                ++shakeCounter;
                ControllerState = VelocityCheck.DOWN;
            }
        }
        else if (ControllerState == VelocityCheck.UP)
        {
            if (currVelocity < -threshHold)
            {
                //Debug.Log("Dmitri up to down");
                ++shakeCounter;
                ControllerState = VelocityCheck.DOWN;
            }
        }
        else if (ControllerState == VelocityCheck.DOWN)
        {
            if (currVelocity > threshHold)
            {
                //Debug.Log("Dmitri down to up");
                ++shakeCounter;
                ControllerState = VelocityCheck.UP;
            }
        }

        debugUI.GetComponent<Text>().text = "" + currVelocity;
        debugUI2.GetComponent<Text>().text = "" + ControllerState;

        oldPos = newPos;

        ReloadAction();
        DebugFunc();
    }

    // PC Debugg func
    void DebugFunc()
    {
        var pos = this.transform.position;

        if (Input.GetKey(KeyCode.K))
        {

            pos.y += 1 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.L))
        {
            pos.y -= 1 * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {

            pos = originalPos;
            pos.y += 0.05f;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {

            pos = originalPos;
            pos.y -= 0.05f;
        }


        this.transform.position = pos;
    }

    // Reload action
    void ReloadAction()
    {
        if (shakeCounter >= 5)
        {
            Debug.Log("Reload");
            shakeCounter = 0;
            GetComponent<RangeAttackScript>().Ammo = 3;
        }
    }

}
