using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alert : MonoBehaviour
{
    public GameObject TargetObject;//The object that is using this prompt
    public Transform TargetedObject = null;
    public float despawnTimer = 4;
    float timer;
    float deltaTimeIncrement = 0;
    public GameObject exclamationMark;
    bool rotateWay;

    private void Start()
    {
        timer = despawnTimer;

        TargetObject = transform.parent.gameObject;

        this.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        this.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
    }

    public void SetPromptTarget(GameObject character)
    {
        TargetObject = character;
    }

    private void Awake()
    {

    }

    private void Update()
    {
        //Color tmp = spriteRenderer.color;
        //tmp.a -= 1.5f * Time.deltaTime;
        //spriteRenderer.color = tmp;

        Debug.Log("Hi1");
        Debug.Log(this.transform.GetChild(0));
        Debug.Log(this.transform.GetChild(1));
        if (TargetObject.GetComponent<Entity_Unit>())
        {
            Debug.Log("Hi2");

            if (TargetObject.GetComponent<Entity_Unit>().GetTarget() && TargetObject.GetComponent<Entity_Unit>().GetTarget() != TargetedObject)
            {
                TargetedObject = TargetObject.GetComponent<Entity_Unit>().GetTarget();
                TargetFound();
            }
            else if (!TargetObject.GetComponent<Entity_Unit>().GetTarget())
            {
                TargetedObject = null;
            }


            transform.parent.eulerAngles = new Vector3(0, 0, 0);
            float zAngle = 360 - this.transform.GetChild(1).rotation.eulerAngles.z;
            Debug.Log(-zAngle);
            if (zAngle < -35)
            {
                rotateWay = true;
            }
            if (zAngle > 35)
            {
                Debug.Log("hi");
                rotateWay = false;
            }

            if (rotateWay) 
            {
                transform.Rotate(this.transform.GetChild(1).forward, 10 * Time.deltaTime);
            }
            if (!rotateWay)  
            {
                transform.Rotate(this.transform.GetChild(1).forward, - 10 * Time.deltaTime);
            }
        }

        //if (TargetObject)
        //{

        //}
        //else
        //{
        //    //Destroy(gameObject);
        //    this.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        //    this.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
        //}

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Debug.Log("Hi3");
            this.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            this.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void TargetFound()
    {
        timer = despawnTimer;
        this.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        this.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
        
        //this.transform.GetChild(1).GetComponent<MeshRenderer>().transform.rotation = Arotation ;
    }
}
