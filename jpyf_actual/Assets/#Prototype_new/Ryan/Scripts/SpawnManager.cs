using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    // Its week 10 why didnt i think of this
    public static SpawnManager Instance = null;

    public List<GameObject> SpawnList;
    // idk if local variables are cheaper
    GameObject reuseable;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(this.gameObject);

        
    }

    // Use this for initialization
    //   void Start ()
    //   {

    //}

    // Update is called once per frame
    void Update()
    {

    }

    // Spawning
    //public void SpawnObject(int id)
    //{
    //    Instantiate(SpawnList[id]);
    //}

    //public void SpawnObject(int id, Vector3 pos, Quaternion rot, Transform parent)
    //{
    //    Instantiate(SpawnList[id], pos, rot, parent);
    //}

    //public void SpawnObject(int id, bool idle)
    //{
    //    reuseable = Instantiate(SpawnList[id]);
    //    reuseable.GetComponent<Entity_Unit>().SetisIdle(idle);
    //    //return reuseable;
    //}

        // If ur object contains nav mesh reccomend to warp after instantiating
    public void SpawnObject(int id, Vector3 pos, Quaternion rot, Transform parent, bool idle)
    {
        reuseable = Instantiate(SpawnList[id], pos, rot, parent);
        reuseable.GetComponent<NavMeshAgent>().Warp(pos);
        reuseable.GetComponent<Entity_Unit>().SetisIdle(idle);
        //return reuseable;
    }

    // Spawns from id 1 ~ 5 object
    public void RandomSpawn(Vector3 pos, Quaternion rot, Transform parent, bool idle)
    {
        float random = Random.Range(0, 15);
        reuseable = Instantiate(SpawnList[(int)(random % 5)], pos, rot, parent);
        reuseable.GetComponent<NavMeshAgent>().Warp(pos);
        reuseable.GetComponent<Entity_Unit>().SetisIdle(idle);
    }


    public void RandomSpawn(Vector3 pos, bool idle)
    {
        float random = Random.Range(0, 15);               
        reuseable = Instantiate(SpawnList[(int)(random % 5)]) as GameObject;
        reuseable.GetComponent<NavMeshAgent>().Warp(pos);
        reuseable.transform.GetChild(0).GetComponent<Entity_Unit>().SetisIdle(idle);

    }
}
