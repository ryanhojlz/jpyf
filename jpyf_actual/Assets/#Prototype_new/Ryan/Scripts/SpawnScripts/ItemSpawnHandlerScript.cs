using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnHandlerScript : MonoBehaviour
{
    public List<Transform> SpawnLocation;
    public List<Pickup_Scripts> Items;
    
    int SpawnSpeed = 1;
    float timer = 1;
    // Use this for initialization
	void Start ()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            SpawnLocation.Add(transform.GetChild(i));
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer -= SpawnSpeed * Time.deltaTime;
        if (timer <= 0)
        {
            timer = 1;
            int item_type = Random.Range(0, Items.Count - 1);
            SpawnItem(item_type);
        }
	}

    public void SpawnItem(int id)
    {

        int location = Random.Range(0, SpawnLocation.Count - 1);

        Pickup_Scripts item = Instantiate(Items[id]);
        item.transform.position = SpawnLocation[location].position;
    }
}
