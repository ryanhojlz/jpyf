using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Breaking : MonoBehaviour
{
    bool m_Player = false;
    int m_SuccessAmount = 0;

    protected float m_timelimit = 5f;
    protected float m_maxSpamPoint = 100f;

    protected float m_powerPerHit = 10f;

    bool m_successGather = false;
    int m_totalResource = 1;

    [SerializeField]
    Pickup_Scripts ObjPrefeb = null;

    List<GameObject> WithinRange = new List<GameObject>();

    Mini_Game minigame = null;
    // Use this for initialization
    void Start()
    {
        minigame = Mini_Game.Instance;

        m_powerPerHit = 25;
        //Debug.Log("Hi this time i got come in liao");
    }

    // Update is called once per frame
    void Update()
    {
        if (m_successGather)
        {
            SpawnObject();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PickupHandlerScript>())
            minigame.AddToList(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PickupHandlerScript>())
            minigame.RemoveFromList(this);
    }

    public virtual void CollectMaterial()
    {
        if (m_Player)
        {
            GameObject.Find("QTE_Collecting_Object").GetComponent<Mini_Game>().QTEstart(this);            
        }
    }

    public void SpawnObject()
    {
        if (ObjPrefeb)
        {
            for (int i = 0; i < m_totalResource; ++i)
            {           

                Pickup_Scripts item = Instantiate(ObjPrefeb);
                item.transform.position = transform.position;
                var temppos = item.transform.position;
                temppos.y += item.GetComponent<Collider>().bounds.size.y / 2;
                item.transform.position = temppos;
            }
            minigame.RemoveFromList(this);
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("No Object Prefeb is found to spawn for " + this.name);
        }
    }

    public float GetTimeLimit()
    {
        return m_timelimit;
    }

    public float GetMaxSpamPoint()
    {
        return m_maxSpamPoint;
    }

    public float GetPowerPerHit()
    {
        return m_powerPerHit;
    }

    //public void SetComplete(int _resourceAmount)
    //{
    //    m_successGather = true;
    //    m_totalResource = _resourceAmount;
    //}

    public void SetComplete(bool success)
    {
        m_successGather = success;
        m_totalResource = 1;
    }
}
