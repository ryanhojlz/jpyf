using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsPrototypeScript : MonoBehaviour
{
    public static GameEventsPrototypeScript Instance = null;

    // Milestone / Blockade references
    public GameObject[] Milestones;

    // Tutorial Boolean
    public bool BabySit = false;
    public GameObject FirstObjective = null;
    // Use this for initialization
    void Start ()
    {
        // Singelton stuf
        if (!Instance)
            Instance = this;
        else
            Destroy(this.gameObject);

        // Assigning milestones
        Milestones = GameObject.FindGameObjectsWithTag("MilestoneBlockade");

        FirstObjective = Milestones[Milestones.Length - 1];

        BabySit = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (FirstObjective == null)
        {
            BabySit = false;
        }

        if (!BabySit)
            SpawnHandlerScript.Instance.spawnEnemy = true;
        else if (BabySit)
            SpawnHandlerScript.Instance.spawnEnemy = false;

    }
}
