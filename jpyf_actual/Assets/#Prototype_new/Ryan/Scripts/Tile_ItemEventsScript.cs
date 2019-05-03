using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_ItemEventsScript : MonoBehaviour
{
    // Object list
    public List<Transform> Objects;
    public List<Transform> m_spawnList;

    float random = 0;
    int spawnId = 0; 
    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < transform.Find("SpawnPoints").transform.childCount; ++i)
        {
            m_spawnList.Add(transform.Find("SpawnPoints").transform.GetChild(i));
        }
        SpawnLocation();
    }

    //// Update is called once per frame
    //void Update ()
    //   {

    //}


    public void SpawnLocation()
    {
        for (int i = 0; i < m_spawnList.Count; ++i)
        {
            switch (i)
            {
                case 0:
                    spawnId = 0;
                    break;
                case 1:
                    spawnId = 0;
                    break;
                case 2:
                    spawnId = 1;
                    break;
                case 3:
                    spawnId = 1;
                    break;
            }

            // Spawned item
            //random = Random.Range(0, 10);
            //if (random > 5)
            //{
            //    spawnId = 0;
            //}
            //else
            //{
            //    spawnId = 1;
            //}


            Transform spawneditem = Instantiate(Objects[spawnId]);
            spawneditem.position = m_spawnList[i].transform.position;
           
        }
    }
}
