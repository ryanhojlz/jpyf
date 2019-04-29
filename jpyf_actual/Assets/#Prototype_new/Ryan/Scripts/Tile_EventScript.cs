﻿using System.Collections;
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

    public float f_spawnTimer = 0;

    public int i_shrineHungerMeter = 0;

    // Object List
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
        // Update the light
        Lights_Update();
        Tile_EventStart();
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

    void Tile_EventStart()
    {
        // Event Start
        if (b_eventStart)
        {
            // Start spawning mobs
            f_spawnTimer -= 1 * Time.deltaTime;
            if (f_spawnTimer <= 0)
            {
                f_spawnTimer = 8.5f;
                SpawnEnemyRandomLocation(Random.Range(0, enemy_list.Count));
            }
            else
            {
                
            }

        }
        else // If not event Start
        {
            
        }


        if (i_shrineHungerMeter >= 2)
        {
            Destroy(this.gameObject);
        }
    }

    void SpawnEnemy(int id, Vector3 pos)
    {
        GameObject go = Instantiate(enemy_list[id].gameObject);
        go.GetComponent<NavMeshAgent>().Warp(pos);
    }

    void SpawnEnemyRandomLocation(int id)
    {
        if (handler.EnemyCount >= 15)
            return;
        GameObject go = Instantiate(enemy_list[id].gameObject);
        go.GetComponent<NavMeshAgent>().Warp(m_spawnList[Random.Range(0, m_spawnList.Count)].position);
    }

    public void Start_Event()
    {
        if (b_eventStart)
            return;
        b_eventStart = true;
    }

    public void UpShrineHunger(int value)
    {
        i_shrineHungerMeter += value;
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
