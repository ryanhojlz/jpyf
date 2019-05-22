using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public int shrineHungerCap = 0;
    // Object List
    public List<Transform> m_spawnList;
    public List<GameObject> enemy_list;


    public List<GameObject> spawnedEnemies;
    // Use this for initialization
    void Start ()
    {
        m_wallObject = transform.Find("Wall");
        m_shrine = transform.Find("Shrine");
        handler = GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>();

        m_lightObj = transform.Find("Shrine").GetChild(0).GetComponent<Light>();

        for (int i = 0; i < transform.Find("SpawnPoints").transform.childCount; ++i)
        {
            if (transform.Find("SpawnPoints").transform.GetChild(i).gameObject.activeInHierarchy)
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
        if (b_eventStart)
        {
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
        else
        {
            m_lightObj.intensity = 0;
        }
        
        
    }

    void Tile_EventStart()
    {
        // Event Start
        if (b_eventStart)
        {

            transform.Find("Shrine").transform.GetChild(1).gameObject.SetActive(true);
            transform.Find("Shrine").transform.GetChild(1).gameObject.GetComponent<TextMesh>().text = "";


            // Start spawning mobs
            f_spawnTimer -= 1 * Time.deltaTime;
            if (f_spawnTimer <= 0)
            {
                if (GameEventsPrototypeScript.Instance.f_difficulty < 1)
                {
                    f_spawnTimer = 12.0f;
                }
                else if (GameEventsPrototypeScript.Instance.f_difficulty < 2)
                {
                    f_spawnTimer = 10.0f;
                }
                else if (GameEventsPrototypeScript.Instance.f_difficulty < 3)
                {
                    f_spawnTimer = 8.0f;
                }
                else if (GameEventsPrototypeScript.Instance.f_difficulty < 3)
                {
                    f_spawnTimer = 6.0f;
                }
                else
                {
                    f_spawnTimer = 5.0f;
                }

                
                SpawnEnemyRandomLocation(Random.Range(0, enemy_list.Count));
                
            }
        }
        else // If not event Start
        {
            
        }


        if (Input.GetKeyDown(KeyCode.Colon))
        {
            i_shrineHungerMeter = shrineHungerCap + (int)GameEventsPrototypeScript.Instance.f_difficulty;
        }


        // Win condition shrine
        if (i_shrineHungerMeter >= shrineHungerCap) 
        {
            GameEventsPrototypeScript.Instance.f_difficulty++;
            //Debug.Log("Difficulty " + GameEventsPrototypeScript.Instance.f_difficulty);
            GameEventsPrototypeScript.Instance.TileEvent_Start = false;

            var DestroyList = GameObject.FindObjectsOfType<Pickup_Scripts>();
            foreach (Pickup_Scripts g in DestroyList)
            {
                if (g.id != 5)
                    continue;
                GameObject.Destroy(g.gameObject);
            }

            foreach (GameObject g in spawnedEnemies)
            {
                if (!g)
                    continue;
                Destroy(g.gameObject);

            }

            Destroy(this.gameObject);
        }
    }

    void SpawnEnemy(int id, Vector3 pos)
    {
        GameObject go = Instantiate(enemy_list[id].gameObject);
        go.GetComponent<NavMeshAgent>().Warp(pos);
        go.transform.parent = this.transform;
    }

    public void SpawnEnemyRandomLocation(int id)
    {
        //if (handler.EnemyCount >= 5)
        //    return;

        //if (GameEventsPrototypeScript.Instance.f_difficulty > 2)
        //{
        //    GameObject go = Instantiate(enemy_list[1].gameObject) as GameObject;
        //    go.GetComponent<NavMeshAgent>().Warp(m_spawnList[Random.Range(0, m_spawnList.Count)].position);
        //    GameObject go2 = Instantiate(enemy_list[1].gameObject) as GameObject;
        //    go2.GetComponent<NavMeshAgent>().Warp(m_spawnList[Random.Range(0, m_spawnList.Count)].position);

        //}
        //else if (GameEventsPrototypeScript.Instance.f_difficulty < 2)
        //{
        //    GameObject go = Instantiate(enemy_list[1].gameObject) as GameObject;
        //    go.GetComponent<NavMeshAgent>().Warp(m_spawnList[Random.Range(0, m_spawnList.Count)].position);
        //}
        if (Stats_ResourceScript.Instance.EnemyCount > 15)
        {
            return;
        }

        if (GameEventsPrototypeScript.Instance.Tutorial <= 5)
        {
            GameObject go = Instantiate(enemy_list[1].gameObject) as GameObject;
            go.GetComponent<NavMeshAgent>().Warp(m_spawnList[Random.Range(0, m_spawnList.Count)].position);
            spawnedEnemies.Add(go);

        }
        else
        {
            GameObject go = Instantiate(enemy_list[id].gameObject) as GameObject;
            go.GetComponent<NavMeshAgent>().Warp(m_spawnList[Random.Range(0, m_spawnList.Count)].position);
            spawnedEnemies.Add(go);
        }

    }

    public void Start_Event()
    {
        if (b_eventStart)
            return;
        b_eventStart = true;
        GameEventsPrototypeScript.Instance.TileEvent_Start = true;
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
