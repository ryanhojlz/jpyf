using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRoomScript : MonoBehaviour
{
    // When cart enter trigger box spawn enemies

    public int i_spawningTicks = 0;
    float f_tick_timer = 12;
    bool b_start_spawning = false;

    public Transform spawnParent = null;
    public List<Transform> spawn_location;

    private void Awake()
    {
        i_spawningTicks = (int)Random.Range(1, 2);
        
    }

    // Use this for initialization
    void Start ()
    {
        spawnParent = this.transform.parent.GetChild(0);
        for (int i = 0; i < spawnParent.childCount; ++i)
        {
            if (spawnParent.GetChild(i).gameObject.activeSelf)
            {
                spawn_location.Add(spawnParent.GetChild(i));
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (b_start_spawning)
        {
            // Timer to spawn enemies
            f_tick_timer -= 1 * Time.deltaTime;
            
            // timer reaches 0;
            if (f_tick_timer <= 0)
            {
                --i_spawningTicks;
                SpawnOnEachSpot();
                if (i_spawningTicks > 0)
                {
                    // Reset timer
                    f_tick_timer = 12.0f;
                }
                else
                {
                    // No more need to spawn enemies
                    Destroy(this.gameObject);
                }
            }
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        // Collided with payload
        if (other.transform == PayloadMovementScript.Instance.payloadObject)
        {

            // If havent spawn yet
            if (!b_start_spawning)
            {
                // First spawn
                b_start_spawning = true;
                //--i_spawningTicks;
                SpawnOnEachSpot();
            }
        }
    }

    // Spawn per area
    void SpawnOnEachSpot()
    {
        for (int i = 0; i < spawn_location.Count; ++i)
        {
            SpawnManager.Instance.RandomSpawn(spawn_location[i].transform.position, true);
        }
    }
}
