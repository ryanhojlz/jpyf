using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SpawnHandlerScript : MonoBehaviour
{
    public List<Transform> ObjectList;
    public List<Transform> SpawnLocation;
    public Stats_ResourceScript handler;
    public float timer = 1;

    float timerReference = 0;
    int spawnInt = 0;
    
    public float spawnSpeed = 0.01f;
    

    // Use this for initialization
	void Start ()
    {
        handler = GameObject.Find("Stats_ResourceHandler").GetComponent<Stats_ResourceScript>();
        timerReference = timer;
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
                SpawnLocation.Add(transform.GetChild(i));
        }
        timerReference = timer;
        spawnSpeed = 0.1f;
    }
	
	// Update is called once per frame
	void Update ()
    {
       //Spawner
       //timer -= (spawnSpeed + handler.m_spawnMultiplier) * Time.deltaTime;
       timer -= (spawnSpeed) * Time.deltaTime;

        if (timer <= 0)
        {
            spawnInt = Random.Range(0, SpawnLocation.Count);
            // spawn object
            GameObject obj = Instantiate(ObjectList[0].gameObject);
            obj.GetComponent<NavMeshAgent>().Warp(SpawnLocation[spawnInt].position);

            timer = timerReference;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            spawnInt = Random.Range(0, SpawnLocation.Count);

            // spawn object
            GameObject obj = Instantiate(ObjectList[0].gameObject);

            obj.GetComponent<NavMeshAgent>().Warp(SpawnLocation[spawnInt].position);
        }
	}
}
