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

    // Use this for initialization
    void Start()
    {
        m_powerPerHit = 25;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Player)
        {
           // Debug.Log("I AM INSIDE");
        }
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    CollectMaterial();
        //}

        if (Object_ControlScript.Instance.checkCanGatherItem)
        {
            CollectMaterial();
        }


        if (m_successGather)
        {
            SpawnObject();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player2")
        {
            //WithinRange
            m_Player = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player2")
        {
            m_Player = false;
        }
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
                //ItemSpawnHandlerScript tempObj = GameObject.Find("ItemSpawnPoint").GetComponent<ItemSpawnHandlerScript>();
                //tempObj.SpawnItem(this.gameObject.transform.position);

                Pickup_Scripts item = Instantiate(ObjPrefeb);
                item.transform.position = transform.position;
            }

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

    public void SetComplete(int _resourceAmount)
    {
        m_successGather = true;
        m_totalResource = _resourceAmount;
    }
}
