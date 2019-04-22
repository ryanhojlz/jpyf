using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnHandlerScript : MonoBehaviour
{
    public List<Transform> SpawnLocation;
    public List<Pickup_Scripts> Items;
    
    float SpawnSpeed = 0.2f;
    float timer = 1;

    // Random random
    int item_type = 0;
    int location = 0;
    int itemCount = 0;

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
            SpawnItem();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SpawnItem_randomBatch();
        }

	}

    public void SpawnItem()
    {
        item_type = Random.Range(0, Items.Count);
        location = Random.Range(0, SpawnLocation.Count);
        Pickup_Scripts item = Instantiate(Items[item_type]);
        item.transform.position = SpawnLocation[location].position;
    }

    public void SpawnItem_randomBatch()
    {
        Debug.Log("Random batches ");
        itemCount = Random.Range(1, 4);
        for (int i = 0; i < itemCount; ++i)
        {
            item_type = Random.Range(0, Items.Count);
            location = Random.Range(0, SpawnLocation.Count);
            Pickup_Scripts item = Instantiate(Items[item_type]);
            item.transform.position = SpawnLocation[location].position;
        }
    }
}
