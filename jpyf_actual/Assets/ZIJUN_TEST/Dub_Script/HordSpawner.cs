using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordSpawner : MonoBehaviour
{

    public GameObject prefebUnitToSpawn;
    public int numberToSpawn;

    bool AlreadySpawned = false;

	// Use this for initialization 
	void Start ()
    {


        int num_child = this.transform.childCount;

        for (int i = 0; i < num_child; ++i)
        {
            this.transform.DetachChildren();
        }

        //Destroy(this.gameObject);
    }

    private void Update()
    {

        //Ray CastToGround = new Ray(this.transform.position, Vector3.down);
        //RaycastHit hit;
        //Physics.Raycast(CastToGround, out hit);

        ////GameObject newObj = Instantiate(spawnMonster[IndexUnitToSpawn], hit.point, this.transform.rotation) as GameObject;
        ////newObj.transform.position = spawningPosition[IndexSpawnPos].transform.position;

        //if (!AlreadySpawned)
        //{
        //    GameObject newObj = Instantiate(prefebUnitToSpawn, hit.point, this.transform.rotation) as GameObject;
        //    newObj.transform.position = hit.point;

        //    AlreadySpawned = true;
        //}
    }
}
