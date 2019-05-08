using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Follow_Objective : MonoBehaviour
{

    [SerializeField]
    Transform target = null;

    [SerializeField]
    Camera cam = null;

    public bool haveObjective = true;

    float this_width = 0f;
    float this_height = 0f;
    float thisXscale = 1f;
    float thisYscale = 1f;

    float previousTime = 0f;

    Color colorForAlpha = new Color();
    Stats_ResourceScript resourceHandler = null;

    public static Follow_Objective Instance = null;

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
        //if (Instance == null)
        //{
        //    Instance = this;
        //}
        //else
        //{
        //    Destroy(this);
        //}

        resourceHandler = Stats_ResourceScript.Instance;
        //target = GameObject.Find("PayLoad").transform;
        cam = GameObject.Find("ControllerCam").GetComponent<Camera>();
        //this.GetComponent<Image>().sprite = low_hp;
        //canvas 
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.GetComponent<Image>())
            return;

        




        if (!haveObjective || !target)
        {
            SetAlpha(0);
            return;
        }
        else
        {
            SetAlpha(1);
        }

        ThisImageSizeUpdate();

        Vector3 dir = cam.WorldToScreenPoint(target.transform.position);

        Vector3 forward = cam.transform.forward;
        Vector3 towardsTarget = target.transform.position - cam.transform.position;

        if (Vector3.Dot(towardsTarget.normalized, forward) > 0)
        {
            if (dir.x > Screen.width - (this.this_width * 0.5f))
            {
                dir.x = Screen.width - (this.this_width * 0.5f);
            }
            else if (dir.x < 0 + (this.this_width * 0.5f))
            {
                dir.x = 0 + (this.this_width * 0.5f);
            }

            if (dir.y > Screen.height - (this.this_height * 0.5f))
            {
                dir.y = Screen.height - (this.this_height * 0.5f);
            }
            else if (dir.y < 0 + (this.this_height * 0.5f))
            {
                dir.y = 0 + (this.this_height * 0.5f);
            }
            this.transform.position = dir;
        }
        else
        {
            //Debug.Log("Forward : " + forward);

            if (dir.y > 0 + (this.this_height * 0.5f) && dir.y < Screen.height - (this.this_height * 0.5f))
            {
                if (dir.x > Screen.width * 0.5f)
                {
                    dir.x = 0 + (this.this_width * 0.5f);
                }
                else
                {
                    dir.x = Screen.width - (this.this_width * 0.5f);
                }
            }
            else
            {
                if (dir.x > Screen.width - (this.this_width * 0.5f))
                {
                    dir.x = Screen.width - (this.this_width * 0.5f);
                }
                else if (dir.x < 0 + (this.this_width * 0.5f))
                {
                    dir.x = 0 + (this.this_width * 0.5f);
                }
            }

            if (dir.y > Screen.height - (this.this_height * 0.5f))
            {

                dir.y = 0 + (this.this_height * 0.5f);
            }
            else if (dir.y < 0 + (this.this_height * 0.5f))
            {

                dir.y = Screen.height - (this.this_height * 0.5f);
            }

            this.transform.position = dir;
        }
        //Debug.Log(dir.normalized);
    }

    void ThisImageSizeUpdate()
    {
        thisXscale = this.transform.lossyScale.x;
        thisYscale = this.transform.lossyScale.y;

        this_width = this.GetComponent<RectTransform>().rect.width * thisXscale;
        this_height = this.GetComponent<RectTransform>().rect.height * thisYscale;

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
