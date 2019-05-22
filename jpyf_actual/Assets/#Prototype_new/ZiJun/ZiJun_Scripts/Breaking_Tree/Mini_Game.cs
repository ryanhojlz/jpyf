using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mini_Game : MonoBehaviour
{
    public Image imgTimer;
    public Image img;
    public GameObject Manager;

    public GameObject Player;

    Object_Breaking objBreak = null;

    int counter = 1;
    //section
    int section = 3;

    int currentSection = 0;

    bool isActiveQTE = false;
    bool QTE_Completed = true;

    float timeLimit = 5f;

    float currentTime = 0f;
    float maxTimeLimit = 0f;

    float sourceAmout = 100f;

    float sourceCurrentAmount = 0f;
    float sourceMaxAmount = 0f;

    float powerHit = 0f;

    Object_ControlScript objControl = null;

    public static Mini_Game Instance = null;

    public List<Object_Breaking> m_OB_list = new List<Object_Breaking>();

    //For checking
    int index = -1;
    float nearest = float.MaxValue;
    float dist = 0;

    Vector3 OriPos = Vector3.zero;


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
   
    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("PS4_Player");
        objControl = GameObject.Find("PS4_ObjectHandler").GetComponent<Object_ControlScript>();
        //QTEstart();
        if (!imgTimer)
        {
            Debug.Log("You have forgotten to put an image in inspector for : " + this.name);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (m_OB_list.Count > 0)
        {
            UI_FeedbackScript.Instance.InteractTrue[0] = true;
        }
        else
        {
            UI_FeedbackScript.Instance.InteractTrue[0] = false;
        }

        if (Object_ControlScript.Instance.checkCanGatherItem)
        {
            //Debug.Log("Got Come in");
            QTEstart(FindNearestInList());
        }

        if (!isActiveQTE)
        {
            Manager.SetActive(false);// = false;
            return;
        }
        else
        {
            Manager.SetActive(true);
        }

        if (objBreak)
        {
            if (!imgTimer || !img)
            {
                Debug.Log("some image cannot be found");
                return;
            }

            sourceCurrentAmount -= 10 * currentSection * Time.deltaTime;

            currentTime -= Time.deltaTime;
            imgTimer.fillAmount = currentTime / maxTimeLimit;
            img.fillAmount = sourceCurrentAmount / sourceMaxAmount;

            currentSection = 1 + (int)(section * img.fillAmount);

            //Debug.Log((int)(section * img.fillAmount))

            //if (imgTimer.fillAmount <= 0 || img.fillAmount >= 1)
            //{
            //    //counter = (int)(section * img.fillAmount);
            //    isActiveQTE = false;
            //    objBreak.SetComplete(1 + currentSection);//To always spawn at least once
            //}

            if (imgTimer.fillAmount <= 0 && img.fillAmount < 1)
            {
                objBreak.SetComplete(false);//To always spawn at least once
                isActiveQTE = false;
            }
            else if (img.fillAmount >= 1)
            {
                objBreak.SetComplete(true);
                isActiveQTE = false;
            }

            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    BreakObject();
            //}


            if (objControl.checkCanGatherItem)
            {
                BreakObject();
            }

        }

        //if (Player && isActiveQTE)
        //{
        //    if (Player.GetComponent<Rigidbody>())
        //        if (Player.GetComponent<Rigidbody>().velocity.magnitude > 1f)
        //            isActiveQTE = false;
        //}

        if (Player && isActiveQTE)
        {
            if ((Player.transform.position - OriPos).magnitude > 0.1f)
            {
                isActiveQTE = false;
            }
        }
    }

    public void QTEstart(Object_Breaking other)
    {
        // If player carrying object

        //Debug.Log("Name : " + other);

        if (!other)
        {
            UI_FeedbackScript.Instance.InteractTrue[0] = false;
            return;

        }

        OriPos = Player.transform.position;

        //Debug.Log("Hehe its working till here");
        UI_FeedbackScript.Instance.InteractTrue[0] = true;
        if (PickupHandlerScript.Instance.ReturnCurrentObject() == null)
        {
            if (other && !isActiveQTE)
            {
                objBreak = other;
                isActiveQTE = true;
                currentTime = other.GetTimeLimit();
                maxTimeLimit = other.GetTimeLimit();

                sourceCurrentAmount = other.GetMaxSpamPoint() * 0.5f;
                sourceMaxAmount = other.GetMaxSpamPoint();

                powerHit = other.GetPowerPerHit();
            }
        }
        
        
    }

    public void QTEstop()
    {
        isActiveQTE = false;
    }

    // Breaking Object
    public void BreakObject()
    {
        sourceCurrentAmount += powerHit;
        if(objBreak)
            objBreak.PlayHit();
    }

    public void AddToList(Object_Breaking obj)
    {
        if (!m_OB_list.Contains(obj))
        {
            m_OB_list.Add(obj);
        }
    }

    public void RemoveFromList(Object_Breaking obj)
    {
        if (m_OB_list.Contains(obj))
        {
            m_OB_list.Remove(obj);
        }
    }

    private Object_Breaking FindNearestInList()
    {
        index = -1;
        nearest = float.MaxValue;

        for (int i = 0; i < m_OB_list.Count; ++i)
        {
            dist = (Player.transform.position - m_OB_list[i].transform.position).magnitude;
            if (dist < nearest)
            {
                nearest = dist;
                index = i;
            }
        }

        if (index < 0)
            return null;

        return m_OB_list[index];
    }

    public Transform GetNearestBreakingObj()
    {
        if(FindNearestInList())
            return FindNearestInList().transform;

        return null;
    }
}
