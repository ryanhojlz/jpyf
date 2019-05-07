using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnHandlerScript : MonoBehaviour
{
    public static ItemSpawnHandlerScript Instance = null;
    public List<Transform> SpawnLocation;
    public List<Pickup_Scripts> Items;
    
    float SpawnSpeed = 0.1f;
    float timer = 1;

    // Random random
    int item_type = 0;
    int location = 0;
    int itemCount = 0;

    public bool SpawnItems = false;
    // Use this for initialization
	void Start ()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (Instance)
        {
            Destroy(this.gameObject);
        }

        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
                SpawnLocation.Add(transform.GetChild(i));
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!SpawnItems)
            return;
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

    public void SpawnItem(Vector3 position)
    {
        item_type = Random.Range(0, Items.Count);
        Pickup_Scripts item = Instantiate(Items[item_type]);
        item.transform.position = position;
    }

    public void SpawnItem(int id,Vector3 position)
    {
        
        item_type = id;
        Pickup_Scripts item = Instantiate(Items[item_type]);
        position.y = +1;
        item.transform.position = position;
    }


    public void SpawnItem_randomBatch()
    {
        Debug.Log("Random batches ");
        itemCount = Random.Range(1, 4);
        for (int i = 0; i < itemCount; ++i)
        {
            item_type = Random.Range(0, 1);
            location = Random.Range(0, SpawnLocation.Count);
            Pickup_Scripts item = Instantiate(Items[item_type]);
            item.transform.position = SpawnLocation[location].position;
        }
    }
}
