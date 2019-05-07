using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawnerScript : MonoBehaviour
{
    public GameObject bombprefab = null;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpawnBomb(this.transform.position);
	}

    void SpawnBomb(Vector3 pos)
    {
        if (this.transform.childCount < 1)
        {
            Instantiate(bombprefab, pos, Quaternion.identity, this.transform);
        }
    }
}
