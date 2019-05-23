﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prompt_Key : MonoBehaviour
{

    [SerializeField]
    Transform target = null;

    [SerializeField]
    Camera cam = null;

    [SerializeField]
    Sprite Triangle = null;

    [SerializeField]
    Sprite Circle = null;

    [SerializeField]
    Sprite Square = null;

    [SerializeField]
    Sprite Cross = null;

    public bool haveObjective = true;

    float this_width = 0f;
    float this_height = 0f;
    float thisXscale = 1f;
    float thisYscale = 1f;

    float previousTime = 0f;

    Color colorForAlpha = new Color();
    Stats_ResourceScript resourceHandler = null;

    PickupHandlerScript handler = null;

    public static Prompt_Key Instance = null;

    //Calculation Purposes
    Vector3 dir = Vector3.zero;
    Vector3 offSet = Vector3.zero;

    //Void
    Transform tempTargetHolder = null;

    //For optimisation
    RectTransform m_rectTransform = null;

    // Use this for initialization
    void Start()
    {
        handler = PickupHandlerScript.Instance;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        resourceHandler = Stats_ResourceScript.Instance;
        //target = GameObject.Find("PayLoad").transform;
        cam = GameObject.Find("ControllerCam").GetComponent<Camera>();
        this.GetComponent<Image>().sprite = Circle;


        m_rectTransform = this.GetComponent<RectTransform>();
        //this.GetComponent<Image>().sprite = low_hp;
        //canvas 
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.GetComponent<Image>())
            return;

        if (handler)
        {
            if (handler.nearest_pickup_object)
            {
                tempTargetHolder = handler.nearest_pickup_object;
                if (tempTargetHolder.GetChild(0))
                {
                    tempTargetHolder = tempTargetHolder.GetChild(0);
                }

                SetObjectiveTarget(tempTargetHolder);

                //Debug.Log("Setting");
            }
        }
        else
        {
            return;
        }

        if (!haveObjective || !target || target.GetComponent<Entity_Tengu>())
        {
            SetAlpha(0);
            return;
        }
        else
        {
            SetAlpha(1);
        }

        ThisImageSizeUpdate();

        if (target.GetComponent<Entity_Unit>())
        {
            //Debug.Log("Yup is here");

            offSet = target.position;
            offSet.y += target.GetComponent<CapsuleCollider>().height * target.lossyScale.y;

            dir = cam.WorldToScreenPoint(offSet);
        }
        else
        {
            dir = cam.WorldToScreenPoint(target.transform.position);
        }

        Vector3 forward = cam.transform.forward;
        Vector3 towardsTarget = target.transform.position - cam.transform.position;

        if (Vector3.Dot(towardsTarget.normalized, forward) <= 0 || !handler.nearest_pickup_object)
        {
            SetAlpha(0);
        }

        this.transform.position = dir;
        //Debug.Log(dir.normalized);
    }

    void ThisImageSizeUpdate()
    {
        thisXscale = this.transform.lossyScale.x;
        thisYscale = this.transform.lossyScale.y;

        this_width = m_rectTransform.rect.width * thisXscale;
        this_height = m_rectTransform.rect.height * thisYscale;

    }

    void SetAlpha(float value)
    {
        //this.GetComponent<Image>().sprite = low_lantern;
        colorForAlpha = this.GetComponent<Image>().color;
        colorForAlpha.a = value;
        this.GetComponent<Image>().color = colorForAlpha;
    }

    public void SetObjectiveTarget(Transform objective)
    {
        target = objective;
        haveObjective = true;
    }

    public void SetObjectiveOnOff(bool OnOff)
    {
        haveObjective = OnOff;
    }
}
