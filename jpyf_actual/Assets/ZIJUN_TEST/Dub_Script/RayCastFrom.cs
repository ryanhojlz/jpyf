﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastFrom : MonoBehaviour {

    public float MaxRange = 0;
    public GameObject targetedObject;

    public GameObject Ball;

    GameObject Target = null;

    // Update is called once per frame


    private void Start()
    {
        Target = Instantiate(Ball, GetRayCastHitPosition(), this.transform.rotation);
    }

    void Update ()
    {
        //this.forward;	

        float ObjToRayObj = (this.transform.position - targetedObject.transform.position).magnitude;
        float Range = targetedObject.GetComponent<BasicGameOBJ>().rangeValue;

        MaxRange = Mathf.Sqrt(ObjToRayObj * ObjToRayObj + Range * Range);//(this.transform.position - targetedObject.transform.position).magnitude + targetedObject.GetComponent<BasicGameOBJ>().rangeValue;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            //targetedObject.GetComponent<Minion>().ShootTargetedPos(GetRayCastHitPosition());
            if (targetedObject.GetComponent<Minion>())
                targetedObject.GetComponent<Minion>().ShootTargetedPos(GetRayCastHitPosition());
        }

        Target.transform.position = GetRayCastHitPosition();
    }

    public Vector3 GetRayCastHitPosition()
    {

        //RaycastHit hit = new RaycastHit();

        //Vector3 PointHit = Vector3.zero;

        //Debug.Log(this.transform.forward);

        //Ray CrosshairHit = this.GetComponent<Camera>().ScreenPointToRay(PositionShootFrom);//new Ray(this.transform.position, this.transform.forward);
        //Ray CrosshairHit = new Ray(this.transform.position, this.transform.forward);
        //this.GetComponent<Camera>().ScreenPointToRay(PositionShootFrom);

        //Physics.Raycast(CrosshairHit, out hit);

        //PointHit = hit.point;

        //if (hit.point == Vector3.zero)
        //{
        //    return CrosshairHit.GetPoint(MaxRange);
        //}
        //this.transform.forward;
        //return Vector3.zero;

        //Ray ObjectRayCast = new Ray(targetedObject.transform.position, (PointHit - targetedObject.transform.position).normalized);

        //Physics.Raycast(ObjectRayCast, out hit, targetedObject.GetComponent<BasicGameOBJ>().rangeValue);

        //if (hit.point == Vector3.zero)
        //{
        //    //Debug.Log("hehe");
        //    return ObjectRayCast.GetPoint(targetedObject.GetComponent<BasicGameOBJ>().rangeValue);
        //}

        //return hit.point;

        RaycastHit hit = new RaycastHit();

        Vector3 PointHit = Vector3.zero;

        Ray CrosshairHit = new Ray(this.transform.position, this.transform.forward);

        //this.GetComponent<Camera>().ScreenPointToRay(PositionShootFrom);

        Physics.queriesHitTriggers = false;

        Physics.Raycast(CrosshairHit, out hit, MaxRange);

        PointHit = hit.point;

        Physics.queriesHitTriggers = true;

        if (hit.point == Vector3.zero)
        {
            //Debug.Log("hehe");
            //PointHit = CrosshairHit.GetPoint(MaxRange);
            return CrosshairHit.GetPoint(MaxRange);
        }
        //this.transform.forward;
        return hit.point;
    }
}
