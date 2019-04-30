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

    float this_width = 0f;
    float this_height = 0f;
    float thisXscale = 1f;
    float thisYscale = 1f;

    Color colorForAlpha = new Color();
    Stats_ResourceScript resourceHandler = null;

    // Use this for initialization
    void Start ()
    {
        resourceHandler = Stats_ResourceScript.Instance;
        target = GameObject.Find("PayLoad");
        cam = GameObject.Find("ControllerCam").GetComponent<Camera>();
        //canvas 
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (((float)resourceHandler.m_CartHP / (float)resourceHandler.m_CartHpCap) > 0.25f)
        {
            colorForAlpha = this.GetComponent<Image>().color;
            colorForAlpha.a = 0;
            this.GetComponent<Image>().color = colorForAlpha;
        }
        else
        {
            colorForAlpha = this.GetComponent<Image>().color;
            colorForAlpha.a = 1;
            this.GetComponent<Image>().color = colorForAlpha;
        }

        ThisImageSizeUpdate();
        //Debug.Log(target.name);
        Vector3 dir = cam.WorldToScreenPoint(target.transform.position);

        Vector3 forward = Camera.main.transform.forward;
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

            //if (dir.y > Screen.height - (this.this_height * 0.5f))
            //{
            //    dir.y = Screen.height - (this.this_height * 0.5f);
            //}
            //else if (dir.y < 0 + (this.this_height * 0.5f))
            //{
            //    dir.y = 0 + (this.this_height * 0.5f);
            //}

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
}
