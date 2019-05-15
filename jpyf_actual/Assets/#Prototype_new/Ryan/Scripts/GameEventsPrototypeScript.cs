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
    // Tutorial 0 for debugging side
    // Tutorial -1 for actual tutorial
    public int Tutorial = 0;
    float tutorial_timer = 45;

    // Tutorial Boolean
    public bool b_enabled_tutorial = false;


    public GameObject Objective1 = null;
    public GameObject Objective2 = null;
    public GameObject Objective3 = null;
    //public GameObject Objective4 = null;

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

    public bool b_bigExplain = false;
    // Pause Func
    bool b_isPaused = false;


    // Win lose stuff
    enum WINLOSE
    {
        neutral,
        win,
        lose
    }
    WINLOSE condition = WINLOSE.neutral;

    // Losing timer
    public float losingTimer = 10;
    // Resetter variable
    public float ref_losingTimer = 10;


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
        //
        b_enabled_tutorial = true;
        //Stats_ResourceScript.Instance.m_StartLanternTick = true;


        // Payload ref assigning 
        payload_ref = GameObject.Find("PayLoad").transform;
        // Easy mode
       
        // Assigning milestones
        //Milestones = GameObject.FindGameObjectsWithTag("MilestoneBlockade");
        // Assigning subtitle
        subtitles_4外人 = GameObject.Find("Subtitles").GetComponent<Text>();
        panel = subtitles_4外人.transform.parent;
        timer_4外人 = panel.GetChild(1).GetComponent<Text>();
        // Subtitles for VR
        subtitles_4VR = GameObject.Find("VRTEXT_UI").GetComponent<TextMesh>();
        // Assigning text for debug
        index_text = GameObject.Find("TutorialNumber").GetComponent<Text>();
       


        // Assign Objectives
        //Objective1 = Milestones[Milestones.Length - 1];
        //Objective2 = Milestones[Milestones.Length - 2];
        //Objective3 = Milestones[Milestones.Length - 3];
        //Objective4 = Milestones[Milestones.Length - 4];


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


        if (Tutorial == 0)
        {
            Stats_ResourceScript.Instance.m_P2_hp = 0;
        }


    }



    // Update is called once per frame
    void Update ()
    {
        // Pause
        ProcessPause();
        ProcessWinLoseCondition();
        // Tutorial Updates etc
        UpdateTutorial();
        
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
            //case -1:
            //    // Introduction on how to heal the player
            //    if (Stats_ResourceScript.Instance.m_P2_hp >= 100)
            //    {
            //        ++Tutorial;
            //    }
            //    else
            //    {
            //        ShowSubtitles = true;
            //        subtitles_4外人.text = "You are dead in need to be revived";
            //        subtitles_4VR.text = "Revive P2 \n Grab the the drum sticks infront of you" +
            //            "Press the right trigger of your controller";
            //    }
            //    break;
            case 0:
                // Introduction
                ShowSubtitles = true;
                // Text timer
                Stats_ResourceScript.Instance.m_LanternHp = 100;

                if (Stats_ResourceScript.Instance.m_P2_hp >= 100)
                {
                    tutorial_timer -= 1 * Time.deltaTime;
                }
                if (tutorial_timer > 44)
                {                  
                    subtitles_4外人.text = "You are dead. Please wait to be revived";
                    subtitles_4VR.text = "Revive P2 \n Grab the the drum sticks infront of you" +
                        "Press the right trigger of your controller";
                    
                }                
                else if (tutorial_timer > 40)
                {
                    subtitles_4外人.text = "Objective Push P1's cart";
                    subtitles_4VR.text = "You are P1 and \n you need P2 to bring you to the other side";
                }
                else if (tutorial_timer > 35)
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
                    if (tutorialObjective_1.childCount >= 3)
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
                        "hammer the drum";
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
                    Destroy(tutorialObjective_6.gameObject);
                    ++Tutorial;
                }
                break;
            case 6:
                // If 2nd shrine encounter

                if (!Objective2)
                {
                    if (tutorialObjective_7)
                    {
                        ShowSubtitles = true;
                        subtitles_4外人.text = "Tengus will grab you be careful";
                        subtitles_4VR.text = "Tengus will grab P1\n" +
                            "Press the Circle Button for Staff";
                    }
                    if (tutorialObjective_7.childCount <= 0)
                    {
                        ShowSubtitles = false;
                        Destroy(this.tutorialObjective_7.gameObject);
                        tutorial_timer = 50;
                        Statistics.Instance.SetTutorialComplete();
                        ++Tutorial;
                    }
                }                
                break;
            case 7:
                if (!Objective3)
                {                     
                    b_bigExplain = true;
                    ShowSubtitles = true;
                    tutorial_timer -= 1 * Time.deltaTime;
                    if (tutorial_timer > 45)
                    {
                        subtitles_4外人.text = "Congrats on clearing basic tutorial";
                        subtitles_4VR.text = "Congrats on clearing basic tutorial";
                    }
                    else if (tutorial_timer > 40)
                    {
                        subtitles_4外人.text = "From here on the lantern will start depleting";
                        subtitles_4VR.text = "From here on the lantern will start depleting";
                    }
                    else if (tutorial_timer > 35)
                    {
                        subtitles_4外人.text = "If the lantern dies out a timer will start ticking ";
                        subtitles_4VR.text = "If the lantern dies out a timer will start ticking";
                    }
                    else if (tutorial_timer > 30)
                    {
                        subtitles_4外人.text = "Once the timer reaches 0 P1 & P2 lose";
                        subtitles_4VR.text = "Once the timer reaches 0 P1 & P2 lose";
                    }
                    else if (tutorial_timer > 25)
                    {
                        subtitles_4外人.text = "Once the timer reaches 0 P1 & P2 lose";
                        subtitles_4VR.text = "Once the timer reaches 0 P1 & P2 lose";
                    }
                    else if (tutorial_timer > 20)
                    {
                        subtitles_4VR.text = "You can refuel by throwing candles\n into the flaming pot";
                        subtitles_4外人.text = "You can replenish lantern fuel\n by breaking lanterns and throwing the resources back to the cart";
                    }
                    else if (tutorial_timer > 15)
                    {
                        subtitles_4VR.text = "Enemies will now do more damage\n the lower the lantern fuel";
                        subtitles_4外人.text = "Enemies will now do more damage the lower the lantern fuel";

                    }
                    else if (tutorial_timer > 10)
                    {
                        ShowSubtitles = false;
                        Stats_ResourceScript.Instance.m_StartLanternTick = true;
                        if (b_enabled_tutorial)
                            b_enabled_tutorial = false;
                          
                        ++Tutorial;
                        b_bigExplain = false;
                    }

                }
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

    // Shetty pause func
    // Its placed like this so i can put on varios keypress
    void PauseFunc()
    {
        if (b_isPaused)
        {
            b_isPaused = false;
        }
        else if (!b_isPaused)
        {
            b_isPaused = true;
        }
    }

    // Actual pausing
    void ProcessPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseFunc();
        }
        
        if (b_isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }


    // Lose Condition
    void ProcessWinLoseCondition()
    {
        //if neutral havent win lose
        if (condition == WINLOSE.neutral)
        {
            if (Stats_ResourceScript.Instance.m_LanternHp <= 0)
            {
                losingTimer -= 1 * Time.deltaTime;
                if (losingTimer <= 0)
                {
                    condition = WINLOSE.lose;
                }
            }
            else
            {
                // When lantern hp not 0 
                if (losingTimer < ref_losingTimer)
                {

                    losingTimer += 1 * Time.deltaTime;

                    if (losingTimer > ref_losingTimer)
                    {
                        losingTimer = ref_losingTimer;
                    }
                }
            }
        }


        if (payload_ref.transform.position.z >= 350)
        {
            if (condition == WINLOSE.neutral)
                condition = WINLOSE.win;
        }
        
       
        
        

        if (condition == WINLOSE.lose)
        {
            WinLoseUIScript.Instance.renderimg = -1;
            //Debug.Log("Nibba just lost");
        }
        else if (condition == WINLOSE.win)
        {
            WinLoseUIScript.Instance.renderimg = 1;
            //Debug.Log("Nibba just won");
        }

    }

    public float ReturnAlpha()
    {
        float alpha = 1.0f * ((ref_losingTimer - losingTimer) / ref_losingTimer);
        return alpha;
    }

    
}
