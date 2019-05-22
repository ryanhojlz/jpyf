using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning_Bomb : MonoBehaviour
{
    //way 1 : manager to spawn bomb

    public bool can_spawn = true;//boolean used to spawn bomb

    public GameObject bombPrefeb = null;

    public float TimerToSpawn = 5f;

    bool readyToSpawn = false;

    List<Transform> listOfBombSpawner = new List<Transform>();

    float previousTime = 0f;

    GameObject temp_holder = null;//Used for the loop assigning
    // Use this for initialization
    void Start()
    {
        Transform this_obj = this.gameObject.transform;

        if (this_obj.childCount > 0)
        {
            for (int i = 0; i < this_obj.childCount; ++i)
            {
                listOfBombSpawner.Add(this_obj.GetChild(i));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfSlotAvaliable();

        if (previousTime + TimerToSpawn < Time.time)
        {
            readyToSpawn = true;
        }

        if (readyToSpawn)
        {
            Debug.Log("Came here 2");
            SpawnBomb();
        }
    }

    void SpawnBomb()
    {
        foreach (Transform bs in listOfBombSpawner)//Searching where there is an open slot to spawn the bomb
        {
            if (bs.childCount == 0)
            {
                temp_holder = Instantiate(bombPrefeb) as GameObject;
                temp_holder.transform.parent = bs;
                temp_holder.transform.localPosition = new Vector3(0, 0, 0);
                BombHasSpawn();
                break;
            }
        }
    }

    void CheckIfSlotAvaliable()
    {
        foreach (Transform bs in listOfBombSpawner)//Searching where there is an open slot to spawn the bomb
        {
            if (bs.childCount == 0)
            {
                return;
            }
        }

        previousTime = Time.time;
    }

    void BombHasSpawn()//What to do after the bomb has spawned
    {
        readyToSpawn = false;
        previousTime = Time.time;
    }
}
