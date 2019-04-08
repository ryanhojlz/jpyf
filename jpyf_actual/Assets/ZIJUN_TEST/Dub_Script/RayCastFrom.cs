using System.Collections;
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
        if (targetedObject.gameObject)
        {
            float ObjToRayObj = (this.transform.position - targetedObject.transform.position).magnitude;
            float Range = targetedObject.GetComponent<BasicGameOBJ>().rangeValue;

            //MaxRange = Mathf.Sqrt((ObjToRayObj * ObjToRayObj) + (Range * Range));//(this.transform.position - targetedObject.transform.position).magnitude + targetedObject.GetComponent<BasicGameOBJ>().rangeValue;

            MaxRange = Mathf.Sqrt((Range * Range) - (ObjToRayObj * ObjToRayObj));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //targetedObject.GetComponent<Minion>().ShootTargetedPos(GetRayCastHitPosition());
                if (targetedObject.GetComponent<Minion>())
                    targetedObject.GetComponent<Minion>().ShootTargetedPos(GetRayCastHitPosition());
            }

            Debug.Log(Mathf.Sqrt((ObjToRayObj * ObjToRayObj) + (Range * Range)));

            Target.transform.position = GetRayCastHitPosition();
        }
    }

    public void SetShooter(GameObject Shooter)//Use This function the set variable for unit that is shooting
    {
        targetedObject = Shooter;
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
        Debug.DrawRay(this.transform.position, this.transform.forward, Color.red);
        
        PointHit = hit.point;

        Physics.queriesHitTriggers = true;

        

        if (hit.point == Vector3.zero)
        {
            //Debug.Log("hehe");
            //PointHit = CrosshairHit.GetPoint(MaxRange);
            Debug.DrawLine(this.transform.position, CrosshairHit.GetPoint(MaxRange), Color.green);
            return CrosshairHit.GetPoint(MaxRange);
        }
        //this.transform.forward;
        Debug.DrawLine(this.transform.position, hit.point, Color.green);
        return hit.point;
    }
}
