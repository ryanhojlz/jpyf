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
    [SerializeField] protected Vector3 m_from = new Vector3(45.0f, 0.0f, 0.0f);
    [SerializeField] protected Vector3 m_to = new Vector3(-45.0f, 0.0f, 0.0f);
    [SerializeField] protected float m_frequency = 10.0F;
    private void Start()
    {
        timer = despawnTimer;

        TargetObject = transform.parent.gameObject;

        this.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        this.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;

        //this.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
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

       // Debug.Log("Hi1");
       // Debug.Log(this.transform.GetChild(0));
       //Debug.Log(this.transform.GetChild(1));
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
            float zAngle = 360 - this.transform.GetChild(0).transform.GetChild(0).rotation.eulerAngles.z;
            Debug.Log(zAngle);
            //if ((zAngle < 90 && zAngle > 0) || (zAngle > 320 && zAngle < 360))
            //{
            //    Debug.Log("rotating one way round now");
            //    rotateWay = true;
            //}
            //if (zAngle < 35)
            //{
            //    Debug.Log("rotating the other way round now");
            //    rotateWay = false;
            //}
            //Debug.Log(rotateWay);
            //if (rotateWay) 
            //{
            //    transform.Rotate(this.transform.GetChild(0).transform.GetChild(0).forward, 10 * Time.deltaTime);
            //}
            //if (!rotateWay)  
            //{
            //    transform.Rotate(this.transform.GetChild(0).transform.GetChild(0).forward, - 10 * Time.deltaTime);
            //}

            Quaternion from = Quaternion.Euler(this.m_from);
            Quaternion to = Quaternion.Euler(this.m_to);

            float lerp = 0.5F * (1.0F + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup * this.m_frequency));
            this.transform/*.GetChild(0)*/.localRotation = Quaternion.Lerp(from, to, lerp);
            //this.transform.GetChild(0).transform.GetChild(0).localRotation = Quaternion.Lerp(from, to, lerp);
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
            this.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;

            //this.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void TargetFound()
    {
        timer = despawnTimer;
        this.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        //this.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
        this.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;

        //this.transform.GetChild(1).GetComponent<MeshRenderer>().transform.rotation = Arotation ;
    }
}
