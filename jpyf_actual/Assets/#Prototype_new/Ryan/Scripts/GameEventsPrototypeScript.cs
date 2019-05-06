using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameEventsPrototypeScript : MonoBehaviour
{
    public static GameEventsPrototypeScript Instance = null;

    // Milestone / Blockade references
    public GameObject[] Milestones;

    // Triggers
    public int Tutorial = 0;
    float tutorial_timer = 40;

    // Tutorial Boolean
    public bool BabySit = false;
    public GameObject Objective1 = null;
    public GameObject Objective2 = null;
    public GameObject Objective3 = null;
    public GameObject Objective4 = null;

    // References
    public Text subtitles_4外人 = null;
    public Text timer_4外人 = null;
    // VR Subtitless
    public TextMesh subtitles_4VR = null;
    

    public Transform panel = null;
    public float f_difficulty = 0;
    public bool TileEvent_Start = false;
    public bool ShowSubtitles = false;

    // Tutorial Objectives
    public Transform tutorialObjective_1 = null;
    public Transform tutorialObjective_2 = null;
    public Transform tutorialObjective_3 = null;
    public Transform tutorialObjective_4 = null;
    public Transform tutorialObjective_5 = null;


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
        // Assigning subtitle
        subtitles_4外人 = GameObject.Find("Subtitles").GetComponent<Text>();
        panel = subtitles_4外人.transform.parent;
        timer_4外人 = panel.GetChild(1).GetComponent<Text>();
        // Subtitles for VR
        subtitles_4VR = GameObject.Find("VRTEXT_UI").GetComponent<TextMesh>();


        // Assign Objectives
        Objective1 = Milestones[Milestones.Length - 1];
        Objective2 = Milestones[Milestones.Length - 2];
        Objective3 = Milestones[Milestones.Length - 3];
        Objective4 = Milestones[Milestones.Length - 4];

        BabySit = true;
        tutorialObjective_1 = GameObject.Find("TutorialObjective_1").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        BabySitConstraints();
        UpdateTutorial();
    }


    void UpdateTutorial()
    {
        timer_4外人.text = "" + tutorial_timer;
        // Change the subtitles etc
        switch (Tutorial)
        {
            case 0:
                // Introduction
                // Text timer
                tutorial_timer -= 1 * Time.deltaTime;
                ShowSubtitles = true;

                if (tutorial_timer > 35)
                {
                    subtitles_4外人.text = "Push the old mans cart";
                    subtitles_4VR.text = "You are old and \n you need your minion to push the cart";
                }
                else if (tutorial_timer > 30)
                {
                    subtitles_4外人.text = "The Cart is Damaged and you have to help repair it so you can move onward";
                    subtitles_4VR.text = "The Cart is Damaged and you have to repair it \n so you can move onward \n" +
                        "Wait for your minion to bring resources to you";
                    Follow_Objective.Instance.SetObjectiveTarget(tutorialObjective_1);
                    tutorial_timer = 20;
                    ++Tutorial;
                }
                break;
            case 1:
                //tutorial_timer -= 1 * Time.deltaTime;
                // Teach the player how to collect resources
                if (tutorialObjective_1)
                {
                    if (tutorialObjective_1.childCount < 3)
                    {
                        subtitles_4外人.text = "Collect the wood and throw back into the cart, Wait for the old man to repair with the materials";
                    }
                }
                if (Stats_ResourceScript.Instance.m_Minerals > 0)
                {
                    subtitles_4VR.text = "You have gained some materials, \n" +
                        " use the repair tool \n " +
                        "hammer the blue object below your drum";
                }
                if (Stats_ResourceScript.Instance.m_CartHP >= 100)
                {
                    ++Tutorial;
                }
                break;
            case 2:
                
                break;
        }

        if (ShowSubtitles)
        {
            subtitles_4外人.enabled = true;
            panel.gameObject.SetActive(true);
        }
        else
        {
            subtitles_4外人.enabled = false;
            panel.gameObject.SetActive(false);
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
