using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prompt_Breaking : MonoBehaviour
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

    float this_width = 0f;
    float this_height = 0f;
    float thisXscale = 1f;
    float thisYscale = 1f;

    float previousTime = 0f;

    Color colorForAlpha = new Color();
    Stats_ResourceScript resourceHandler = null;

    //PickupHandlerScript handler = null;

    public static Prompt_Breaking Instance = null;

    //Calculation Purposes
    Vector3 dir = Vector3.zero;
    Vector3 offSet = Vector3.zero;

    //Void
    Transform tempTargetHolder = null;

    Mini_Game minigame = null;

    //For optimisation
    RectTransform m_rectTransform = null;

    // Use this for initialization
    private void Awake()
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
    void Start()
    {
        minigame = Mini_Game.Instance;
        //handler = PickupHandlerScript.Instance;

        resourceHandler = Stats_ResourceScript.Instance;
        //target = GameObject.Find("PayLoad").transform;
        cam = GameObject.Find("ControllerCam").GetComponent<Camera>();
        this.GetComponent<Image>().sprite = Triangle;
        //this.GetComponent<Image>().sprite = low_hp;
        //canvas 

        m_rectTransform = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.GetComponent<Image>())
            return;

        if (minigame)
        {
            if (minigame.GetNearestBreakingObj())
            {
                tempTargetHolder = minigame.GetNearestBreakingObj();
                if (tempTargetHolder.GetChild(0))
                {
                    tempTargetHolder = tempTargetHolder.GetChild(0);
                }

                SetObjectiveTarget(tempTargetHolder);
            }
        }
        else
        {
            return;
        }

        //Debug.Log(target);

        if (!target)
        {            
            SetAlpha(0);
            //UI_FeedbackScript.Instance.InteractTrue[0] = false;
            return;
        }
        else
        {
            //UI_FeedbackScript.Instance.InteractTrue[0] = true;
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

        if (Vector3.Dot(towardsTarget.normalized, forward) <= 0 || !minigame.GetNearestBreakingObj())
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
    }
}

