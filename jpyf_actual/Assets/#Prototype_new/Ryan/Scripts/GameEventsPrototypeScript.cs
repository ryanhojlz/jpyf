using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEventsPrototypeScript : MonoBehaviour
{
    // Singleton
    public static GameEventsPrototypeScript Instance = null;
    // Milestone / Blockade references
    public GameObject[] Milestones;


    // Payload reference
    public Transform payload_ref = null;

    // Triggers
    public int Tutorial = 0;
    float tutorial_timer = 40;

    // Tutorial Boolean
    public bool BabySit = false;
    public GameObject Objective1 = null;
    public GameObject Objective2 = null;
    public GameObject Objective3 = null;
    public GameObject Objective4 = null;

    // Text References
    // Subtitles
    public Text subtitles_4外人 = null;
    // Timer if any need
    public Text timer_4外人 = null;
    // VR Subtitless
    public TextMesh subtitles_4VR = null;

    // Tutorial Index // To show on screen what tutorial i am at mainly for debugging
    public Text index_text = null;
    // Panel
    public Transform panel = null;
    public float f_difficulty = 0;
    public bool TileEvent_Start = false;
    public bool ShowSubtitles = false;


    // Transform Link
    public Transform[] Bomb_Tutorial = new Transform[2];

    // Tutorial Objectives
    // Wood material introduction
    public Transform tutorialObjective_1 = null;
    // Cart Objective
    public Transform tutorialObjective_2 = null;
    // Bomb Objective
    public Transform tutorialObjective_3 = null;
    // Wall Objective
    public Transform tutorialObjective_4 = null;
    // First Shrine Encounter
    public Transform tutorialObjective_5 = null;
    // Enemy Shield Tutorial
    public Transform tutorialObjective_6 = null;
    // Tengu tutorial
    public Transform tutorialObjective_7 = null;


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
        // Payload ref assigning 
        payload_ref = GameObject.Find("PayLoad").transform;
        // Easy mode
        BabySit = true;
        // Assigning milestones
        Milestones = GameObject.FindGameObjectsWithTag("MilestoneBlockade");
        // Assigning subtitle
        subtitles_4外人 = GameObject.Find("Subtitles").GetComponent<Text>();
        panel = subtitles_4外人.transform.parent;
        timer_4外人 = panel.GetChild(1).GetComponent<Text>();
        // Subtitles for VR
        subtitles_4VR = GameObject.Find("VRTEXT_UI").GetComponent<TextMesh>();
        // Assigning text for debug
        index_text = GameObject.Find("TutorialNumber").GetComponent<Text>();
       


        // Assign Objectives
        Objective1 = Milestones[Milestones.Length - 1];
        Objective2 = Milestones[Milestones.Length - 2];
        Objective3 = Milestones[Milestones.Length - 3];
        Objective4 = Milestones[Milestones.Length - 4];


        // Tutorial Objective 1 // Resource Collection
        tutorialObjective_1 = GameObject.Find("TutorialObjective_1").transform;
        // Tutorial Objective 2 // Moving the Cart
        tutorialObjective_2 = GameObject.Find("TutorialObjective_2").transform;
        // Tutorial Objective 3 // Bombs 
        tutorialObjective_3 = GameObject.Find("TutorialObjective_3").transform;

        tutorialObjective_3.gameObject.SetActive(false);

        // Tutorial Objective 4 // Wall
        tutorialObjective_4 = GameObject.Find("TutorialObjective_4").transform;
        // Tutorial Objective 5 // First shrine encounter
        tutorialObjective_5 = GameObject.Find("TutorialObjective_5").transform;


        // Bomb tutorial
        Bomb_Tutorial[0] = GameObject.Find("Bomb_Tutorial_1").transform;
        Bomb_Tutorial[1] = GameObject.Find("Bomb_Tutorial_2").transform;
        tutorialObjective_6 = GameObject.Find("TutorialObjective_6").transform;

        // Tengu Tutorial
        tutorialObjective_7 = GameObject.Find("TutorialObjective_7").transform;

    }



    // Update is called once per frame
    void Update ()
    {
        BabySitConstraints();
        UpdateTutorial();
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

    // Welcome to my terrible
    // Hardcoded Tutorial
    void UpdateTutorial()
    {
        timer_4外人.text = "" + tutorial_timer;

        index_text.text = "Tutorial  " + Tutorial;
        // Change the subtitles etc
        switch (Tutorial)
        {
            case -1:

                break;
            case 0:
                // Introduction
                // Text timer
                tutorial_timer -= 1 * Time.deltaTime;
                ShowSubtitles = true;

                if (tutorial_timer > 35)
                {
                    subtitles_4外人.text = "Objective Push P1's cart";
                    subtitles_4VR.text = "You are P1 and \n you need P2 to bring you to the other side";
                }
                else if (tutorial_timer > 30)
                {
                    subtitles_4外人.text = "The Cart is Damaged and you have to help repair it so you can move onward";
                    subtitles_4VR.text = "The Cart is Damaged and you have to repair it \n so you can move onward \n" +
                        "Wait for P2 to bring resources to you";
                    Follow_Objective.Instance.SetObjectiveTarget(tutorialObjective_1);
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
                        subtitles_4外人.text = "Collect the wood and throw back into the cart, Wait for the P1 to repair with the materials";
                    }
                    if (tutorialObjective_1.childCount == 0)
                    {

                        Destroy(tutorialObjective_1.gameObject);
                    }
                }

                if (Stats_ResourceScript.Instance.m_Minerals > 0)
                {
                    // When you get enough materials change text
                    subtitles_4VR.text = "You have gained some materials,\n" +
                        "Grab the tool on the left & press square \n " +
                        "hammer the blue object below your drum";
                }
                if (Stats_ResourceScript.Instance.m_CartHP >= 20)
                { 
                    ++Tutorial;
                }
                break;
            case 2:
                // Wall tutorials
                // When payload reaches a certain distance
                if (payload_ref.position.z > 1.5f)
                {
                    subtitles_4外人.text = "There is a wall infront grab a bomb to destroy it";
                    subtitles_4VR.text = "Wait for P2 to destroy it";
                    tutorialObjective_3.gameObject.SetActive(true);
                    Follow_Objective.Instance.SetObjectiveTarget(tutorialObjective_3);
                }
                else
                {
                    Follow_Objective.Instance.SetObjectiveTarget(tutorialObjective_2);
                    // When payload still stationary
                    subtitles_4外人.text = "Stand in the cart radius to push the cart foward";
                    subtitles_4VR.text = "You require P2 to push the cart foward";
                }

                if (PickupHandlerScript.Instance.currentObject)
                {
                    // When u pickup the bomb
                    if (PickupHandlerScript.Instance.currentObject.GetComponent<bomb_script>())
                    {
                        ++Tutorial;
                    }
                }
                break;
            case 3:
                if (!tutorialObjective_4)
                {
                    Destroy(tutorialObjective_3.gameObject);
                    ++Tutorial;
                }
                else
                {
                    // Destory the wall
                    subtitles_4外人.text = "Destroy the wall with a bomb";
                    Follow_Objective.Instance.SetObjectiveTarget(tutorialObjective_4);
                }
                break;
            case 4:
                // First shrine encounter
                if (tutorialObjective_5)
                {
                    if (tutorialObjective_5.parent.GetComponent<Tile_EventScript>().b_eventStart)
                    {
                        subtitles_4VR.text = "Swap Grab the tool on the left of you \n" +
                            "& Press Triangle on the controller";
                        subtitles_4外人.text = "Enemies will start spawning, you can grab them on throw them towards the P1 view";
                    }
                    else
                    {
                        subtitles_4外人.text = "Continue Pushing Onward";
                    }
                }
                else
                {
                    // Wall has been destroyed on to the next tutorial
                    ++Tutorial;
                    ShowSubtitles = false;
                }
                break;
            case 5:
                // Bomb shield tutorial
                if (payload_ref.transform.position.z > 30)
                {
                    Follow_Objective.Instance.SetObjectiveTarget(tutorialObjective_6);

                    ShowSubtitles = true;
                    subtitles_4外人.text = "Some enemies have shields and need to be broken down by bombs";
                    subtitles_4VR.text = "Some enemies have shields and need to be broken down by bombs";
                }
                if (tutorialObjective_6.transform.childCount <= 0)
                {
                    ShowSubtitles = false;
                    Destroy(Bomb_Tutorial[0].gameObject);
                    Destroy(Bomb_Tutorial[1].gameObject);

                    ++Tutorial;
                }
                break;
            case 6:
                // If 2nd shrine encounter
                if (!tutorialObjective_7)
                {
                    ShowSubtitles = true;
                    subtitles_4外人.text = "Tengus will grab you be careful";
                    subtitles_4VR.text = "Tengus will grab P1\n" +
                        "Press the Circle Button for Staff";
                }
                else
                {
                    if (tutorialObjective_7.childCount <= 0)
                    {
                        Destroy(this.tutorialObjective_7.gameObject);
                    }
                    ++Tutorial;
                }
                break;
            case 7:

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
}
