using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnterSpawner : MonoBehaviour
{
    // Mainly using this script for the tutorial
    public List<GameObject> ObjectList;
    public bool onlySpawn_once = false;
    public bool spawn = false;
    public int spawnType = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!onlySpawn_once)
        {
            SpawnEnemy(spawnType);
            onlySpawn_once = true;
        }
    }

    void SpawnEnemy(int id)
    {
        Instantiate(ObjectList[id],this.transform.position, Quaternion.identity);
    }
   
    private void OnTriggerEnter(Collider other)
    {
        // Spawn enemy
        if (other.tag == "Payload")
        {
            if (!spawn)
                spawn = true;
        }
    }
}