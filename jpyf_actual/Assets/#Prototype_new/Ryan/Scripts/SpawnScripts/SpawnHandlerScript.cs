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
    
    public float spawnSpeed = 0.1f;

    public bool spawnEnemy = false;
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
        //Debug.Log("adawdaw" + (spawnSpeed + handler.m_spawnMultiplier));
        //Spawner
        //timer -= (spawnSpeed + handler.m_spawnMultiplier) * Time.deltaTime;
        //timer -= (spawnSpeed + handler.m_spawnMultiplier) * Time.deltaTime;

        if (spawnEnemy)
        {
            timer -= (spawnSpeed) * Time.deltaTime;
            if (timer <= 0)
            {
                SpawnEnemyRandom(1);
                //spawnInt = Random.Range(0, SpawnLocation.Count);
                //// spawn object
                //GameObject obj = Instantiate(ObjectList[Random.Range(0,ObjectList.Count)].gameObject);
                //obj.GetComponent<NavMeshAgent>().Warp(SpawnLocation[spawnInt].position);
                //timer = timerReference;
            }
        }



        if (Input.GetKeyDown(KeyCode.M))
        {
            SpawnEnemyRandom(1);
            //spawnInt = Random.Range(0, SpawnLocation.Count);

            //// spawn object
            //GameObject obj = Instantiate(ObjectList[0].gameObject);

            //obj.GetComponent<NavMeshAgent>().Warp(SpawnLocation[spawnInt].position);
        }
	}

    void SpawnEnemyRandom(int count)
    {
        Debug.Log("Text is " + count + handler.i_num_enemies_spawn);
        for (int i = 0; i < count + handler.i_num_enemies_spawn; ++i)
        {
            spawnInt = Random.Range(0, SpawnLocation.Count);
            // spawn object
            GameObject obj = Instantiate(ObjectList[Random.Range(0, ObjectList.Count)].gameObject);
            obj.GetComponent<NavMeshAgent>().Warp(SpawnLocation[spawnInt].position);
            timer = timerReference;

        }
    }
}
