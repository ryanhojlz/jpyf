using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Third_Person_Camera : MonoBehaviour {

    public GameObject targetedObject;
    public float upOffset = 0;
    public float forwardOffset = 0;
    public float sideOffset = 0;

    public float rotationSpeed = 100;

    public GameObject Ball;

    public float MaxRange = 0;

    public LayerMask myLayerMask;

    Vector3 ForwardDirection = Vector3.zero;
    Vector3 UpDirection = Vector3.zero;
    Vector3 SideDirection = Vector3.zero;

    GameObject Target = null;




    Vector2 PositionShootFrom = new Vector2(Screen.width * 0.5f, Screen.height * 0.7f);

    Vector3 PrevPos = Vector3.zero;

    // Use this for initialization
    void Start ()
    {
        //this.transform.position = new Vector3(
        //    targetedObject.transform.position.x + targetedObject.transform.forward.x * sideOffset,
        //    targetedObject.transform.position.y + targetedObject.transform.forward.y * upOffset,
        //    targetedObject.transform.position.z + targetedObject.transform.forward.z * forwardOffset
        //    );
        ForwardDirection = this.transform.forward * forwardOffset;
        UpDirection = this.transform.up * upOffset;
        SideDirection = this.transform.right * sideOffset;

        this.transform.position = targetedObject.transform.position + ForwardDirection + UpDirection + SideDirection;

        PrevPos = targetedObject.transform.position;

        MaxRange = (this.transform.position - targetedObject.transform.position).magnitude + targetedObject.GetComponent<BasicGameOBJ>().rangeValue;

        //Target = Instantiate(Ball, GetRayCastHitPosition(), this.transform.rotation);
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = transform.position + (targetedObject.transform.position - PrevPos);
        NavMeshAgent agent = targetedObject.GetComponent<NavMeshAgent>();

        PositionShootFrom = new Vector2(Screen.width * 0.5f, Screen.height * 0.7f);

        if (agent)
        {
            agent.enabled = false;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Rotation(0, -rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Rotation(0, rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Rotation(-rotationSpeed, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Rotation(rotationSpeed, 0);
        }

        Vector3 Move = new Vector3(this.transform.forward.x, 0, this.transform.forward.z);

        //targetedObject.GetComponent<Rigidbody>().AddForce(Move * 10);
        if (Input.GetKey(KeyCode.W))
        {
            //targetedObject.GetComponent<Rigidbody>().AddForce(Move * 100);
            targetedObject.transform.position = GetRayCastHitPosition();
        }
        else
        {
            //targetedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if (targetedObject.GetComponent<Rigidbody>().velocity != Vector3.zero)
        {
            var q = Quaternion.LookRotation((targetedObject.GetComponent<Rigidbody>().velocity.normalized + this.transform.position) - transform.position);
            targetedObject.transform.rotation = Quaternion.RotateTowards(targetedObject.transform.rotation, q, agent.angularSpeed * Time.deltaTime);
        }

        //Debug.Log(Input.mousePosition);
        //Debug.Log(Screen.width * 0.5);
        //Debug.Log(Screen.height * 0.5);

        //Target.transform.position = GetRayCastHitPosition();

        PrevPos = targetedObject.transform.position;

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    //targetedObject.GetComponent<Minion>().ShootTargetedPos(GetRayCastHitPosition());
        //    if(targetedObject.GetComponent<Minion>())
        //    targetedObject.GetComponent<Minion>().ShootTargetedPos(GetRayCastHitPosition());
        //}


        //Instantiate(Ball, GetRayCastHitPosition(), this.transform.rotation);
    }

    void Rotation(float x, float y)
    {
        transform.RotateAround(targetedObject.transform.position, Vector3.up, x * Time.deltaTime);
        transform.RotateAround(targetedObject.transform.position, this.transform.right, y * Time.deltaTime);
    }

    public Vector3 GetRayCastHitPosition()
    {

        RaycastHit hit = new RaycastHit();

        Vector3 PointHit = Vector3.zero;

        Ray CrosshairHit = this.GetComponent<Camera>().ScreenPointToRay(PositionShootFrom);//new Ray(this.transform.position, this.transform.forward);

        //this.GetComponent<Camera>().ScreenPointToRay(PositionShootFrom);

        Physics.Raycast(CrosshairHit, out hit);

        PointHit = hit.point;

        if (hit.point == Vector3.zero)
        {
            //Debug.Log("hehe");
            //PointHit = CrosshairHit.GetPoint(MaxRange);
            return CrosshairHit.GetPoint(MaxRange);
        }
        //this.transform.forward;
        return hit.point;

        Ray ObjectRayCast = new Ray(targetedObject.transform.position, (PointHit - targetedObject.transform.position).normalized);

        Physics.Raycast(ObjectRayCast, out hit, targetedObject.GetComponent<BasicGameOBJ>().rangeValue);

        if (hit.point == Vector3.zero)
        {
            //Debug.Log("hehe");
            return ObjectRayCast.GetPoint(targetedObject.GetComponent<BasicGameOBJ>().rangeValue);
        }

        return hit.point;
    }
}
