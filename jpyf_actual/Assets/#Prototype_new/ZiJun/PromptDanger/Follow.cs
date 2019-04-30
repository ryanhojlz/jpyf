using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Follow : MonoBehaviour {

    [SerializeField]
    GameObject target = null;

    [SerializeField]
    Camera cam = null;

    [SerializeField]
    Canvas canvas = null;

    [SerializeField]
    Sprite low_hp;

    [SerializeField]
    Sprite low_lantern;

    [SerializeField]
    float delayTime = 2f;

    bool b_low_hp = false;
    bool b_low_lantern = false;


    float this_width = 0f;
    float this_height = 0f;
    float thisXscale = 1f;
    float thisYscale = 1f;

    float previousTime = 0f;

    bool selectHealth = true;

    Color colorForAlpha = new Color();
    Stats_ResourceScript resourceHandler = null;

    // Use this for initialization
    void Start ()
    {
        resourceHandler = Stats_ResourceScript.Instance;
        target = GameObject.Find("PayLoad");
        cam = GameObject.Find("ControllerCam").GetComponent<Camera>();
        //this.GetComponent<Image>().sprite = low_hp;
        //canvas 
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!this.GetComponent<Image>())
            return;

        if ((float)resourceHandler.m_CartHP / (float)resourceHandler.m_CartHpCap < 0.25f)
        {
            b_low_hp = true;
        }
        else
        {
            b_low_hp = false;
        }

        if ((float)resourceHandler.m_LanternHp / (float)resourceHandler.m_LanternHpCap < 0.25f)
        {
            b_low_lantern = true;
        }
        else
        {
            b_low_lantern = false;
        }

        if (b_low_hp || b_low_lantern)
        {
            SetAlpha(1f);

            if (b_low_hp && b_low_lantern)
            {
                if (selectHealth)
                {
                    this.GetComponent<Image>().sprite = low_hp;
                }
                else
                {
                    this.GetComponent<Image>().sprite = low_lantern;
                }

                if (previousTime + delayTime < Time.time)
                {
                    previousTime = Time.time;
                    selectHealth = !selectHealth;
                }
            }
            else if (b_low_hp)
            {
                this.GetComponent<Image>().sprite = low_hp;
                UpdateHealthSelected();
            }
            else if (b_low_lantern)
            {
                this.GetComponent<Image>().sprite = low_lantern;
                UpdateHealthSelected();
            }
        }
        else
        {
            SetAlpha(0f);
        }

        ThisImageSizeUpdate();
        //Debug.Log(target.name);
        Vector3 dir = cam.WorldToScreenPoint(target.transform.position);

        Vector3 forward = cam.transform.forward;
        Vector3 towardsTarget = target.transform.position - cam.transform.position;

        //Debug.Log("Forward : " + forward);

        //Debug.Log(Vector3.Dot(towardsTarget.normalized, forward));

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
        this.GetComponent<Image>().sprite = low_lantern;
        colorForAlpha = this.GetComponent<Image>().color;
        colorForAlpha.a = value;
        this.GetComponent<Image>().color = colorForAlpha;
    }

    void UpdateHealthSelected()
    {
        selectHealth = !b_low_hp;
        previousTime = Time.time;
    }
}
