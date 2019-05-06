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
    public GameObject Objective1 = null;
    public GameObject Objective2 = null;
    public GameObject Objective3 = null;
    public GameObject Objective4 = null;


    public float f_difficulty = 0;


    private void Awake()
    {

        // Singelton stuf
        if (!Instance)
            Instance = this;
        else if (Instance)
            Destroy(this.gameObject);

    }

    // Use this for initialization
    void Start ()
    {
        // Assigning milestones
        Milestones = GameObject.FindGameObjectsWithTag("MilestoneBlockade");

        Objective1 = Milestones[Milestones.Length - 1];
        Objective2 = Milestones[Milestones.Length - 2];
        Objective3 = Milestones[Milestones.Length - 3];
        Objective4 = Milestones[Milestones.Length - 4];

        BabySit = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Objective4 == null)
        {
            BabySit = false;
        }


        
        if (!BabySit)
            SpawnHandlerScript.Instance.spawnEnemy = true;
        else if (BabySit)
            SpawnHandlerScript.Instance.spawnEnemy = false;



        //f_difficulty = 0;
        //for (int i = 0; i < Milestones.Length - 1; ++i)
        //{
        //    if (Milestones[i] == null)
        //    {
        //        ++f_difficulty;
        //    }
        //}
        

    }

    
}
