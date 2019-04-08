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


        //int num_child = this.transform.childCount;

        //for (int i = 0; i < num_child; ++i)
        //{
        //    this.transform.DetachChildren();
        //}

        //Destroy(this.gameObject);
    }

    private void Update()
    {
        //Ray CastToGround = new Ray(this.transform.position, Vector3.down);
        //RaycastHit hit;
        //Physics.Raycast(CastToGround, out hit);

        //GameObject newObj = Instantiate(spawnMonster[IndexUnitToSpawn], hit.point, this.transform.rotation) as GameObject;
        //newObj.transform.position = spawningPosition[IndexSpawnPos].transform.position;

        //if (!AlreadySpawned)
        //{
        //    GameObject newObj = Instantiate(prefebUnitToSpawn, hit.point, this.transform.rotation) as GameObject;
        //    newObj.transform.position = hit.point;

        //    AlreadySpawned = true;
        //}


        //Debug.Log(Mathf.Atan(90));


        //Vector3 TestVector = new Vector3(Mathf.Cos(90 * Mathf.Deg2Rad), Mathf.Sin(90 * Mathf.Deg2Rad));
        //Debug.Log(TestVector);

        if (numberToSpawn > 10)//If spawning more then 10, max it at 10
        {
            numberToSpawn = 10;
        }

        int counter = 0;

        float TotalAngle = 360f;

        float PerAngle = TotalAngle / numberToSpawn;

        float radius_obj = prefebUnitToSpawn.transform.localScale.x;

        float HalfPerAngle = PerAngle * 0.5f;

        float radius = radius_obj / Mathf.Tan(HalfPerAngle * Mathf.Deg2Rad);

        while (this.transform.childCount < numberToSpawn)
        {
            Vector3 TestVector = new Vector3(Mathf.Cos(PerAngle * counter * Mathf.Deg2Rad), 0, Mathf.Sin(PerAngle * counter * Mathf.Deg2Rad));

            Ray CastToGround = new Ray(this.transform.position + (TestVector * radius), Vector3.down);
            RaycastHit hit;
            Physics.Raycast(CastToGround, out hit);

            GameObject newObj = Instantiate(prefebUnitToSpawn, hit.point, this.transform.rotation);
            newObj.transform.position = hit.point;
            newObj.transform.parent = this.transform;

            counter++;
        }

        int num_child = this.transform.childCount;

        for (int i = 0; i < num_child; ++i)
        {
            this.transform.DetachChildren();
        }

        Destroy(this.gameObject);
    }
}
