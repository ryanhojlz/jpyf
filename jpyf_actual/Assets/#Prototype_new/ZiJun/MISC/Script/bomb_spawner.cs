using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb_spawner : MonoBehaviour
{
    public float spawnPerBomb = 3f;
    public float previousTime = 0f;

    public GameObject Bomb_Prefeb = null;
	// Use this for initialization
	void Start ()
    {
        previousTime = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (previousTime + spawnPerBomb < Time.time)
        {
            previousTime = Time.time;
            //Debug.Log("Hiii");
            Instantiate(Bomb_Prefeb, this.transform.position, this.transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Bomb_Prefeb, this.transform.position, this.transform.rotation);
        }
    }
}
