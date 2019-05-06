using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameEventsPrototypeScript : MonoBehaviour
{
    public static GameEventsPrototypeScript Instance = null;

    // Milestone / Blockade references
    public GameObject[] Milestones;

    public int Tutorial = 0;
    float tutorial_timer = 15;


    // Tutorial Boolean
    public bool BabySit = false;
    public GameObject Objective1 = null;
    public GameObject Objective2 = null;
    public GameObject Objective3 = null;
    public GameObject Objective4 = null;


    public Text subtitles_外人;
    public float f_difficulty = 0;
    public bool TileEvent_Start = false;

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
        BabySitConstraints();
    }
    

    void UpdateTutorial()
    {
        switch (Tutorial)
        {
            case 0:
                tutorial_timer -= 1 * Time.deltaTime;
                break;
            case 1:
                break;
        }
    }


    // Game was to hard nuff sad
    void BabySitConstraints()
    {
        if (Objective4 == null)
        {
            BabySit = false;
        }
        if (!BabySit)
            SpawnHandlerScript.Instance.spawnEnemy = true;
        else if (BabySit)
            SpawnHandlerScript.Instance.spawnEnemy = false;

    }

}
