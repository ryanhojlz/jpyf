using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SpawnHandlerScript : MonoBehaviour
{
    public List<Transform> ObjectList;
    public List<Transform> SpawnLocation;
    public float timer = 5;
    float timerReference = 0;
    float spawnlocation;
	// Use this for initialization
	void Start ()
    {
        timerReference = timer;
        for (int i = 0; i < transform.childCount; ++i)
        {
            SpawnLocation.Add(transform.GetChild(i));
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer -= 1 * Time.deltaTime;
        if (timer <= 0)
        {
            
            timer = timerReference;
            // spawn object
            GameObject obj = Instantiate(ObjectList[0].gameObject);
            obj.GetComponent<NavMeshAgent>().Warp(SpawnLocation[0].position);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            GameObject obj = Instantiate(ObjectList[0].gameObject);
            obj.GetComponent<NavMeshAgent>().Warp(SpawnLocation[0].position);
        }
	}
}
