using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternItemCreationScript : MonoBehaviour
{
    // Prefab reference
    public GameObject Capsule = null;
    // Handler reference
    public Stats_ResourceScript handler = null;

    // Object pointer for initialization
    public GameObject spiritObject = null;

    //Object on me
    public GameObject objectinplace = null;
    
    Vector3 originPoint = Vector3.zero;
    
    // Use this for initialization
	void Start ()
    {
        handler = GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>();
        SpawnPickUp();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (objectinplace == null)
        {
            if (handler.m_Souls > 10)
            {
                handler.ConsumeSouls(10);
                SpawnPickUp();
            }
        }
		
	}

    void SpawnPickUp()
    {
        spiritObject = Instantiate(Capsule) as GameObject;
        spiritObject.transform.localPosition = Vector3.zero;
        originPoint = this.transform.position;
        originPoint.y += 0.1f;
        spiritObject.transform.position = originPoint;
        objectinplace = spiritObject;

        spiritObject.transform.parent = this.transform;
    }

     
}
