using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Ally : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] spawningPosition;
    public GameObject spawnMonster_Keypress;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject newObj = Instantiate(spawnMonster_Keypress) as GameObject;
            newObj.transform.position = spawningPosition[0].transform.position;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            GameObject newObj = Instantiate(spawnMonster_Keypress) as GameObject;
            newObj.transform.position = spawningPosition[1].transform.position;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject newObj = Instantiate(spawnMonster_Keypress) as GameObject;
            newObj.transform.position = spawningPosition[2].transform.position;
        }
    }
}
