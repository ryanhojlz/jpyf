using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally_Auto_Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] spawningPosition;
    public List<GameObject> spawnMonster;

    public int SpawnLimit;

    public float SpawnTime = 2f;
    float timer;

    public float Spawn_Range = 0f;
    public float Spawn_Healer = 0f;
    public float Spawn_Tank = 1f;

    
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

        if (timer <= 0f && GameObject.FindGameObjectsWithTag("Ally_Unit").Length < SpawnLimit)
        {
//            int IndexSpawnPos = Random.Range(0, 3);
            int IndexSpawnPos = Random.Range(0, spawningPosition.Length);

            int IndexUnitToSpawn = Random.Range(0, spawnMonster.Count);
            float tempIndex = Random.Range(0f, Spawn_Range + Spawn_Healer + Spawn_Tank);

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

            if (number_of_unit <= 0)
                return;

            switch (spawnMonster[IndexUnitToSpawn].GetComponent<SpawnUnit>().Unit_Type)
            {
                case SpawnUnit.SPAWNTYPE.MELEE:
                    {
                        float temp_decrease = decreasePTank;
                        if (temp_decrease > Spawn_Tank)
                        {
                            temp_decrease = Spawn_Tank;
                        }

                        Spawn_Range += temp_decrease / (number_of_unit - 1);
                        Spawn_Healer += temp_decrease / (number_of_unit - 1);
                        Spawn_Tank -= temp_decrease;
                    }
                    break;

                case SpawnUnit.SPAWNTYPE.RANGE:
                    {
                        float temp_decrease = decreasePRange;
                        if (temp_decrease > Spawn_Range)
                        {
                            temp_decrease = Spawn_Range;
                        }

                        Spawn_Range -= temp_decrease;
                        Spawn_Healer += temp_decrease / (number_of_unit - 1);
                        Spawn_Tank += temp_decrease / (number_of_unit - 1);
                    }
                    break;

                case SpawnUnit.SPAWNTYPE.HEAL:
                    {
                        float temp_decrease = decreasePHeal;
                        if (temp_decrease > Spawn_Healer)
                        {
                            temp_decrease = Spawn_Healer;
                        }

                        Spawn_Range += temp_decrease / (number_of_unit - 1);
                        Spawn_Healer -= temp_decrease;
                        Spawn_Tank += temp_decrease / (number_of_unit - 1);
                    }
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
