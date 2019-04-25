using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_EventScript : MonoBehaviour
{
    public Transform m_wallObject = null;
    public Transform m_shrine = null;
    public List<Transform> m_spawnList;
    public Stats_ResourceScript handler;
    public Light m_lightObj = null;
    public bool b_eventStart = false;

    // Use this for initialization
    void Start ()
    {
        m_wallObject = transform.Find("Wall");
        m_shrine = transform.Find("Shrine");
        handler = GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>();

        m_lightObj = transform.Find("Shrine").GetChild(0).GetComponent<Light>();

        for (int i = 0; i < transform.Find("SpawnPoints").transform.childCount; ++i)
        {
            m_spawnList.Add(transform.Find("SpawnPoints").transform.GetChild(i));
        }
    }


    // Update is called once per frame
    void Update ()
    {
        
	}

    void Lights_Update()
    {
        if (m_lightObj.intensity > 0)
        {
            m_lightObj.intensity -= 1 * Time.deltaTime;
            
        }
        
    }
}
