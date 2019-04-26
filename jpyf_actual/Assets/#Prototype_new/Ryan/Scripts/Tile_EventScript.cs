using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Tile_EventScript : MonoBehaviour
{
    public Transform m_wallObject = null;
    public Transform m_shrine = null;
    public Stats_ResourceScript handler;
    public Light m_lightObj = null;
    public bool b_eventStart = false;
    bool b_dimlights = false;

    public List<Transform> m_spawnList;
    public List<GameObject> enemy_list;


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
        Lights_Update();

        funcDebugg();
	}

    void Lights_Update()
    {
        // Update the lights so the players can tell something is there

        if (!b_dimlights)
        {
            m_lightObj.intensity -= 2 * Time.deltaTime;
            if (m_lightObj.intensity <= 0)
            {
                b_dimlights = true;
            }
        }
        else if (b_dimlights)
        {
            m_lightObj.intensity += 2 * Time.deltaTime;
            if (m_lightObj.intensity >= 5)
            {
                b_dimlights = false;
            }
        }
        
    }


    void SpawnEnemy(int id, Vector3 pos)
    {
        GameObject go = Instantiate(enemy_list[id].gameObject);
        go.GetComponent<NavMeshAgent>().Warp(pos);
    }


    void funcDebugg()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnEnemy(0, m_spawnList[0].position);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnEnemy(1, m_spawnList[1].position);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpawnEnemy(2, m_spawnList[2].position);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SpawnEnemy(2, m_spawnList[3].position);
        }
    }
}
