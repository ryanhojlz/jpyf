using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackHandler : MonoBehaviour
{
    //For hitting payload feedback//////////////
    bool isHit = true;                        //
    bool rotate = false;                      //
    bool isComplete = false;                  //
    float rotateTime = 0f;                    //
    float previousTime = 0f;                  //
                                              //
    float TotalRotateTime = 0.2f;             //
                                              //
    enum TurnSEQ                              //
    {                                         //
        None,                                 //
        TurnLeft,                             //
        TurnRight,                            //
        BackToCenter                          //
    }                                         //
                                              //
    Quaternion AssignRotation;                //
                                              //
                                              //
    TurnSEQ payloadTurning = TurnSEQ.None;    //
    TurnSEQ Previous = TurnSEQ.None;          //
    TurnSEQ StartWith = TurnSEQ.None;         //
                                              //
    GameObject payLoad = null;                //
    //For hitting payload feedback//////////////


    // Use this for initialization
    void Start()
    {
        //For hitting payload feedback//////////
        payLoad = GameObject.Find("PayLoad"); //
        rotateTime = TotalRotateTime / 4;     //
        //For hitting payload feedback//////////
    }

    // Update is called once per frame
    void Update()
    {
        //For hitting payload feedback//////////
        if (Input.GetKeyDown(KeyCode.K))      //
        {                                     //
            isHit = true;                     //
        }                                     //
                                              //
        if (isHit)                            //
        {                                     //
            HitPayloadFeedBack();             //
        }                                     //
        //For hitting payload feedback//////////
    }

    void HitPayloadFeedBack()
    {
        switch (payloadTurning)
        {
            case TurnSEQ.TurnLeft:
                {
                    payLoad.transform.Rotate(Vector3.forward, 10 * Time.deltaTime);

                    if (previousTime + rotateTime < Time.time)
                    {
                        previousTime = Time.time;
                        Previous = TurnSEQ.TurnLeft;
                        payloadTurning = TurnSEQ.BackToCenter;
                    }
                }
                break;

            case TurnSEQ.TurnRight:
                {
                    payLoad.transform.Rotate(Vector3.forward, -10 * Time.deltaTime);

                    if (previousTime + rotateTime < Time.time)
                    {
                        previousTime = Time.time;
                        Previous = TurnSEQ.TurnRight;
                        payloadTurning = TurnSEQ.BackToCenter;
                    }
                }
                break;
            case TurnSEQ.BackToCenter:
                {
                    if (Previous != StartWith)
                    {
                        isComplete = true;
                    }
                   
                    if (Previous == TurnSEQ.TurnLeft)
                    {
                        payLoad.transform.Rotate(Vector3.forward, -10 * Time.deltaTime);
                    }
                    else if (Previous == TurnSEQ.TurnRight)
                    {
                        payLoad.transform.Rotate(Vector3.forward, 10 * Time.deltaTime);
                    }

                    if (previousTime + rotateTime < Time.time)
                    {
                        if (isComplete)
                        {
                            AssignRotation = payLoad.transform.rotation;
                            AssignRotation.z = 0;
                            payLoad.transform.rotation = AssignRotation;

                            payloadTurning = TurnSEQ.None;
                            Previous = TurnSEQ.None;
                            StartWith = TurnSEQ.None;

                            isComplete = false;
                            isHit = false;
                        }
                        else
                        {
                            previousTime = Time.time;

                            if (Previous == TurnSEQ.TurnLeft)
                                payloadTurning = TurnSEQ.TurnRight;
                            else if (Previous == TurnSEQ.TurnRight)
                                payloadTurning = TurnSEQ.TurnLeft;

                            Previous = TurnSEQ.BackToCenter;
                        }
                    }
                    
                }
                break;

            case TurnSEQ.None:
                {
                    previousTime = Time.time;
                    payloadTurning = TurnSEQ.TurnRight;
                    Previous = TurnSEQ.None;

                    StartWith = payloadTurning;
                }
                break;
        }
    }
    public void HitPayload()
    {
        isHit = true;
    }
}
