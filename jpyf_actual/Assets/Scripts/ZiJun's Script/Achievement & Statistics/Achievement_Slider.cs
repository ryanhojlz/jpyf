using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement_Slider : MonoBehaviour
{

    //Rigidbody rb;
    //// Use this for initialization
    //void Start()
    //{
    //    rb = this.GetComponent<Rigidbody>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    rb.velocity = new Vector3(500, 0, 0);
    //}

    //public float LifeTime = 7f;
    //public float MaxXPos = 110f;
    //public float StayTime = 3f;
    //float OrigialLT = 0f;
    //float speed = 0f;

    //GameObject Canvas;

    //private void Start()
    //{
    //    OrigialLT = LifeTime;
    //    Canvas = this.transform.parent.gameObject;

    //    MaxXPos = Canvas.transform.position.x - (Canvas.transform.localScale.x * (Canvas.GetComponent<RectTransform>().rect.width * 0.5f) - (this.transform.localScale.x * this.GetComponent<RectTransform>().rect.width * 0.5f));

    //    speed = Mathf.Sqrt((this.transform.position.x - MaxXPos) * (this.transform.position.x - MaxXPos)) / ((LifeTime - StayTime) * 0.5f);

    //    //Debug.Log("Name of OBJ : " + Canvas.name);
    //}

    //private void Update()
    //{
    //    LifeTime -= Time.deltaTime;
    //    if (this.transform.position.x < MaxXPos && LifeTime > (LifeTime - StayTime) * 0.5f + StayTime)
    //    {
    //        this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    //    }
    //    else if (LifeTime < (LifeTime - StayTime) * 0.5f + StayTime)
    //    {
    //        this.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
    //    }

    //    //Debug.Log(transform.position);
    //}

    GameObject Canvas;

    GameObject OriginalPosition;//The original position of the object
    float distanceBetween = 0f;//Distance Between the point & the object
    float EndPositionX = 0f;

    public float LifeTime = 7f;
    public float StayTime = 3f;

    float TimeToReach = 0f;
    float ReturnTime = 0f;

    float TimeRequiredToReach = 0f;

    float movespeed = 0f;

    private void Start()
    {
        //Calculation for all screen size (Not adeptive after spawned)
        Canvas = this.transform.parent.gameObject;

        OriginalPosition = new GameObject();
        OriginalPosition.transform.position = new Vector3((Canvas.transform.position.x - (Canvas.GetComponent<RectTransform>().rect.width * 0.5f * Canvas.transform.lossyScale.x)) - (this.GetComponent<RectTransform>().rect.width * 0.5f * this.transform.lossyScale.x), this.transform.position.y, this.transform.position.z);
        this.transform.position = OriginalPosition.transform.position;
        OriginalPosition.transform.parent = this.transform.parent;

        TimeRequiredToReach = (LifeTime - StayTime) * 0.5f;
        TimeToReach = LifeTime - TimeRequiredToReach;
        ReturnTime = TimeRequiredToReach;

        EndPositionX = (Canvas.transform.position.x - Canvas.GetComponent<RectTransform>().rect.width * 0.5f * Canvas.transform.lossyScale.x) + (this.GetComponent<RectTransform>().rect.width * 0.5f * this.transform.lossyScale.x);
        distanceBetween = Mathf.Sqrt((OriginalPosition.transform.position.x - EndPositionX) * (OriginalPosition.transform.position.x - EndPositionX));
        movespeed = distanceBetween / TimeRequiredToReach;
    }

    private void Update()
    {
        //Debug.Log(distanceBetween);

        LifeTime -= Time.deltaTime;

        if (LifeTime > TimeToReach)
        {
            this.transform.position += new Vector3(movespeed * Time.deltaTime, 0, 0);
        }
        else if (LifeTime < ReturnTime)
        {
            this.transform.position -= new Vector3(movespeed * Time.deltaTime, 0, 0);
        }
        //Debug.Log("Width of canvas : " + Canvas.GetComponent<RectTransform>().rect.width);
        //Debug.Log("Size of canvas : " + Canvas.GetComponent<RectTransform>().rect.size);
        //Debug.Log("Position of canvas : " + Canvas.transform.position);
    }
}
