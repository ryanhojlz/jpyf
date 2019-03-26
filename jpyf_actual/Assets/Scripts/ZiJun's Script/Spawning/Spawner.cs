using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] spawningPosition;
    public List<GameObject> spawnMonster;

    public int SpawnLimit;

    public float SpawnTime = 2f;
    float timer;

    float Spawn_Range = 0f;
    float Spawn_Healer = 0f;
    float Spawn_Tank = 1f;

    
    float decreasePTank = 0.1f;
    float decreasePHeal = 0.3f;
    float decreasePRange = 0.2f;
    int number_of_unit;




    void Start()
    {
        timer = SpawnTime;
        number_of_unit = spawnMonster.Count;
    }

    // Update is called once per frame
    void Update()
    {
        //float increaseP = decreaseP / 3f;
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //GameObject newObj = Instantiate(spawnMonster[0]) as GameObject;
        //newObj.transform.position = spawningPosition[0].transform.position;
        //}

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    GameObject newObj = Instantiate(spawnMonster[0]) as GameObject;
        //    newObj.transform.position = spawningPosition[1].transform.position;
        //}

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    GameObject newObj = Instantiate(spawnMonster[0]) as GameObject;
        //    newObj.transform.position = spawningPosition[2].transform.position;
        //}

        //int IndexSpawnPos = Random.Range(0, 3);
        //int IndexUnitToSpawn = Random.Range(0, spawnMonster.Count);

        //Debug.Log("Spawn_Position = " + IndexSpawnPos + " ," + "Spawn_Unit = " + IndexUnitToSpawn);

        if (timer <= 0f && GameObject.FindGameObjectsWithTag("Enemy_Unit").Length < SpawnLimit)
        {
//            int IndexSpawnPos = Random.Range(0, 3);
            int IndexSpawnPos = Random.Range(0, spawningPosition.Length);

            int IndexUnitToSpawn = Random.Range(0, spawnMonster.Count);
            float tempIndex = Random.Range(0f, 1f);

            if (tempIndex < Spawn_Range)
            {
                for (int i = 0; i < spawnMonster.Count; ++i)
                {
                    if (spawnMonster[i].GetComponent<SpawnUnit>().Unit_Type == SpawnUnit.SPAWNTYPE.RANGE)
                    {
                        IndexUnitToSpawn = i;
                    }
                }
            }
            else if (tempIndex < Spawn_Range + Spawn_Healer)
            {
                for (int i = 0; i < spawnMonster.Count; ++i)
                {
                    if (spawnMonster[i].GetComponent<SpawnUnit>().Unit_Type == SpawnUnit.SPAWNTYPE.HEAL)
                    {
                        IndexUnitToSpawn = i;
                    }
                }
            }
            else
            {
                for (int i = 0; i < spawnMonster.Count; ++i)
                {
                    if (spawnMonster[i].GetComponent<SpawnUnit>().Unit_Type == SpawnUnit.SPAWNTYPE.MELEE)
                    {
                        IndexUnitToSpawn = i;
                    }
                }
            }


            //Spawn
            Ray CastToGround = new Ray(spawningPosition[IndexSpawnPos].transform.position, Vector3.down);
            RaycastHit hit;
            Physics.Raycast(CastToGround, out hit);

            GameObject newObj = Instantiate(spawnMonster[IndexUnitToSpawn],hit.point, this.transform.rotation) as GameObject;
            newObj.transform.position = spawningPosition[IndexSpawnPos].transform.position;


            switch (spawnMonster[IndexUnitToSpawn].GetComponent<SpawnUnit>().Unit_Type)
            {
                case SpawnUnit.SPAWNTYPE.MELEE:
                    Spawn_Range += decreasePTank/ number_of_unit;
                    Spawn_Healer += decreasePTank/ number_of_unit;
                    Spawn_Tank -= decreasePTank;
                    break;

                case SpawnUnit.SPAWNTYPE.RANGE:
                    Spawn_Range -= decreasePRange;
                    Spawn_Healer += decreasePRange / number_of_unit;
                    Spawn_Tank += decreasePRange / number_of_unit;
                    break;

                case SpawnUnit.SPAWNTYPE.HEAL:
                    Spawn_Range += decreasePHeal / decreasePRange;
                    Spawn_Healer -= decreasePHeal;
                    Spawn_Tank += decreasePHeal / decreasePRange;
                    break;

            }

            timer = SpawnTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
